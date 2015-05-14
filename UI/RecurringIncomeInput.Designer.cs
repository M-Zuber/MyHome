namespace MyHome2013
{
    partial class RecurringIncomeInput
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
            this.lblRecuurence = new System.Windows.Forms.Label();
            this.lblInEndDate = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblInStartDate = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.lblInAmount = new System.Windows.Forms.Label();
            this.lblInMethod = new System.Windows.Forms.Label();
            this.lblInCat = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtDetail = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbPayment = new System.Windows.Forms.ComboBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.pnRecurrenceOptions = new System.Windows.Forms.Panel();
            this.rbYear = new System.Windows.Forms.RadioButton();
            this.rbDay = new System.Windows.Forms.RadioButton();
            this.rbMonth = new System.Windows.Forms.RadioButton();
            this.rbWeek = new System.Windows.Forms.RadioButton();
            this.pnRecurrenceOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRecuurence
            // 
            this.lblRecuurence.AutoSize = true;
            this.lblRecuurence.Location = new System.Drawing.Point(12, 174);
            this.lblRecuurence.Name = "lblRecuurence";
            this.lblRecuurence.Size = new System.Drawing.Size(77, 13);
            this.lblRecuurence.TabIndex = 50;
            this.lblRecuurence.Text = "Recurrs Every:";
            // 
            // lblInEndDate
            // 
            this.lblInEndDate.AutoSize = true;
            this.lblInEndDate.Location = new System.Drawing.Point(12, 148);
            this.lblInEndDate.Name = "lblInEndDate";
            this.lblInEndDate.Size = new System.Drawing.Size(52, 13);
            this.lblInEndDate.TabIndex = 49;
            this.lblInEndDate.Text = "End Date";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(105, 146);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(121, 20);
            this.dtpEndDate.TabIndex = 41;
            // 
            // lblInStartDate
            // 
            this.lblInStartDate.AutoSize = true;
            this.lblInStartDate.Location = new System.Drawing.Point(12, 122);
            this.lblInStartDate.Name = "lblInStartDate";
            this.lblInStartDate.Size = new System.Drawing.Size(55, 13);
            this.lblInStartDate.TabIndex = 48;
            this.lblInStartDate.Text = "Start Date";
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(12, 96);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(51, 13);
            this.lblComment.TabIndex = 47;
            this.lblComment.Text = "Comment";
            // 
            // lblInAmount
            // 
            this.lblInAmount.AutoSize = true;
            this.lblInAmount.Location = new System.Drawing.Point(12, 70);
            this.lblInAmount.Name = "lblInAmount";
            this.lblInAmount.Size = new System.Drawing.Size(43, 13);
            this.lblInAmount.TabIndex = 46;
            this.lblInAmount.Text = "Amount";
            // 
            // lblInMethod
            // 
            this.lblInMethod.AutoSize = true;
            this.lblInMethod.Location = new System.Drawing.Point(12, 43);
            this.lblInMethod.Name = "lblInMethod";
            this.lblInMethod.Size = new System.Drawing.Size(43, 13);
            this.lblInMethod.TabIndex = 45;
            this.lblInMethod.Text = "Method";
            // 
            // lblInCat
            // 
            this.lblInCat.AutoSize = true;
            this.lblInCat.Location = new System.Drawing.Point(12, 14);
            this.lblInCat.Name = "lblInCat";
            this.lblInCat.Size = new System.Drawing.Size(87, 13);
            this.lblInCat.TabIndex = 44;
            this.lblInCat.Text = "Income Category";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "dd/MM/yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(105, 120);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(121, 20);
            this.dtpStartDate.TabIndex = 40;
            // 
            // txtDetail
            // 
            this.txtDetail.Location = new System.Drawing.Point(105, 94);
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.Size = new System.Drawing.Size(121, 20);
            this.txtDetail.TabIndex = 39;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(105, 68);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(121, 20);
            this.txtAmount.TabIndex = 38;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(151, 265);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 43;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbPayment
            // 
            this.cmbPayment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayment.FormattingEnabled = true;
            this.cmbPayment.Location = new System.Drawing.Point(105, 41);
            this.cmbPayment.Name = "cmbPayment";
            this.cmbPayment.Size = new System.Drawing.Size(121, 21);
            this.cmbPayment.TabIndex = 37;
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(105, 14);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(121, 21);
            this.cmbCategory.TabIndex = 36;
            // 
            // pnRecurrenceOptions
            // 
            this.pnRecurrenceOptions.Controls.Add(this.rbWeek);
            this.pnRecurrenceOptions.Controls.Add(this.rbYear);
            this.pnRecurrenceOptions.Controls.Add(this.rbDay);
            this.pnRecurrenceOptions.Controls.Add(this.rbMonth);
            this.pnRecurrenceOptions.Location = new System.Drawing.Point(89, 174);
            this.pnRecurrenceOptions.Name = "pnRecurrenceOptions";
            this.pnRecurrenceOptions.Size = new System.Drawing.Size(65, 98);
            this.pnRecurrenceOptions.TabIndex = 42;
            // 
            // rbYear
            // 
            this.rbYear.AutoSize = true;
            this.rbYear.Location = new System.Drawing.Point(6, 72);
            this.rbYear.Name = "rbYear";
            this.rbYear.Size = new System.Drawing.Size(47, 17);
            this.rbYear.TabIndex = 2;
            this.rbYear.Text = "Year";
            this.rbYear.UseVisualStyleBackColor = true;
            // 
            // rbDay
            // 
            this.rbDay.AutoSize = true;
            this.rbDay.Location = new System.Drawing.Point(6, 3);
            this.rbDay.Name = "rbDay";
            this.rbDay.Size = new System.Drawing.Size(44, 17);
            this.rbDay.TabIndex = 0;
            this.rbDay.Text = "Day";
            this.rbDay.UseVisualStyleBackColor = true;
            // 
            // rbMonth
            // 
            this.rbMonth.AutoSize = true;
            this.rbMonth.Checked = true;
            this.rbMonth.Location = new System.Drawing.Point(6, 49);
            this.rbMonth.Name = "rbMonth";
            this.rbMonth.Size = new System.Drawing.Size(55, 17);
            this.rbMonth.TabIndex = 1;
            this.rbMonth.TabStop = true;
            this.rbMonth.Text = "Month";
            this.rbMonth.UseVisualStyleBackColor = true;
            // 
            // rbWeek
            // 
            this.rbWeek.AutoSize = true;
            this.rbWeek.Location = new System.Drawing.Point(6, 26);
            this.rbWeek.Name = "rbWeek";
            this.rbWeek.Size = new System.Drawing.Size(54, 17);
            this.rbWeek.TabIndex = 51;
            this.rbWeek.TabStop = true;
            this.rbWeek.Text = "Week";
            this.rbWeek.UseVisualStyleBackColor = true;
            // 
            // RecurringIncomeInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 312);
            this.Controls.Add(this.lblRecuurence);
            this.Controls.Add(this.lblInEndDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.lblInStartDate);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.lblInAmount);
            this.Controls.Add(this.lblInMethod);
            this.Controls.Add(this.lblInCat);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.txtDetail);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbPayment);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.pnRecurrenceOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecurringIncomeInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recurring Income";
            this.Load += new System.EventHandler(this.RecurringIncomeInput_Load);
            this.pnRecurrenceOptions.ResumeLayout(false);
            this.pnRecurrenceOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRecuurence;
        private System.Windows.Forms.Label lblInEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblInStartDate;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Label lblInAmount;
        private System.Windows.Forms.Label lblInMethod;
        private System.Windows.Forms.Label lblInCat;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbPayment;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Panel pnRecurrenceOptions;
        private System.Windows.Forms.RadioButton rbYear;
        private System.Windows.Forms.RadioButton rbDay;
        private System.Windows.Forms.RadioButton rbMonth;
        private System.Windows.Forms.RadioButton rbWeek;
    }
}