namespace MyHome2013
{
    partial class InputOutUI
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
            this.cmbPayment = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtDetail = new System.Windows.Forms.TextBox();
            this.dtPick = new System.Windows.Forms.DateTimePicker();
            this.lblOutDate = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.lblOutAmount = new System.Windows.Forms.Label();
            this.lblOutMethod = new System.Windows.Forms.Label();
            this.lblOutCat = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(95, 9);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(121, 21);
            this.cmbCategory.TabIndex = 0;
            // 
            // cmbPayment
            // 
            this.cmbPayment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayment.FormattingEnabled = true;
            this.cmbPayment.Location = new System.Drawing.Point(95, 36);
            this.cmbPayment.Name = "cmbPayment";
            this.cmbPayment.Size = new System.Drawing.Size(121, 21);
            this.cmbPayment.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(73, 141);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(95, 63);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(121, 20);
            this.txtAmount.TabIndex = 2;
            // 
            // txtDetail
            // 
            this.txtDetail.Location = new System.Drawing.Point(95, 89);
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.Size = new System.Drawing.Size(121, 20);
            this.txtDetail.TabIndex = 3;
            // 
            // dtPick
            // 
            this.dtPick.CustomFormat = "dd/MM/yyyy";
            this.dtPick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPick.Location = new System.Drawing.Point(95, 115);
            this.dtPick.Name = "dtPick";
            this.dtPick.Size = new System.Drawing.Size(121, 20);
            this.dtPick.TabIndex = 4;
            // 
            // lblOutDate
            // 
            this.lblOutDate.AutoSize = true;
            this.lblOutDate.Location = new System.Drawing.Point(2, 117);
            this.lblOutDate.Name = "lblOutDate";
            this.lblOutDate.Size = new System.Drawing.Size(30, 13);
            this.lblOutDate.TabIndex = 21;
            this.lblOutDate.Text = "Date";
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(2, 91);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(51, 13);
            this.lblComment.TabIndex = 20;
            this.lblComment.Text = "Comment";
            // 
            // lblOutAmount
            // 
            this.lblOutAmount.AutoSize = true;
            this.lblOutAmount.Location = new System.Drawing.Point(2, 65);
            this.lblOutAmount.Name = "lblOutAmount";
            this.lblOutAmount.Size = new System.Drawing.Size(43, 13);
            this.lblOutAmount.TabIndex = 19;
            this.lblOutAmount.Text = "Amount";
            // 
            // lblOutMethod
            // 
            this.lblOutMethod.AutoSize = true;
            this.lblOutMethod.Location = new System.Drawing.Point(2, 38);
            this.lblOutMethod.Name = "lblOutMethod";
            this.lblOutMethod.Size = new System.Drawing.Size(43, 13);
            this.lblOutMethod.TabIndex = 18;
            this.lblOutMethod.Text = "Method";
            // 
            // lblOutCat
            // 
            this.lblOutCat.AutoSize = true;
            this.lblOutCat.Location = new System.Drawing.Point(2, 9);
            this.lblOutCat.Name = "lblOutCat";
            this.lblOutCat.Size = new System.Drawing.Size(93, 13);
            this.lblOutCat.TabIndex = 17;
            this.lblOutCat.Text = "Expense Category";
            // 
            // InputOutUI
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 173);
            this.Controls.Add(this.lblOutDate);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.lblOutAmount);
            this.Controls.Add(this.lblOutMethod);
            this.Controls.Add(this.lblOutCat);
            this.Controls.Add(this.dtPick);
            this.Controls.Add(this.txtDetail);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbPayment);
            this.Controls.Add(this.cmbCategory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputOutUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New expense";
            this.Load += new System.EventHandler(this.InputOutUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.ComboBox cmbPayment;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.DateTimePicker dtPick;
        private System.Windows.Forms.Label lblOutDate;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Label lblOutAmount;
        private System.Windows.Forms.Label lblOutMethod;
        private System.Windows.Forms.Label lblOutCat;
    }
}