namespace MyHome2013
{
    partial class ProgressForm
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
            this.pgbAllDataProgress = new System.Windows.Forms.ProgressBar();
            this.lblAllDataProgress = new System.Windows.Forms.Label();
            this.lblCurrentTableProgress = new System.Windows.Forms.Label();
            this.pgbTableProgress = new System.Windows.Forms.ProgressBar();
            this.lblPatience = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pgbAllDataProgress
            // 
            this.pgbAllDataProgress.ForeColor = System.Drawing.Color.Cyan;
            this.pgbAllDataProgress.Location = new System.Drawing.Point(12, 66);
            this.pgbAllDataProgress.Maximum = 5;
            this.pgbAllDataProgress.Name = "pgbAllDataProgress";
            this.pgbAllDataProgress.Size = new System.Drawing.Size(194, 23);
            this.pgbAllDataProgress.Step = 1;
            this.pgbAllDataProgress.TabIndex = 0;
            // 
            // lblAllDataProgress
            // 
            this.lblAllDataProgress.AutoSize = true;
            this.lblAllDataProgress.Location = new System.Drawing.Point(12, 50);
            this.lblAllDataProgress.Name = "lblAllDataProgress";
            this.lblAllDataProgress.Size = new System.Drawing.Size(84, 13);
            this.lblAllDataProgress.TabIndex = 1;
            this.lblAllDataProgress.Text = "Overall Progress";
            // 
            // lblCurrentTableProgress
            // 
            this.lblCurrentTableProgress.AutoSize = true;
            this.lblCurrentTableProgress.Location = new System.Drawing.Point(12, 94);
            this.lblCurrentTableProgress.Name = "lblCurrentTableProgress";
            this.lblCurrentTableProgress.Size = new System.Drawing.Size(115, 13);
            this.lblCurrentTableProgress.TabIndex = 3;
            this.lblCurrentTableProgress.Text = "Current Table Progress";
            // 
            // pgbTableProgress
            // 
            this.pgbTableProgress.ForeColor = System.Drawing.Color.Cyan;
            this.pgbTableProgress.Location = new System.Drawing.Point(12, 110);
            this.pgbTableProgress.Maximum = 5;
            this.pgbTableProgress.Name = "pgbTableProgress";
            this.pgbTableProgress.Size = new System.Drawing.Size(194, 23);
            this.pgbTableProgress.Step = 2;
            this.pgbTableProgress.TabIndex = 2;
            // 
            // lblPatience
            // 
            this.lblPatience.Location = new System.Drawing.Point(37, 13);
            this.lblPatience.Name = "lblPatience";
            this.lblPatience.Size = new System.Drawing.Size(145, 37);
            this.lblPatience.TabIndex = 4;
            this.lblPatience.Text = "Please be patient while the operation is performed....\r\n";
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 145);
            this.Controls.Add(this.lblPatience);
            this.Controls.Add(this.lblCurrentTableProgress);
            this.Controls.Add(this.pgbTableProgress);
            this.Controls.Add(this.lblAllDataProgress);
            this.Controls.Add(this.pgbAllDataProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ProgressForm";
            this.Text = "ProgressForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgbAllDataProgress;
        private System.Windows.Forms.Label lblAllDataProgress;
        private System.Windows.Forms.Label lblCurrentTableProgress;
        private System.Windows.Forms.ProgressBar pgbTableProgress;
        private System.Windows.Forms.Label lblPatience;
    }
}