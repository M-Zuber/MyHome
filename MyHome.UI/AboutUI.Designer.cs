namespace MyHome.UI
{
    partial class AboutUI
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
            this.lblAbout = new System.Windows.Forms.Label();
            this.btnOkay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAbout
            // 
            this.lblAbout.Location = new System.Drawing.Point(11, 9);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblAbout.Size = new System.Drawing.Size(211, 137);
            this.lblAbout.TabIndex = 0;
            this.lblAbout.Text = "MyHome 2013\r\nDesigned and Written  by\r\nS. Amar\r\nY. Biton\r\nA. Gonen\r\nM. Packter\r\nM" +
    ". Zuber\r\n\r\nUpdated by:\r\nM. Zuber";
            this.lblAbout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOkay
            // 
            this.btnOkay.Location = new System.Drawing.Point(84, 153);
            this.btnOkay.Name = "btnOkay";
            this.btnOkay.Size = new System.Drawing.Size(75, 23);
            this.btnOkay.TabIndex = 1;
            this.btnOkay.Text = "Okay";
            this.btnOkay.UseVisualStyleBackColor = true;
            this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
            // 
            // AboutUI
            // 
            this.AcceptButton = this.btnOkay;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 188);
            this.Controls.Add(this.btnOkay);
            this.Controls.Add(this.lblAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutUI";
            this.Text = "About MyHome";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblAbout;
        private System.Windows.Forms.Button btnOkay;
    }
}