namespace MyHome2013
{
    partial class DataViewUI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbIncomeCategories = new System.Windows.Forms.ComboBox();
            this.txtIncomeCategoryTotal = new System.Windows.Forms.TextBox();
            this.dtPick = new System.Windows.Forms.DateTimePicker();
            this.lblPickDate = new System.Windows.Forms.Label();
            this.lblIncomeCatTotals = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblExpenseCatTotals = new System.Windows.Forms.Label();
            this.txtExpenseCategoryTotal = new System.Windows.Forms.TextBox();
            this.cmbExpenseCategories = new System.Windows.Forms.ComboBox();
            this.dgOut = new System.Windows.Forms.DataGridView();
            this.dgIn = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblIncomeChart = new System.Windows.Forms.Label();
            this.lblExpenseChart = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgIn)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbIncomeCategories
            // 
            this.cmbIncomeCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbIncomeCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIncomeCategories.FormattingEnabled = true;
            this.cmbIncomeCategories.Location = new System.Drawing.Point(3, 171);
            this.cmbIncomeCategories.Name = "cmbIncomeCategories";
            this.cmbIncomeCategories.Size = new System.Drawing.Size(130, 21);
            this.cmbIncomeCategories.TabIndex = 2;
            // 
            // txtIncomeCategoryTotal
            // 
            this.txtIncomeCategoryTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIncomeCategoryTotal.Location = new System.Drawing.Point(3, 213);
            this.txtIncomeCategoryTotal.Name = "txtIncomeCategoryTotal";
            this.txtIncomeCategoryTotal.ReadOnly = true;
            this.txtIncomeCategoryTotal.Size = new System.Drawing.Size(130, 20);
            this.txtIncomeCategoryTotal.TabIndex = 3;
            // 
            // dtPick
            // 
            this.dtPick.CustomFormat = "MM/yyyy";
            this.dtPick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPick.Location = new System.Drawing.Point(12, 48);
            this.dtPick.Name = "dtPick";
            this.dtPick.ShowUpDown = true;
            this.dtPick.Size = new System.Drawing.Size(107, 20);
            this.dtPick.TabIndex = 0;
            this.dtPick.ValueChanged += new System.EventHandler(this.dtPick_ValueChanged);
            // 
            // lblPickDate
            // 
            this.lblPickDate.AutoSize = true;
            this.lblPickDate.Location = new System.Drawing.Point(12, 26);
            this.lblPickDate.Name = "lblPickDate";
            this.lblPickDate.Size = new System.Drawing.Size(69, 13);
            this.lblPickDate.TabIndex = 5;
            this.lblPickDate.Text = "Pick a month";
            // 
            // lblIncomeCatTotals
            // 
            this.lblIncomeCatTotals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIncomeCatTotals.Location = new System.Drawing.Point(3, 126);
            this.lblIncomeCatTotals.Name = "lblIncomeCatTotals";
            this.lblIncomeCatTotals.Size = new System.Drawing.Size(130, 42);
            this.lblIncomeCatTotals.TabIndex = 6;
            this.lblIncomeCatTotals.Text = "Choose an income category \r\nto view the total";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(136, 351);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lblIncomeCatTotals, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblExpenseCatTotals, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtExpenseCategoryTotal, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cmbExpenseCategories, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtIncomeCategoryTotal, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.cmbIncomeCategories, 0, 4);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 93);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(136, 258);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // lblExpenseCatTotals
            // 
            this.lblExpenseCatTotals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExpenseCatTotals.Location = new System.Drawing.Point(3, 0);
            this.lblExpenseCatTotals.Name = "lblExpenseCatTotals";
            this.lblExpenseCatTotals.Size = new System.Drawing.Size(130, 42);
            this.lblExpenseCatTotals.TabIndex = 1;
            this.lblExpenseCatTotals.Text = "Choose an expense category \r\nto view the total";
            // 
            // txtExpenseCategoryTotal
            // 
            this.txtExpenseCategoryTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExpenseCategoryTotal.Location = new System.Drawing.Point(3, 87);
            this.txtExpenseCategoryTotal.Name = "txtExpenseCategoryTotal";
            this.txtExpenseCategoryTotal.ReadOnly = true;
            this.txtExpenseCategoryTotal.Size = new System.Drawing.Size(130, 20);
            this.txtExpenseCategoryTotal.TabIndex = 10;
            // 
            // cmbExpenseCategories
            // 
            this.cmbExpenseCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbExpenseCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExpenseCategories.FormattingEnabled = true;
            this.cmbExpenseCategories.Location = new System.Drawing.Point(3, 45);
            this.cmbExpenseCategories.Name = "cmbExpenseCategories";
            this.cmbExpenseCategories.Size = new System.Drawing.Size(130, 21);
            this.cmbExpenseCategories.TabIndex = 1;
            // 
            // dgOut
            // 
            this.dgOut.AllowUserToAddRows = false;
            this.dgOut.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgOut.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgOut.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOut.Location = new System.Drawing.Point(3, 23);
            this.dgOut.Name = "dgOut";
            this.dgOut.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgOut.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgOut.Size = new System.Drawing.Size(206, 325);
            this.dgOut.TabIndex = 3;
            this.dgOut.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgOut_MouseDoubleClick);
            // 
            // dgIn
            // 
            this.dgIn.AllowUserToAddRows = false;
            this.dgIn.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgIn.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgIn.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgIn.Location = new System.Drawing.Point(215, 23);
            this.dgIn.Name = "dgIn";
            this.dgIn.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgIn.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgIn.Size = new System.Drawing.Size(206, 325);
            this.dgIn.TabIndex = 4;
            this.dgIn.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgIn_MouseDoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblIncomeChart, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgOut, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgIn, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblExpenseChart, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(136, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(424, 351);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lblIncomeChart
            // 
            this.lblIncomeChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIncomeChart.AutoSize = true;
            this.lblIncomeChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIncomeChart.Location = new System.Drawing.Point(215, 0);
            this.lblIncomeChart.Name = "lblIncomeChart";
            this.lblIncomeChart.Size = new System.Drawing.Size(206, 20);
            this.lblIncomeChart.TabIndex = 3;
            this.lblIncomeChart.Text = "Income";
            this.lblIncomeChart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblExpenseChart
            // 
            this.lblExpenseChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExpenseChart.AutoSize = true;
            this.lblExpenseChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpenseChart.Location = new System.Drawing.Point(3, 0);
            this.lblExpenseChart.Name = "lblExpenseChart";
            this.lblExpenseChart.Size = new System.Drawing.Size(206, 20);
            this.lblExpenseChart.TabIndex = 3;
            this.lblExpenseChart.Text = "Expenses";
            this.lblExpenseChart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DataViewUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 351);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lblPickDate);
            this.Controls.Add(this.dtPick);
            this.Controls.Add(this.panel1);
            this.Name = "DataViewUI";
            this.Text = "Monthly Flow";
            this.Load += new System.EventHandler(this.DataViewUI_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgIn)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbIncomeCategories;
        private System.Windows.Forms.TextBox txtIncomeCategoryTotal;
        private System.Windows.Forms.DateTimePicker dtPick;
        private System.Windows.Forms.Label lblPickDate;
        private System.Windows.Forms.Label lblIncomeCatTotals;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgOut;
        private System.Windows.Forms.DataGridView dgIn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblIncomeChart;
        private System.Windows.Forms.Label lblExpenseChart;
        private System.Windows.Forms.Label lblExpenseCatTotals;
        private System.Windows.Forms.ComboBox cmbExpenseCategories;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtExpenseCategoryTotal;
    }
}