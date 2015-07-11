namespace MyHome.UI
{
    partial class InputINUI
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
            this.dtPick = new System.Windows.Forms.DateTimePicker();
            this.txtDetail = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbPayment = new System.Windows.Forms.ComboBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblInCat = new System.Windows.Forms.Label();
            this.lblInMethod = new System.Windows.Forms.Label();
            this.lblInAmount = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.lblInDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dtPick
            // 
            this.dtPick.CustomFormat = "dd/MM/yyyy";
            this.dtPick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPick.Location = new System.Drawing.Point(90, 115);
            this.dtPick.Name = "dtPick";
            this.dtPick.Size = new System.Drawing.Size(121, 20);
            this.dtPick.TabIndex = 4;
            // 
            // txtDetail
            // 
            this.txtDetail.Location = new System.Drawing.Point(90, 89);
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.Size = new System.Drawing.Size(121, 20);
            this.txtDetail.TabIndex = 3;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(90, 63);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(121, 20);
            this.txtAmount.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(70, 149);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbPayment
            // 
            this.cmbPayment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayment.FormattingEnabled = true;
            this.cmbPayment.Location = new System.Drawing.Point(90, 36);
            this.cmbPayment.Name = "cmbPayment";
            this.cmbPayment.Size = new System.Drawing.Size(121, 21);
            this.cmbPayment.TabIndex = 1;
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(90, 9);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(121, 21);
            this.cmbCategory.TabIndex = 0;
            // 
            // lblInCat
            // 
            this.lblInCat.AutoSize = true;
            this.lblInCat.Location = new System.Drawing.Point(3, 9);
            this.lblInCat.Name = "lblInCat";
            this.lblInCat.Size = new System.Drawing.Size(87, 13);
            this.lblInCat.TabIndex = 12;
            this.lblInCat.Text = "Income Category";
            // 
            // lblInMethod
            // 
            this.lblInMethod.AutoSize = true;
            this.lblInMethod.Location = new System.Drawing.Point(3, 38);
            this.lblInMethod.Name = "lblInMethod";
            this.lblInMethod.Size = new System.Drawing.Size(43, 13);
            this.lblInMethod.TabIndex = 13;
            this.lblInMethod.Text = "Method";
            // 
            // lblInAmount
            // 
            this.lblInAmount.AutoSize = true;
            this.lblInAmount.Location = new System.Drawing.Point(3, 65);
            this.lblInAmount.Name = "lblInAmount";
            this.lblInAmount.Size = new System.Drawing.Size(43, 13);
            this.lblInAmount.TabIndex = 14;
            this.lblInAmount.Text = "Amount";
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(3, 91);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(51, 13);
            this.lblComment.TabIndex = 15;
            this.lblComment.Text = "Comment";
            // 
            // lblInDate
            // 
            this.lblInDate.AutoSize = true;
            this.lblInDate.Location = new System.Drawing.Point(3, 117);
            this.lblInDate.Name = "lblInDate";
            this.lblInDate.Size = new System.Drawing.Size(30, 13);
            this.lblInDate.TabIndex = 16;
            this.lblInDate.Text = "Date";
            // 
            // InputINUI
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 174);
            this.Controls.Add(this.lblInDate);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.lblInAmount);
            this.Controls.Add(this.lblInMethod);
            this.Controls.Add(this.lblInCat);
            this.Controls.Add(this.dtPick);
            this.Controls.Add(this.txtDetail);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbPayment);
            this.Controls.Add(this.cmbCategory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputINUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Income";
            this.Load += new System.EventHandler(this.InputINUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtPick;
        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbPayment;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblInCat;
        private System.Windows.Forms.Label lblInMethod;
        private System.Windows.Forms.Label lblInAmount;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Label lblInDate;
    }
}