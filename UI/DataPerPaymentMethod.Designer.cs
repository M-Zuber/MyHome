namespace MyHome2013
{
    partial class DataPerPaymentMethod
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblExpenses = new System.Windows.Forms.Label();
            this.lblIncome = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.crtExpenses = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.crtIncome = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dtPick = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMonth = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crtExpenses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.crtIncome)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblExpenses
            // 
            this.lblExpenses.AutoSize = true;
            this.lblExpenses.Location = new System.Drawing.Point(3, 0);
            this.lblExpenses.Name = "lblExpenses";
            this.lblExpenses.Size = new System.Drawing.Size(79, 13);
            this.lblExpenses.TabIndex = 5;
            this.lblExpenses.Text = "Expenses Data";
            // 
            // lblIncome
            // 
            this.lblIncome.AutoSize = true;
            this.lblIncome.Location = new System.Drawing.Point(372, 0);
            this.lblIncome.Name = "lblIncome";
            this.lblIncome.Size = new System.Drawing.Size(68, 13);
            this.lblIncome.TabIndex = 4;
            this.lblIncome.Text = "Income Data";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.crtExpenses, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.crtIncome, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblExpenses, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblIncome, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(738, 322);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // crtExpenses
            // 
            chartArea1.Name = "ChartArea1";
            this.crtExpenses.ChartAreas.Add(chartArea1);
            this.crtExpenses.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.crtExpenses.Legends.Add(legend1);
            this.crtExpenses.Location = new System.Drawing.Point(3, 23);
            this.crtExpenses.Name = "crtExpenses";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.CustomProperties = "PieDrawingStyle=Concave";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.crtExpenses.Series.Add(series1);
            this.crtExpenses.Size = new System.Drawing.Size(363, 296);
            this.crtExpenses.TabIndex = 2;
            this.crtExpenses.Text = "chart1";
            // 
            // crtIncome
            // 
            chartArea2.Name = "ChartArea1";
            this.crtIncome.ChartAreas.Add(chartArea2);
            this.crtIncome.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.crtIncome.Legends.Add(legend2);
            this.crtIncome.Location = new System.Drawing.Point(372, 23);
            this.crtIncome.Name = "crtIncome";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.CustomProperties = "PieDrawingStyle=Concave";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.crtIncome.Series.Add(series2);
            this.crtIncome.Size = new System.Drawing.Size(363, 296);
            this.crtIncome.TabIndex = 3;
            this.crtIncome.Text = "chart1";
            // 
            // dtPick
            // 
            this.dtPick.Location = new System.Drawing.Point(3, 7);
            this.dtPick.Name = "dtPick";
            this.dtPick.Size = new System.Drawing.Size(200, 20);
            this.dtPick.TabIndex = 0;
            this.dtPick.ValueChanged += new System.EventHandler(this.dtPick_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMonth);
            this.panel1.Controls.Add(this.dtPick);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 322);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(738, 32);
            this.panel1.TabIndex = 8;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(209, 11);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(0, 13);
            this.lblMonth.TabIndex = 2;
            // 
            // DataPerPaymentMethod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 354);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "DataPerPaymentMethod";
            this.Text = "Payment Method per Month";
            this.Load += new System.EventHandler(this.DataPerPaymentMethod_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crtExpenses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.crtIncome)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblExpenses;
        private System.Windows.Forms.Label lblIncome;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart crtExpenses;
        private System.Windows.Forms.DataVisualization.Charting.Chart crtIncome;
        private System.Windows.Forms.DateTimePicker dtPick;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMonth;
    }
}