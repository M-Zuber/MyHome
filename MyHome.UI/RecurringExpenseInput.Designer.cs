namespace MyHome.UI
{
    partial class RecurringExpenseInput
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
            this.lblOutStartDate = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.lblOutAmount = new System.Windows.Forms.Label();
            this.lblOutMethod = new System.Windows.Forms.Label();
            this.lblOutCat = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtDetail = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbPayment = new System.Windows.Forms.ComboBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblOutEndDate = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblRecuurence = new System.Windows.Forms.Label();
            this.rbDay = new System.Windows.Forms.RadioButton();
            this.rbMonth = new System.Windows.Forms.RadioButton();
            this.rbYear = new System.Windows.Forms.RadioButton();
            this.pnRecurrenceOptions = new System.Windows.Forms.Panel();
            this.pnRecurrenceOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblOutStartDate
            // 
            this.lblOutStartDate.AutoSize = true;
            this.lblOutStartDate.Location = new System.Drawing.Point(12, 127);
            this.lblOutStartDate.Name = "lblOutStartDate";
            this.lblOutStartDate.Size = new System.Drawing.Size(55, 13);
            this.lblOutStartDate.TabIndex = 32;
            this.lblOutStartDate.Text = "Start Date";
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(12, 101);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(51, 13);
            this.lblComment.TabIndex = 31;
            this.lblComment.Text = "Comment";
            // 
            // lblOutAmount
            // 
            this.lblOutAmount.AutoSize = true;
            this.lblOutAmount.Location = new System.Drawing.Point(12, 75);
            this.lblOutAmount.Name = "lblOutAmount";
            this.lblOutAmount.Size = new System.Drawing.Size(43, 13);
            this.lblOutAmount.TabIndex = 30;
            this.lblOutAmount.Text = "Amount";
            // 
            // lblOutMethod
            // 
            this.lblOutMethod.AutoSize = true;
            this.lblOutMethod.Location = new System.Drawing.Point(12, 48);
            this.lblOutMethod.Name = "lblOutMethod";
            this.lblOutMethod.Size = new System.Drawing.Size(43, 13);
            this.lblOutMethod.TabIndex = 29;
            this.lblOutMethod.Text = "Method";
            // 
            // lblOutCat
            // 
            this.lblOutCat.AutoSize = true;
            this.lblOutCat.Location = new System.Drawing.Point(12, 19);
            this.lblOutCat.Name = "lblOutCat";
            this.lblOutCat.Size = new System.Drawing.Size(93, 13);
            this.lblOutCat.TabIndex = 28;
            this.lblOutCat.Text = "Expense Category";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "dd/MM/yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(105, 125);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(121, 20);
            this.dtpStartDate.TabIndex = 4;
            // 
            // txtDetail
            // 
            this.txtDetail.Location = new System.Drawing.Point(105, 99);
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.Size = new System.Drawing.Size(121, 20);
            this.txtDetail.TabIndex = 3;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(105, 73);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(121, 20);
            this.txtAmount.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(151, 253);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbPayment
            // 
            this.cmbPayment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayment.FormattingEnabled = true;
            this.cmbPayment.Location = new System.Drawing.Point(105, 46);
            this.cmbPayment.Name = "cmbPayment";
            this.cmbPayment.Size = new System.Drawing.Size(121, 21);
            this.cmbPayment.TabIndex = 1;
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(105, 19);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(121, 21);
            this.cmbCategory.TabIndex = 0;
            // 
            // lblOutEndDate
            // 
            this.lblOutEndDate.AutoSize = true;
            this.lblOutEndDate.Location = new System.Drawing.Point(12, 153);
            this.lblOutEndDate.Name = "lblOutEndDate";
            this.lblOutEndDate.Size = new System.Drawing.Size(52, 13);
            this.lblOutEndDate.TabIndex = 34;
            this.lblOutEndDate.Text = "End Date";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(105, 151);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(121, 20);
            this.dtpEndDate.TabIndex = 5;
            // 
            // lblRecuurence
            // 
            this.lblRecuurence.AutoSize = true;
            this.lblRecuurence.Location = new System.Drawing.Point(12, 179);
            this.lblRecuurence.Name = "lblRecuurence";
            this.lblRecuurence.Size = new System.Drawing.Size(77, 13);
            this.lblRecuurence.TabIndex = 35;
            this.lblRecuurence.Text = "Recurrs Every:";
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
            this.rbMonth.Location = new System.Drawing.Point(6, 26);
            this.rbMonth.Name = "rbMonth";
            this.rbMonth.Size = new System.Drawing.Size(55, 17);
            this.rbMonth.TabIndex = 1;
            this.rbMonth.TabStop = true;
            this.rbMonth.Text = "Month";
            this.rbMonth.UseVisualStyleBackColor = true;
            // 
            // rbYear
            // 
            this.rbYear.AutoSize = true;
            this.rbYear.Location = new System.Drawing.Point(6, 49);
            this.rbYear.Name = "rbYear";
            this.rbYear.Size = new System.Drawing.Size(47, 17);
            this.rbYear.TabIndex = 2;
            this.rbYear.Text = "Year";
            this.rbYear.UseVisualStyleBackColor = true;
            // 
            // pnRecurrenceOptions
            // 
            this.pnRecurrenceOptions.Controls.Add(this.rbYear);
            this.pnRecurrenceOptions.Controls.Add(this.rbDay);
            this.pnRecurrenceOptions.Controls.Add(this.rbMonth);
            this.pnRecurrenceOptions.Location = new System.Drawing.Point(89, 179);
            this.pnRecurrenceOptions.Name = "pnRecurrenceOptions";
            this.pnRecurrenceOptions.Size = new System.Drawing.Size(65, 68);
            this.pnRecurrenceOptions.TabIndex = 6;
            // 
            // RecurringExpenseInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 287);
            this.Controls.Add(this.lblRecuurence);
            this.Controls.Add(this.lblOutEndDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.lblOutStartDate);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.lblOutAmount);
            this.Controls.Add(this.lblOutMethod);
            this.Controls.Add(this.lblOutCat);
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
            this.Name = "RecurringExpenseInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recurring Expenses";
            this.Load += new System.EventHandler(this.RecurringExpenseInput_Load);
            this.pnRecurrenceOptions.ResumeLayout(false);
            this.pnRecurrenceOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOutStartDate;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Label lblOutAmount;
        private System.Windows.Forms.Label lblOutMethod;
        private System.Windows.Forms.Label lblOutCat;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbPayment;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblOutEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblRecuurence;
        private System.Windows.Forms.RadioButton rbDay;
        private System.Windows.Forms.RadioButton rbMonth;
        private System.Windows.Forms.RadioButton rbYear;
        private System.Windows.Forms.Panel pnRecurrenceOptions;
    }
}