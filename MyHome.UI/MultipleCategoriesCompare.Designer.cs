namespace MyHome.UI
{
    partial class MultipleCategoriesCompare
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
            var chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            var legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.clearButton = new System.Windows.Forms.Button();
            this.calculateButton = new System.Windows.Forms.Button();
            this.seriesDisplayBox = new System.Windows.Forms.GroupBox();
            this.seriesNameValues = new System.Windows.Forms.ComboBox();
            this.chooseColorButton = new System.Windows.Forms.Button();
            this.seriesColorIndicator = new System.Windows.Forms.Label();
            this.seriesColorValue = new System.Windows.Forms.Label();
            this.seriesNameIndicator = new System.Windows.Forms.Label();
            this.catShowBox = new System.Windows.Forms.GroupBox();
            this.categoryPicker = new System.Windows.Forms.CheckedListBox();
            this.dateRangeBox = new System.Windows.Forms.GroupBox();
            this.endDate = new System.Windows.Forms.Label();
            this.startDate = new System.Windows.Forms.Label();
            this.endDateValue = new System.Windows.Forms.DateTimePicker();
            this.startDateValue = new System.Windows.Forms.DateTimePicker();
            this.categoryData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.seriesDisplayBox.SuspendLayout();
            this.catShowBox.SuspendLayout();
            this.dateRangeBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.categoryData)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.clearButton);
            this.splitContainer1.Panel1.Controls.Add(this.calculateButton);
            this.splitContainer1.Panel1.Controls.Add(this.seriesDisplayBox);
            this.splitContainer1.Panel1.Controls.Add(this.catShowBox);
            this.splitContainer1.Panel1.Controls.Add(this.dateRangeBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.categoryData);
            this.splitContainer1.Size = new System.Drawing.Size(651, 448);
            this.splitContainer1.SplitterDistance = 217;
            this.splitContainer1.TabIndex = 0;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(131, 413);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 4;
            this.clearButton.Text = "Clear Chart";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // calculateButton
            // 
            this.calculateButton.Location = new System.Drawing.Point(12, 413);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(75, 23);
            this.calculateButton.TabIndex = 3;
            this.calculateButton.Text = "Calculate";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.CalculateButton_Click);
            // 
            // seriesDisplayBox
            // 
            this.seriesDisplayBox.Controls.Add(this.seriesNameValues);
            this.seriesDisplayBox.Controls.Add(this.chooseColorButton);
            this.seriesDisplayBox.Controls.Add(this.seriesColorIndicator);
            this.seriesDisplayBox.Controls.Add(this.seriesColorValue);
            this.seriesDisplayBox.Controls.Add(this.seriesNameIndicator);
            this.seriesDisplayBox.Location = new System.Drawing.Point(12, 285);
            this.seriesDisplayBox.Name = "seriesDisplayBox";
            this.seriesDisplayBox.Size = new System.Drawing.Size(200, 122);
            this.seriesDisplayBox.TabIndex = 2;
            this.seriesDisplayBox.TabStop = false;
            this.seriesDisplayBox.Text = "Series Display Style";
            // 
            // seriesNameValues
            // 
            this.seriesNameValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.seriesNameValues.FormattingEnabled = true;
            this.seriesNameValues.Location = new System.Drawing.Point(83, 16);
            this.seriesNameValues.Name = "seriesNameValues";
            this.seriesNameValues.Size = new System.Drawing.Size(111, 21);
            this.seriesNameValues.TabIndex = 4;
            this.seriesNameValues.SelectedIndexChanged += new System.EventHandler(this.SeriesNameValues_SelectedIndexChanged);
            this.seriesNameValues.Click += new System.EventHandler(this.SeriesNameValues_Click);
            // 
            // chooseColorButton
            // 
            this.chooseColorButton.Location = new System.Drawing.Point(6, 59);
            this.chooseColorButton.Name = "chooseColorButton";
            this.chooseColorButton.Size = new System.Drawing.Size(91, 23);
            this.chooseColorButton.TabIndex = 3;
            this.chooseColorButton.Text = "Choose a Color";
            this.chooseColorButton.UseVisualStyleBackColor = true;
            this.chooseColorButton.Click += new System.EventHandler(this.ChooseColorButton_Click);
            // 
            // seriesColorIndicator
            // 
            this.seriesColorIndicator.AutoSize = true;
            this.seriesColorIndicator.Location = new System.Drawing.Point(6, 43);
            this.seriesColorIndicator.Name = "seriesColorIndicator";
            this.seriesColorIndicator.Size = new System.Drawing.Size(66, 13);
            this.seriesColorIndicator.TabIndex = 2;
            this.seriesColorIndicator.Text = "Series Color:";
            // 
            // seriesColorValue
            // 
            this.seriesColorValue.BackColor = System.Drawing.Color.Transparent;
            this.seriesColorValue.Location = new System.Drawing.Point(78, 43);
            this.seriesColorValue.Name = "seriesColorValue";
            this.seriesColorValue.Size = new System.Drawing.Size(87, 13);
            this.seriesColorValue.TabIndex = 1;
            // 
            // seriesNameIndicator
            // 
            this.seriesNameIndicator.AutoSize = true;
            this.seriesNameIndicator.Location = new System.Drawing.Point(6, 16);
            this.seriesNameIndicator.Name = "seriesNameIndicator";
            this.seriesNameIndicator.Size = new System.Drawing.Size(70, 13);
            this.seriesNameIndicator.TabIndex = 0;
            this.seriesNameIndicator.Text = "Series Name:";
            // 
            // catShowBox
            // 
            this.catShowBox.Controls.Add(this.categoryPicker);
            this.catShowBox.Location = new System.Drawing.Point(12, 129);
            this.catShowBox.Name = "catShowBox";
            this.catShowBox.Size = new System.Drawing.Size(200, 150);
            this.catShowBox.TabIndex = 1;
            this.catShowBox.TabStop = false;
            this.catShowBox.Text = "Categories To Show";
            // 
            // categoryPicker
            // 
            this.categoryPicker.CheckOnClick = true;
            this.categoryPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryPicker.FormattingEnabled = true;
            this.categoryPicker.Location = new System.Drawing.Point(3, 16);
            this.categoryPicker.Name = "categoryPicker";
            this.categoryPicker.Size = new System.Drawing.Size(194, 131);
            this.categoryPicker.TabIndex = 0;
            // 
            // dateRangeBox
            // 
            this.dateRangeBox.Controls.Add(this.endDate);
            this.dateRangeBox.Controls.Add(this.startDate);
            this.dateRangeBox.Controls.Add(this.endDateValue);
            this.dateRangeBox.Controls.Add(this.startDateValue);
            this.dateRangeBox.Location = new System.Drawing.Point(12, 12);
            this.dateRangeBox.Name = "dateRangeBox";
            this.dateRangeBox.Size = new System.Drawing.Size(200, 111);
            this.dateRangeBox.TabIndex = 0;
            this.dateRangeBox.TabStop = false;
            this.dateRangeBox.Text = "Date Range";
            // 
            // endDate
            // 
            this.endDate.AutoSize = true;
            this.endDate.Location = new System.Drawing.Point(6, 66);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(52, 13);
            this.endDate.TabIndex = 3;
            this.endDate.Text = "End Date";
            // 
            // startDate
            // 
            this.startDate.AutoSize = true;
            this.startDate.Location = new System.Drawing.Point(6, 27);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(55, 13);
            this.startDate.TabIndex = 2;
            this.startDate.Text = "Start Date";
            // 
            // endDateValue
            // 
            this.endDateValue.Location = new System.Drawing.Point(6, 82);
            this.endDateValue.Name = "endDateValue";
            this.endDateValue.Size = new System.Drawing.Size(188, 20);
            this.endDateValue.TabIndex = 1;
            this.endDateValue.ValueChanged += new System.EventHandler(this.EndDateValue_ValueChanged);
            // 
            // startDateValue
            // 
            this.startDateValue.Location = new System.Drawing.Point(6, 43);
            this.startDateValue.Name = "startDateValue";
            this.startDateValue.Size = new System.Drawing.Size(188, 20);
            this.startDateValue.TabIndex = 0;
            this.startDateValue.ValueChanged += new System.EventHandler(this.StartDateValue_ValueChanged);
            // 
            // categoryData
            // 
            chartArea2.Name = "ChartArea1";
            this.categoryData.ChartAreas.Add(chartArea2);
            this.categoryData.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.categoryData.Legends.Add(legend2);
            this.categoryData.Location = new System.Drawing.Point(0, 0);
            this.categoryData.Name = "categoryData";
            this.categoryData.Size = new System.Drawing.Size(430, 448);
            this.categoryData.TabIndex = 0;
            // 
            // colorDialog1
            // 
            this.colorDialog1.SolidColorOnly = true;
            // 
            // MultipleCategoriesCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 448);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MultipleCategoriesCompare";
            this.Text = "Compare Multiple Categories";
            this.Load += new System.EventHandler(this.PaymentMethodsOverTime_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.seriesDisplayBox.ResumeLayout(false);
            this.seriesDisplayBox.PerformLayout();
            this.catShowBox.ResumeLayout(false);
            this.dateRangeBox.ResumeLayout(false);
            this.dateRangeBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.categoryData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.GroupBox seriesDisplayBox;
        private System.Windows.Forms.GroupBox catShowBox;
        private System.Windows.Forms.GroupBox dateRangeBox;
        private System.Windows.Forms.Label seriesColorIndicator;
        private System.Windows.Forms.Label seriesColorValue;
        private System.Windows.Forms.Label seriesNameIndicator;
        private System.Windows.Forms.CheckedListBox categoryPicker;
        private System.Windows.Forms.Label endDate;
        private System.Windows.Forms.Label startDate;
        private System.Windows.Forms.DateTimePicker endDateValue;
        private System.Windows.Forms.DateTimePicker startDateValue;
        private System.Windows.Forms.DataVisualization.Charting.Chart categoryData;
        private System.Windows.Forms.Button chooseColorButton;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ComboBox seriesNameValues;
    }
}