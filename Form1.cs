/*Sam Collins, A00987689
 * Comp 3931 Audio Project
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Threading;

struct WaveHeader
{
    // chunk 0
    public int chunkID;
    public int fileSize;
    public int riffType;

    // chunk 1
    public int fmtID;
    public int fmtSize; // bytes for this chunk
    public int fmtCode;
    public int channels;
    public int sampleRate;
    public int byteRate;
    public int fmtBlockAlign;
    public int bitDepth;

    // chunk 2
    public int dataID;
    public int bytes;

    public float[] L;
    public float[] R;

}

namespace AudioManager
{
    public unsafe partial class Form1 : Form
    {
        [DllImport("Recorder.dll")]
        public static extern void setupBufferIn();
        [DllImport("Recorder.dll")]
        public static extern byte** setupWaveIn();
        [DllImport("Recorder.dll")]
        public static extern bool* endRecording();

        [DllImport("Recorder.dll")]
        public static extern void setupBufferOut();
        [DllImport("Recorder.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void setupWaveOut(byte** samples);
        [DllImport("Recorder.dll")]
        public static extern void pauseWave();
        [DllImport("Recorder.dll")]
        public static extern void resumeWave();

        [DllImport("Recorder.dll")]
        public static extern int getLengthOfSamples();
        [DllImport("Recorder.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void setLengthOfSamples(int length);

        [DllImport("Recorder.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte** initializeSamples(int numBytes);

        struct Complex
        {
            public double real;
            public double imag;
        }

        WaveHeader head = new WaveHeader();

        private byte** samples = null;
        private byte* fSamples = null;

        static bool bRecording = false;
        static bool bPaused = false;
        static bool hasRecording = false;

        static bool bThreading = true;
        static bool bBenchmarking = false;

        static bool bFileOpen = false;
        static bool bRecordOpen = false;
        static bool canFilter = false;

        static bool bTriWin = false;
        static bool bWelchWin = false;
        static bool bSineWin = false;

        static Complex[] F;
        static double[] filter;
        static double[] filteredSamples;

        static double maxSampSize = 0;
        static Object filterLock = new Object();

        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void WaveChart_Click(object sender, EventArgs e)
        {
         
        }

        private void FreqChart_Click(object sender, EventArgs e)
        {

        }

        private void openWaveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Wave File (*.wav)|*.wav;";
            if (open.ShowDialog() != DialogResult.OK) return;
            
            FileStream fs = File.Open(open.FileName, FileMode.Open);

            BinaryReader reader = new BinaryReader(fs);

            head.chunkID = reader.ReadInt32();
            head.fileSize = reader.ReadInt32();
            head.riffType = reader.ReadInt32();


            // chunk 1
            head.fmtID = reader.ReadInt32();
            head.fmtSize = reader.ReadInt32(); // bytes for this chunk
            head.fmtCode = reader.ReadInt16();
            head.channels = reader.ReadInt16();
            head.sampleRate = reader.ReadInt32();
            head.byteRate = reader.ReadInt32();
            head.fmtBlockAlign = reader.ReadInt16();
            head.bitDepth = reader.ReadInt16();

            if (head.fmtSize == 18)
            {
                // Read any extra values
                int fmtExtraSize = reader.ReadInt16();
                reader.ReadBytes(fmtExtraSize);
            }

            head.dataID = reader.ReadInt32();
            head.bytes = reader.ReadInt32();

            byte[] byteArray = reader.ReadBytes(head.bytes);

            int bytesForSamp = head.bitDepth / 8;
            int samps = head.bytes / bytesForSamp;

            float[] asFloat = null;
            switch (head.bitDepth)
            {
                case 64:
                    double[]
                    asDouble = new double[samps];
                    Buffer.BlockCopy(byteArray, 0, asDouble, 0, head.bytes);
                    asFloat = Array.ConvertAll(asDouble, ev => (float)ev);
                    break;
                case 32:
                    asFloat = new float[samps];
                    Buffer.BlockCopy(byteArray, 0, asFloat, 0, head.bytes);
                    break;
                case 16:
                    Int16[]
                    asInt16 = new Int16[samps];
                    Buffer.BlockCopy(byteArray, 0, asInt16, 0, head.bytes);
                    asFloat = Array.ConvertAll(asInt16, ev => ev / (float)Int16.MaxValue);
                    break;
                default:
                    break;
            }

            switch (head.channels)
            {
                case 1:
                    head.L = asFloat;
                    head.R = null;
                    break;
                case 2:
                    head.L = new float[samps];
                    head.R = new float[samps];
                    for (int i = 0, s = 0; i < samps; i++)
                    {
                        head.L[i] = asFloat[s++];
                        head.R[i] = asFloat[s++];
                    }
                    break;
                default:
                    break;
            }

            hasRecording = false;

            clearWave();
            drawWave();

            bFileOpen = true;
            bRecordOpen = false;

            storeFileSamples();
        }

        private void storeFileSamples()
        {
            double[] dSamples = new double[head.L.Length];
            for (int i = 0; i < head.L.Length; i++)
            {
                dSamples[i] = head.L[i];
            }
            byte[] bSamples = samplesDtB(dSamples);

            samples = initializeSamples(bSamples.Length);
            fixed (byte* temp = &bSamples[0])
            {
                *samples = temp;
            }
            setLengthOfSamples(bSamples.Length);
        }

        private void copy()
        {
            int start = (int)WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionStart;
            int end = (int)WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionEnd;

            int temp;
            int numSamples;

            if (start > head.L.Length)
            {
                start = head.L.Length;
            }

            if (end > head.L.Length)
            {
                end = head.L.Length;
            }

            if (start > end)
            {
                temp = start;
                start = end;
                end = temp;
            }

            if (start < 0 || end < 0)
            {
                return;
            }

            numSamples = (end - start);

            var byteArray = new byte[numSamples * head.channels * 4];
            Buffer.BlockCopy(head.L, start * 4, byteArray, 0, byteArray.Length);

            if (head.channels == 2)
            {
                Buffer.BlockCopy(head.R, start, byteArray, byteArray.Length / head.channels, byteArray.Length / head.channels);
            }

            Clipboard.Clear();
            Clipboard.SetAudio(byteArray);

        }

        private void recordcopy()
        {
            int start = (int)WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionStart;
            int end = (int)WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionEnd;

            int temp;
            int numSamples;

            if (start > end)
            {
                temp = start;
                start = end;
                end = temp;
            }

            numSamples = (end - start);

            byte[] byteArray = new byte[numSamples];

            double[] dSamples = samplesBtD(*samples, getLengthOfSamples());

            for (int i = 0; i < numSamples; i++)
            {
                byteArray[i] = (byte)((dSamples[start + i] * 128) + 128);
            }

            Clipboard.Clear();
            Clipboard.SetAudio(byteArray);
        }

        private void paste()
        {
            int start = (int)WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionStart;
            int end = (int)WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionEnd;

            int temp;
            int samples;

            float[] floatArrayL = null;
            float[] floatArrayR = null;
            float[] newfloatArrayL = null;
            float[] newfloatArrayR = null;

            System.IO.Stream stream;

            if (start > head.L.Length)
            {
                start = head.L.Length;
            }

            if (end > head.L.Length)
            {
                end = head.L.Length;
            }

            if (start > end)
            {
                temp = start;
                start = end;
                end = temp;
            }

            if (start < 0 || end < 0 || !Clipboard.ContainsAudio())
            {
                return;
            }

            samples = (end - start);

            stream = Clipboard.GetAudioStream();

            var byteArrayL = new byte[stream.Length / head.channels];
            var byteArrayR = new byte[stream.Length / head.channels];

            stream.Read(byteArrayL, 0, byteArrayL.Length);
            stream.Read(byteArrayR, 0, byteArrayR.Length);

            floatArrayL = new float[byteArrayL.Length / 4];
            floatArrayR = new float[byteArrayR.Length / 4];

            Buffer.BlockCopy(byteArrayL, 0, floatArrayL, 0, byteArrayL.Length);
            newfloatArrayL = new float[head.L.Length + floatArrayL.Length];

            Array.Copy(head.L, 0, newfloatArrayL, 0, start);
            Array.Copy(floatArrayL, 0, newfloatArrayL, start, floatArrayL.Length);
            Array.Copy(head.L, start, newfloatArrayL, start + floatArrayL.Length, head.L.Length - start);

            head.L = newfloatArrayL;

            if (head.channels == 2)
            {
                Buffer.BlockCopy(byteArrayR, 0, floatArrayR, 0, byteArrayR.Length);
                newfloatArrayR = new float[head.R.Length + floatArrayR.Length];

                Array.Copy(head.R, 0, newfloatArrayR, 0, start);
                Array.Copy(floatArrayR, 0, newfloatArrayR, start, floatArrayR.Length);
                Array.Copy(head.R, start, newfloatArrayR, start + floatArrayR.Length, head.R.Length - start);

                head.R = newfloatArrayR;
            }
        }

        private void recordpaste()
        {
            int start = (int)WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionStart;
            int end = (int)WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionEnd;

            int temp;
            int length;

            if (start > end)
            {
                temp = start;
                start = end;
                end = temp;
            }

            System.IO.Stream stream;

            length = (end - start);

            stream = Clipboard.GetAudioStream();

            byte[] byteArray = new byte[stream.Length];

            stream.Read(byteArray, 0, byteArray.Length);

            fixed (byte* bStream = &byteArray[0])
            {
                double[] dSamples = samplesBtD(*samples, getLengthOfSamples());
                double[] paste = samplesBtD(bStream, byteArray.Length);
                double[] result = new double[dSamples.Length + paste.Length];

                Array.Copy(dSamples, 0, result, 0, start);
                Array.Copy(paste, 0, result, start, paste.Length);
                Array.Copy(dSamples, start, result, start + paste.Length, dSamples.Length - start);

                double[] test = new double[result.Length];
                Array.Copy(dSamples, start, test, start + paste.Length, dSamples.Length - start);

                byte[] bSamples = samplesDtB(result);
 
                setLengthOfSamples(bSamples.Length);

                fixed (byte* redraw = &bSamples[0])
                {
                    plotWave(redraw);
                    *samples = redraw;
                }
            }
            
        }

        private void delete()
        {
            int start = (int)WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionStart;
            int end = (int)WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionEnd;

            int temp;
            int samples = end - start;

            float[] newfloatArray = new float[head.L.Length - samples];

            Array.Copy(head.L, 0, newfloatArray, 0, start);
            Array.Copy(head.L, end, newfloatArray, start, newfloatArray.Length - start);

            head.L = newfloatArray;
        }

        private void recorddelete()
        {
            int start = (int)WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionStart;
            int end = (int)WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionEnd;

            int length = end - start;

            double[] dSamples = samplesBtD(*samples, getLengthOfSamples());
            double[] temp = new double[dSamples.Length - length];

            Array.Copy(dSamples, 0, temp, 0, start);
            Array.Copy(dSamples, end, temp, start, temp.Length - start);

            dSamples = temp;

            byte[] bSamples = samplesDtB(dSamples);
            
            setLengthOfSamples(bSamples.Length);

            fixed (byte* redraw = &bSamples[0])
            {
                *samples = redraw;
                plotWave(*samples);
            }
        }

        private void drawWave ()
        {
            clearWave();

            if (hasRecording)
            {
                plotWave(*samples);
            }
            else
            {
                int N = head.L.Length;

                for (int i = 0; i < N; i++)
                {
                    WaveChart.Series["Wave"].Points.AddXY(i, head.L[i]);
                }
            }
        }

        private double[] triangleWindow(double[] samples)
        {
            double[] windowed = new double[samples.Length];
            double N = samples.Length;

            for (int i = 0; i < samples.Length; i++)
            {
                windowed[i] = samples[i] * (1 - Math.Abs((i - ((N - 1) / 2.0)) / (N / 2.0)));
            }

            return windowed;
        }

        private double[] welchWindow(double[] samples)
        {
            double[] windowed = new double[samples.Length];
            double N = samples.Length;

            for (int i = 0; i < samples.Length; i++)
            {
                windowed[i] = samples[i] * (1 - Math.Pow(((i - ((N - 1.0) / 2.0)) / ((N - 1.0) / 2.0)), 2.0));
            }

            return windowed;
        }

        private double[] sineWindow(double[] samples)
        {
            double[] windowed = new double[samples.Length];
            double N = samples.Length;

            for (int i = 0; i < N; i++)
            {
                windowed[i] = samples[i] * Math.Sin((Math.PI * i) / (N - 1));
            }

            return windowed;
        }

        private static double[] filterIDFT(double[] filter)
        {
            int N = filter.Length;
            double[] temp = new double[N];

            for (int t = 0; t < N; t++)
            {
                for (int f = 0; f < N; f++)
                {
                    temp[t] += (filter[f] * Math.Cos(2 * Math.PI * t * f / N));
                }
            }
            return temp;
        }

        private double[] samplesBtD(byte* samples, int length)
        {
            double[] dSamples = new double[length];

            for (int i = 0; i < length; i++)
            {
                dSamples[i] = ((double)samples[i] - 128) / 128;
            }

            return dSamples;
        }

        private byte[] samplesDtB(double[] dSamples)
        {
            int length = dSamples.Length;

            byte[] bSamples = new byte[length];

            for (int i = 0; i < length; i++)
            {
                bSamples[i] = (byte)((dSamples[i] * 128) + 128);
            }

            return bSamples;
        }

        private static void convolve(double[] bufferedSamples, double[] filter)
        {
            int samplesLength = getLengthOfSamples();

            for (int i = 0; i < samplesLength; i++)
            {
                double num = 0;

                for (int j = i, f = 0; f < filter.Length; j++, f++)
                {
                    num += bufferedSamples[j] * filter[f];
                }

                if (Math.Abs(num) > maxSampSize)
                    maxSampSize = Math.Abs(num);

                filteredSamples[i] = num;
            }

        }

        private static void threadconvolve(double[] bufferedSamples, double[] filter, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                double num = 0;

                for (int j = i, f = 0; f < filter.Length; j++, f++)
                {
                    num += bufferedSamples[j] * filter[f];
                }

                lock (filterLock)
                {
                    if (Math.Abs(num) > maxSampSize)
                        maxSampSize = Math.Abs(num);
                }

                filteredSamples[i] = num;
            }
        }

        private void performConvolution(byte* samps, double[] filter)
        {
            int timerstart = 0;
            int timerend = 0;

            int samplesLength = getLengthOfSamples();
            double[] bufferedSamples = new double[samplesLength + filter.Length];
            filteredSamples = new double[samplesLength];

            double[] dSamples = new double[samplesLength];

            for (int i = 0; i < samplesLength; i++)
            {
                bufferedSamples[i] = ((double)samps[i] - 128) / 128;
            }

            if (bThreading)
            {
                timerstart = Environment.TickCount;

                Thread convolveThread1 = new Thread(() => threadconvolve(bufferedSamples, filter, 0, samplesLength / 2));
                convolveThread1.IsBackground = true;
                convolveThread1.Start();
                Thread convolveThread2 = new Thread(() => threadconvolve(bufferedSamples, filter, samplesLength / 2, samplesLength - 1));
                convolveThread2.IsBackground = true;
                convolveThread2.Start();

                convolveThread1.Join();
                convolveThread2.Join();

                timerend = Environment.TickCount;

                if (bBenchmarking)
                    MessageBox.Show("Threaded Convolution: " + (timerend - timerstart) + " Milliseconds");
            }
            else
            {
                timerstart = Environment.TickCount;
                convolve(bufferedSamples, filter);
                timerend = Environment.TickCount;

                if (bBenchmarking)
                    MessageBox.Show("Normal Convolution: " + (timerend - timerstart) + " Milliseconds");
            }

            for (int i = 0; i < dSamples.Length; i++)
            {
                dSamples[i] = filteredSamples[i] / maxSampSize;
            }

            byte[] bSamples = samplesDtB(dSamples);

            fixed(byte* filtered = &bSamples[0])
            {
                plotWave(filtered);
                *samples = filtered;
            }
            
        }

        private void createFilter()
        {
            //Get values from chart
            int cutoff = (int)FreqChart.ChartAreas["FreqChartArea"].CursorX.SelectionEnd;

            int N = (int)FreqChart.ChartAreas["FreqChartArea"].AxisX.Maximum;

            filter = new double[N];

            filter[0] = 1;
            int start = 1;
            int end = N - 1;

            for (int i = 0; i < cutoff; i++)
            {
                filter[start] = 1;
                filter[end] = 1;
                start++;
                end--;
            }

            //Inverse fourier
            filter = filterIDFT(filter);

            //convolution
            performConvolution(*samples, filter);
        }

        private static void fourier(double[] S, int N)
        {
            F = new Complex[N];

            double realPart = 0;
            double imagPart = 0;

            for (int f = 0; f < N; f ++)
            {
                realPart = 0;
                imagPart = 0;
                for (int t = 0; t < N; t++)
                {
                    realPart += S[t] * Math.Cos(2 * Math.PI * t * f / N);
                    imagPart -= S[t] * Math.Sin(2 * Math.PI * t * f / N);
                }

                F[f].real = realPart / N;
                F[f].imag = imagPart / N;
            }
        }

        private static void threadfourier(double[] S, int N, int offset)
        {
            F = new Complex[N];

            double realPart = 0;
            double imagPart = 0;

            for (int f = offset; f < N; f+=2)
            {
                realPart = 0;
                imagPart = 0;
                for (int t = 0; t < N; t++)
                {
                    realPart += S[t] * Math.Cos(2 * Math.PI * t * f / N);
                    imagPart -= S[t] * Math.Sin(2 * Math.PI * t * f / N);
                }

                F[f].real = realPart / N;
                F[f].imag = imagPart / N;
            }
        }

        private void performFourier()
        {
            int timerstart = 0;
            int timerend = 0;

            int start = (int)WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionStart;
            int end = (int)WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionEnd;

            int N = end - start; //Number of samples
            double[] windSamples = new double[N];

            for (int i = 0; i < N; i++)
            {
                windSamples[i] = WaveChart.Series["Wave"].Points[start + i].YValues[0];
            }

            if (bSineWin)
            {
                windSamples = sineWindow(windSamples);
            }
            if (bTriWin)
            {
                windSamples = triangleWindow(windSamples);
            }
            if (bWelchWin)
            {
                windSamples = welchWindow(windSamples);
            }
            
            if (bThreading)
            {
                timerstart = Environment.TickCount;

                Thread fourierThread1 = new Thread(() => threadfourier(windSamples, N, 0));
                fourierThread1.IsBackground = true;
                fourierThread1.Start();
                Thread fourierThread2 = new Thread(() => threadfourier(windSamples, N, 1));
                fourierThread2.IsBackground = true;
                fourierThread2.Start();

                fourierThread1.Join();
                fourierThread2.Join();

                timerend = Environment.TickCount;

                if(bBenchmarking)
                    MessageBox.Show("Threaded Fourier: " + (timerend - timerstart) + " Milliseconds");
            }
            else
            {
                timerstart = Environment.TickCount;
                fourier(windSamples, N);
                timerend = Environment.TickCount;

                if (bBenchmarking)
                    MessageBox.Show("Normal Fourier: " + (timerend - timerstart) + " Milliseconds");
            }
            canFilter = true;

        }

        private void drawFrequencies()
        {
            performFourier();

            while (FreqChart.Series["Frequencies"].Points.Count() > 0)
            {
                FreqChart.Series["Frequencies"].Points.RemoveAt(0);
            }

            for (int i = 0; i < F.Length; i++)
            {
                //Need to do pythag on the two parts of the complex number
                double Yval = Math.Sqrt(Math.Pow(F[i].real, 2) + Math.Pow(F[i].imag, 2));
                FreqChart.Series["Frequencies"].Points.AddXY(i, Yval);

            }
        }

        private void performFourierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawFrequencies();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hasRecording)
            {
                recordcopy();
            }
            else
            {
                copy();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hasRecording)
            {
                recordpaste();
            }
            else
            {
                paste();
                drawWave();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hasRecording)
            {
                recordcopy();
                recorddelete();
            }
            else
            {
                copy();
                delete();
                drawWave();
            }
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Zoom in function
            double zoomStart = WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionStart;
            double zoomEnd = WaveChart.ChartAreas["WaveChartArea"].CursorX.SelectionEnd;

            if (zoomEnd > zoomStart)
            {
                WaveChart.ChartAreas["WaveChartArea"].AxisX.ScaleView.Zoom(zoomStart, zoomEnd);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Zoom out function
            WaveChart.ChartAreas["WaveChartArea"].AxisX.ScaleView.ZoomReset();
        }

        private void recordBtn_Click(object sender, EventArgs e)
        {
            hasRecording = true;

            bRecording = !bRecording;
            if (bRecording)
            {
                recordBtn.Text = "End";
                samples = setupWaveIn();
            }
            else
            {
                recordBtn.Text = "Record";
                bool* bDone = endRecording();
                while (bDone == null || *bDone == false)
                {
                    System.Threading.Thread.Sleep(100);
                }
                drawWave();
            }
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            setupWaveOut(samples);
        }

        private void pauseBtn_Click(object sender, EventArgs e)
        {
            bPaused = !bPaused;
            if (bPaused)
            {
                pauseWave();
                pauseBtn.Text = "Resume";
            }
            else
            {
                resumeWave();
                pauseBtn.Text = "Pause";
            }
        }

        private void plotWave(byte* wave)
        {
            int sampleRate = 44100;
            clearWave();

            double sample = 0;
            int lengthOfSamples = getLengthOfSamples();
            for (int i = 0; i < lengthOfSamples; i++)
            {
                sample = ((double)wave[i] - 128) / 128;

                WaveChart.Series["Wave"].Points.AddXY(i, sample);

            }
        }

        private void clearWave()
        {
            while (WaveChart.Series["Wave"].Points.Count() > 0)
            {
                WaveChart.Series["Wave"].Points.RemoveAt(WaveChart.Series["Wave"].Points.Count() - 1);
            }
        }

        private void save()
        {
            int samplesLength = getLengthOfSamples();
            int channels = 0;
            int bitDepth = 0;
            int sampleRate = 0;
            int blockAlign = 0;

            if (hasRecording)
            {
                channels = 1;
                bitDepth = 16;
                sampleRate = 44100;
                blockAlign = 2;
            }
            else
            {
                channels = head.channels;
                bitDepth = head.bitDepth;
                sampleRate = head.sampleRate;
                blockAlign = head.fmtBlockAlign;
            }
            

            if (samplesLength == 0)
            {
                return;
            }
            SaveFileDialog mySave = new SaveFileDialog();
            mySave.Filter = "Wave File (*.wav)|*.wav;";
            if (mySave.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            FileStream myFilestream = File.Create(mySave.FileName);
            BinaryWriter binWriter = new BinaryWriter(myFilestream);

            binWriter.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));
            binWriter.Write((int)(32 + samplesLength * channels * bitDepth / 8));
            binWriter.Write(System.Text.Encoding.ASCII.GetBytes("WAVEfmt "));
            binWriter.Write((uint)16);
            binWriter.Write((ushort)(channels));
            binWriter.Write((ushort)(channels));
            binWriter.Write((uint)(sampleRate));
            binWriter.Write((uint)(sampleRate * channels * 32 / 8));
            binWriter.Write((ushort)(blockAlign));
            binWriter.Write((ushort)(16));

            binWriter.Write(System.Text.Encoding.ASCII.GetBytes("data"));
            binWriter.Write((uint)(samplesLength * channels * bitDepth / 8));

            double[] dSamples = samplesBtD(*samples, samplesLength);

            for (int i = 0; i < samplesLength; i++)
            {
                binWriter.Write((ushort)Math.Floor(dSamples[i] * Int16.MaxValue));
            }
            myFilestream.Close();
        }

        private void triangularWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bTriWin = true;
            bWelchWin = false;
            bSineWin = false;
        }
        private void welchWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bTriWin = false;
            bWelchWin = true;
            bSineWin = false;
        }
        private void sineWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bTriWin = false;
            bWelchWin = false;
            bSineWin = true;
        }

        private void noWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bTriWin = false;
            bWelchWin = false;
            bSineWin = false;
        }

        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (canFilter)
            {
                createFilter();
            }
        }

        private void threadingButton_Click(object sender, EventArgs e)
        {
            bThreading = !bThreading;
            if (bThreading)
            {
                threadBtn.Text = "Threading: On";
            }
            else
            {
                threadBtn.Text = "Threading: Off";
            }
        }

        private void toggleBenchmarkingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bBenchmarking = !bBenchmarking;
        }

        private void saveWaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save();
        }
    }
}
