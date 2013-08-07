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
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.txtCategoryTotal = new System.Windows.Forms.TextBox();
            this.dtPick = new System.Windows.Forms.DateTimePicker();
            this.lblPickDate = new System.Windows.Forms.Label();
            this.lblCatTotal = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgOut = new System.Windows.Forms.DataGridView();
            this.dgIn = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgIn)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(12, 269);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(107, 21);
            this.cmbCategory.TabIndex = 2;
            // 
            // txtCategoryTotal
            // 
            this.txtCategoryTotal.Location = new System.Drawing.Point(12, 296);
            this.txtCategoryTotal.Name = "txtCategoryTotal";
            this.txtCategoryTotal.ReadOnly = true;
            this.txtCategoryTotal.Size = new System.Drawing.Size(107, 20);
            this.txtCategoryTotal.TabIndex = 3;
            // 
            // dtPick
            // 
            this.dtPick.CustomFormat = "MM/yyyy";
            this.dtPick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPick.Location = new System.Drawing.Point(12, 48);
            this.dtPick.Name = "dtPick";
            this.dtPick.ShowUpDown = true;
            this.dtPick.Size = new System.Drawing.Size(107, 20);
            this.dtPick.TabIndex = 4;
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
            // lblCatTotal
            // 
            this.lblCatTotal.AutoSize = true;
            this.lblCatTotal.Location = new System.Drawing.Point(12, 240);
            this.lblCatTotal.Name = "lblCatTotal";
            this.lblCatTotal.Size = new System.Drawing.Size(99, 26);
            this.lblCatTotal.TabIndex = 6;
            this.lblCatTotal.Text = "Choose a category \r\nto view the total";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(136, 351);
            this.panel1.TabIndex = 7;
            // 
            // dgOut
            // 
            this.dgOut.AllowUserToAddRows = false;
            this.dgOut.AllowUserToDeleteRows = false;
            this.dgOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOut.Location = new System.Drawing.Point(3, 3);
            this.dgOut.Name = "dgOut";
            this.dgOut.ReadOnly = true;
            this.dgOut.Size = new System.Drawing.Size(206, 345);
            this.dgOut.TabIndex = 0;
            // 
            // dgIn
            // 
            this.dgIn.AllowUserToAddRows = false;
            this.dgIn.AllowUserToDeleteRows = false;
            this.dgIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgIn.Location = new System.Drawing.Point(215, 3);
            this.dgIn.Name = "dgIn";
            this.dgIn.ReadOnly = true;
            this.dgIn.Size = new System.Drawing.Size(206, 345);
            this.dgIn.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dgOut, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgIn, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(136, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(424, 351);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // DataViewUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 351);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lblCatTotal);
            this.Controls.Add(this.lblPickDate);
            this.Controls.Add(this.dtPick);
            this.Controls.Add(this.txtCategoryTotal);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.panel1);
            this.Name = "DataViewUI";
            this.Text = "Monthly Flow";
            this.Load += new System.EventHandler(this.DataViewUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgIn)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.TextBox txtCategoryTotal;
        private System.Windows.Forms.DateTimePicker dtPick;
        private System.Windows.Forms.Label lblPickDate;
        private System.Windows.Forms.Label lblCatTotal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgOut;
        private System.Windows.Forms.DataGridView dgIn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}