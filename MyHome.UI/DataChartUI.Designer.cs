namespace MyHome.UI
{
    partial class DataChartUI
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
            var chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            var legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.cmbCat = new System.Windows.Forms.ComboBox();
            this.crtGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblCategories = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtpStartMonth = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpEndMonth = new System.Windows.Forms.DateTimePicker();
            this.lblEndMonth = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.crtGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbCat
            // 
            this.cmbCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCat.FormattingEnabled = true;
            this.cmbCat.Location = new System.Drawing.Point(103, 14);
            this.cmbCat.Name = "cmbCat";
            this.cmbCat.Size = new System.Drawing.Size(121, 21);
            this.cmbCat.TabIndex = 0;
            this.cmbCat.SelectionChangeCommitted += new System.EventHandler(this.CmbCat_SelectedIndexChanged);
            // 
            // crtGraph
            // 
            chartArea1.Name = "ChartArea1";
            this.crtGraph.ChartAreas.Add(chartArea1);
            this.crtGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.crtGraph.Legends.Add(legend1);
            this.crtGraph.Location = new System.Drawing.Point(0, 0);
            this.crtGraph.Name = "crtGraph";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.crtGraph.Series.Add(series1);
            this.crtGraph.Size = new System.Drawing.Size(614, 315);
            this.crtGraph.TabIndex = 1;
            // 
            // lblCategories
            // 
            this.lblCategories.AutoSize = true;
            this.lblCategories.Location = new System.Drawing.Point(10, 17);
            this.lblCategories.Name = "lblCategories";
            this.lblCategories.Size = new System.Drawing.Size(87, 13);
            this.lblCategories.TabIndex = 1;
            this.lblCategories.Text = "Choose category";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dtpStartMonth);
            this.splitContainer1.Panel1.Controls.Add(this.lblStartDate);
            this.splitContainer1.Panel1.Controls.Add(this.dtpEndMonth);
            this.splitContainer1.Panel1.Controls.Add(this.lblEndMonth);
            this.splitContainer1.Panel1.Controls.Add(this.cmbCat);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.crtGraph);
            this.splitContainer1.Size = new System.Drawing.Size(614, 357);
            this.splitContainer1.SplitterDistance = 38;
            this.splitContainer1.TabIndex = 0;
            // 
            // dtpStartMonth
            // 
            this.dtpStartMonth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartMonth.Location = new System.Drawing.Point(299, 14);
            this.dtpStartMonth.Name = "dtpStartMonth";
            this.dtpStartMonth.Size = new System.Drawing.Size(81, 20);
            this.dtpStartMonth.TabIndex = 1;
            this.dtpStartMonth.ValueChanged += new System.EventHandler(this.DtpStartMonth_ValueChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(231, 17);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(62, 13);
            this.lblStartDate.TabIndex = 4;
            this.lblStartDate.Text = "Start Month";
            // 
            // dtpEndMonth
            // 
            this.dtpEndMonth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndMonth.Location = new System.Drawing.Point(457, 14);
            this.dtpEndMonth.Name = "dtpEndMonth";
            this.dtpEndMonth.Size = new System.Drawing.Size(79, 20);
            this.dtpEndMonth.TabIndex = 2;
            this.dtpEndMonth.ValueChanged += new System.EventHandler(this.DtpEndMonth_ValueChanged);
            // 
            // lblEndMonth
            // 
            this.lblEndMonth.AutoSize = true;
            this.lblEndMonth.Location = new System.Drawing.Point(392, 17);
            this.lblEndMonth.Name = "lblEndMonth";
            this.lblEndMonth.Size = new System.Drawing.Size(59, 13);
            this.lblEndMonth.TabIndex = 4;
            this.lblEndMonth.Text = "End Month";
            // 
            // DataChartUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 357);
            this.Controls.Add(this.lblCategories);
            this.Controls.Add(this.splitContainer1);
            this.Name = "DataChartUI";
            this.Text = "Multi Month Flow";
            this.Load += new System.EventHandler(this.DataChartUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.crtGraph)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCat;
        private System.Windows.Forms.DataVisualization.Charting.Chart crtGraph;
        private System.Windows.Forms.Label lblCategories;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DateTimePicker dtpEndMonth;
        private System.Windows.Forms.Label lblEndMonth;
        private System.Windows.Forms.DateTimePicker dtpStartMonth;
        private System.Windows.Forms.Label lblStartDate;
    }
}