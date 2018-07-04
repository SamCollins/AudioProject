using System;
namespace AudioManager
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.performFourierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triangularWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.welchWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sineWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleBenchmarkingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WavePanel = new System.Windows.Forms.Panel();
            this.WaveChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.FrequencyPanel = new System.Windows.Forms.Panel();
            this.FreqChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.recordBtn = new System.Windows.Forms.Button();
            this.playBtn = new System.Windows.Forms.Button();
            this.pauseBtn = new System.Windows.Forms.Button();
            this.threadBtn = new System.Windows.Forms.Button();
            this.saveWaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.WavePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaveChart)).BeginInit();
            this.FrequencyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FreqChart)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.analyzeToolStripMenuItem,
            this.modifyToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(929, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openWaveToolStripMenuItem,
            this.saveWaveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openWaveToolStripMenuItem
            // 
            this.openWaveToolStripMenuItem.Name = "openWaveToolStripMenuItem";
            this.openWaveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openWaveToolStripMenuItem.Text = "Open Wave";
            this.openWaveToolStripMenuItem.Click += new System.EventHandler(this.openWaveToolStripMenuItem_Click_1);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // analyzeToolStripMenuItem
            // 
            this.analyzeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.performFourierToolStripMenuItem});
            this.analyzeToolStripMenuItem.Name = "analyzeToolStripMenuItem";
            this.analyzeToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.analyzeToolStripMenuItem.Text = "Analyze";
            // 
            // performFourierToolStripMenuItem
            // 
            this.performFourierToolStripMenuItem.Name = "performFourierToolStripMenuItem";
            this.performFourierToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.performFourierToolStripMenuItem.Text = "Perform Fourier";
            this.performFourierToolStripMenuItem.Click += new System.EventHandler(this.performFourierToolStripMenuItem_Click);
            // 
            // modifyToolStripMenuItem
            // 
            this.modifyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noWindowToolStripMenuItem,
            this.triangularWindowToolStripMenuItem,
            this.welchWindowToolStripMenuItem,
            this.sineWindowToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
            this.modifyToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.modifyToolStripMenuItem.Text = "Modify";
            // 
            // noWindowToolStripMenuItem
            // 
            this.noWindowToolStripMenuItem.Name = "noWindowToolStripMenuItem";
            this.noWindowToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.noWindowToolStripMenuItem.Text = "No Window";
            this.noWindowToolStripMenuItem.Click += new System.EventHandler(this.noWindowToolStripMenuItem_Click);
            // 
            // triangularWindowToolStripMenuItem
            // 
            this.triangularWindowToolStripMenuItem.Name = "triangularWindowToolStripMenuItem";
            this.triangularWindowToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.triangularWindowToolStripMenuItem.Text = "Triangular Window";
            this.triangularWindowToolStripMenuItem.Click += new System.EventHandler(this.triangularWindowToolStripMenuItem_Click);
            // 
            // welchWindowToolStripMenuItem
            // 
            this.welchWindowToolStripMenuItem.Name = "welchWindowToolStripMenuItem";
            this.welchWindowToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.welchWindowToolStripMenuItem.Text = "Welch Window";
            this.welchWindowToolStripMenuItem.Click += new System.EventHandler(this.welchWindowToolStripMenuItem_Click);
            // 
            // sineWindowToolStripMenuItem
            // 
            this.sineWindowToolStripMenuItem.Name = "sineWindowToolStripMenuItem";
            this.sineWindowToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.sineWindowToolStripMenuItem.Text = "Sine Window";
            this.sineWindowToolStripMenuItem.Click += new System.EventHandler(this.sineWindowToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.filterToolStripMenuItem.Text = "Low Pass Filter";
            this.filterToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleBenchmarkingToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // toggleBenchmarkingToolStripMenuItem
            // 
            this.toggleBenchmarkingToolStripMenuItem.Name = "toggleBenchmarkingToolStripMenuItem";
            this.toggleBenchmarkingToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.toggleBenchmarkingToolStripMenuItem.Text = "Toggle Benchmarking";
            this.toggleBenchmarkingToolStripMenuItem.Click += new System.EventHandler(this.toggleBenchmarkingToolStripMenuItem_Click);
            // 
            // WavePanel
            // 
            this.WavePanel.Controls.Add(this.WaveChart);
            this.WavePanel.Location = new System.Drawing.Point(0, 27);
            this.WavePanel.Name = "WavePanel";
            this.WavePanel.Size = new System.Drawing.Size(929, 188);
            this.WavePanel.TabIndex = 2;
            this.WavePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // WaveChart
            // 
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.ScaleView.Zoomable = false;
            chartArea1.AxisX2.MajorGrid.Interval = 0.5D;
            chartArea1.AxisY.Maximum = 1D;
            chartArea1.AxisY.Minimum = -1D;
            chartArea1.CursorX.Interval = 0D;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorX.SelectionColor = System.Drawing.Color.MediumSpringGreen;
            chartArea1.Name = "WaveChartArea";
            this.WaveChart.ChartAreas.Add(chartArea1);
            this.WaveChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.WaveChart.Legends.Add(legend1);
            this.WaveChart.Location = new System.Drawing.Point(0, 0);
            this.WaveChart.Name = "WaveChart";
            series1.ChartArea = "WaveChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.Name = "Wave";
            this.WaveChart.Series.Add(series1);
            this.WaveChart.Size = new System.Drawing.Size(929, 188);
            this.WaveChart.TabIndex = 0;
            this.WaveChart.Text = "chart1";
            this.WaveChart.Click += new System.EventHandler(this.WaveChart_Click);
            // 
            // FrequencyPanel
            // 
            this.FrequencyPanel.Controls.Add(this.FreqChart);
            this.FrequencyPanel.Location = new System.Drawing.Point(0, 221);
            this.FrequencyPanel.Name = "FrequencyPanel";
            this.FrequencyPanel.Size = new System.Drawing.Size(929, 184);
            this.FrequencyPanel.TabIndex = 3;
            // 
            // FreqChart
            // 
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.ScaleView.Zoomable = false;
            chartArea2.CursorX.IsUserEnabled = true;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.CursorX.SelectionColor = System.Drawing.Color.MediumSpringGreen;
            chartArea2.CursorX.SelectionStart = 0D;
            chartArea2.Name = "FreqChartArea";
            this.FreqChart.ChartAreas.Add(chartArea2);
            this.FreqChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FreqChart.Location = new System.Drawing.Point(0, 0);
            this.FreqChart.Name = "FreqChart";
            series2.ChartArea = "FreqChartArea";
            series2.Name = "Frequencies";
            this.FreqChart.Series.Add(series2);
            this.FreqChart.Size = new System.Drawing.Size(929, 184);
            this.FreqChart.TabIndex = 0;
            this.FreqChart.Text = "chart1";
            this.FreqChart.Click += new System.EventHandler(this.FreqChart_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(432, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 24);
            this.button1.TabIndex = 5;
            this.button1.Text = "Zoom In";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(505, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 24);
            this.button2.TabIndex = 6;
            this.button2.Text = "Zoom Out";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // recordBtn
            // 
            this.recordBtn.Location = new System.Drawing.Point(593, 0);
            this.recordBtn.Name = "recordBtn";
            this.recordBtn.Size = new System.Drawing.Size(104, 25);
            this.recordBtn.TabIndex = 10;
            this.recordBtn.Text = "Record";
            this.recordBtn.UseVisualStyleBackColor = true;
            this.recordBtn.Click += new System.EventHandler(this.recordBtn_Click);
            // 
            // playBtn
            // 
            this.playBtn.Location = new System.Drawing.Point(703, 0);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(104, 25);
            this.playBtn.TabIndex = 11;
            this.playBtn.Text = "Play";
            this.playBtn.UseVisualStyleBackColor = true;
            this.playBtn.Click += new System.EventHandler(this.playBtn_Click);
            // 
            // pauseBtn
            // 
            this.pauseBtn.Location = new System.Drawing.Point(813, 0);
            this.pauseBtn.Name = "pauseBtn";
            this.pauseBtn.Size = new System.Drawing.Size(104, 24);
            this.pauseBtn.TabIndex = 13;
            this.pauseBtn.Text = "Pause";
            this.pauseBtn.UseVisualStyleBackColor = true;
            this.pauseBtn.Click += new System.EventHandler(this.pauseBtn_Click);
            // 
            // threadBtn
            // 
            this.threadBtn.Location = new System.Drawing.Point(304, 0);
            this.threadBtn.Name = "threadBtn";
            this.threadBtn.Size = new System.Drawing.Size(104, 25);
            this.threadBtn.TabIndex = 15;
            this.threadBtn.Text = "Threading: On";
            this.threadBtn.UseVisualStyleBackColor = true;
            this.threadBtn.Click += new System.EventHandler(this.threadingButton_Click);
            // 
            // saveWaveToolStripMenuItem
            // 
            this.saveWaveToolStripMenuItem.Name = "saveWaveToolStripMenuItem";
            this.saveWaveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveWaveToolStripMenuItem.Text = "Save Wave";
            this.saveWaveToolStripMenuItem.Click += new System.EventHandler(this.saveWaveToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 408);
            this.Controls.Add(this.threadBtn);
            this.Controls.Add(this.pauseBtn);
            this.Controls.Add(this.playBtn);
            this.Controls.Add(this.recordBtn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FrequencyPanel);
            this.Controls.Add(this.WavePanel);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.WavePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WaveChart)).EndInit();
            this.FrequencyPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FreqChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Panel WavePanel;
        private System.Windows.Forms.DataVisualization.Charting.Chart WaveChart;
        private System.Windows.Forms.Panel FrequencyPanel;
        private System.Windows.Forms.DataVisualization.Charting.Chart FreqChart;
        private System.Windows.Forms.ToolStripMenuItem openWaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button recordBtn;
        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.Button pauseBtn;
        private System.Windows.Forms.ToolStripMenuItem analyzeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem performFourierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triangularWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem welchWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sineWindowToolStripMenuItem;
        private System.Windows.Forms.Button threadBtn;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleBenchmarkingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveWaveToolStripMenuItem;
    }
}

