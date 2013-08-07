namespace MyHome2013
{
    partial class NewChoice
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
            this.btnOpen = new System.Windows.Forms.Button();
            this.lblChoose = new System.Windows.Forms.Label();
            this.rdbExpense = new System.Windows.Forms.RadioButton();
            this.rdbIncome = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(53, 102);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(65, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // lblChoose
            // 
            this.lblChoose.Location = new System.Drawing.Point(4, 9);
            this.lblChoose.Name = "lblChoose";
            this.lblChoose.Size = new System.Drawing.Size(186, 32);
            this.lblChoose.TabIndex = 1;
            this.lblChoose.Text = "Please choose what type of new item you are opening:";
            // 
            // rdbExpense
            // 
            this.rdbExpense.AutoSize = true;
            this.rdbExpense.Location = new System.Drawing.Point(4, 45);
            this.rdbExpense.Name = "rdbExpense";
            this.rdbExpense.Size = new System.Drawing.Size(66, 17);
            this.rdbExpense.TabIndex = 2;
            this.rdbExpense.TabStop = true;
            this.rdbExpense.Text = "Expense";
            this.rdbExpense.UseVisualStyleBackColor = true;
            // 
            // rdbIncome
            // 
            this.rdbIncome.AutoSize = true;
            this.rdbIncome.Location = new System.Drawing.Point(4, 69);
            this.rdbIncome.Name = "rdbIncome";
            this.rdbIncome.Size = new System.Drawing.Size(60, 17);
            this.rdbIncome.TabIndex = 3;
            this.rdbIncome.TabStop = true;
            this.rdbIncome.Text = "Income";
            this.rdbIncome.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(124, 102);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // NewChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 137);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.rdbIncome);
            this.Controls.Add(this.rdbExpense);
            this.Controls.Add(this.lblChoose);
            this.Controls.Add(this.btnOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewChoice";
            this.Text = "Opening...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label lblChoose;
        private System.Windows.Forms.RadioButton rdbExpense;
        private System.Windows.Forms.RadioButton rdbIncome;
        private System.Windows.Forms.Button btnCancel;
    }
}