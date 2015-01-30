namespace MyHome2013
{
    partial class ViewCategoriesUI
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
            this.dgvExpenseCategoryNames = new System.Windows.Forms.DataGridView();
            this.dgvIncomeCategoryNames = new System.Windows.Forms.DataGridView();
            this.dgvPaymentMethodNames = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenseCategoryNames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncomeCategoryNames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentMethodNames)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvExpenseCategoryNames
            // 
            this.dgvExpenseCategoryNames.AllowUserToDeleteRows = false;
            this.dgvExpenseCategoryNames.AllowUserToResizeRows = false;
            this.dgvExpenseCategoryNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvExpenseCategoryNames.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExpenseCategoryNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExpenseCategoryNames.Location = new System.Drawing.Point(9, 22);
            this.dgvExpenseCategoryNames.Margin = new System.Windows.Forms.Padding(6);
            this.dgvExpenseCategoryNames.Name = "dgvExpenseCategoryNames";
            this.dgvExpenseCategoryNames.RowHeadersWidth = 24;
            this.dgvExpenseCategoryNames.Size = new System.Drawing.Size(182, 311);
            this.dgvExpenseCategoryNames.TabIndex = 0;
            this.dgvExpenseCategoryNames.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.CellBeginEdit);
            this.dgvExpenseCategoryNames.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExpenseCategoryNames_CellEndEdit);
            // 
            // dgvIncomeCategoryNames
            // 
            this.dgvIncomeCategoryNames.AllowUserToDeleteRows = false;
            this.dgvIncomeCategoryNames.AllowUserToResizeRows = false;
            this.dgvIncomeCategoryNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvIncomeCategoryNames.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvIncomeCategoryNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIncomeCategoryNames.Location = new System.Drawing.Point(9, 22);
            this.dgvIncomeCategoryNames.Margin = new System.Windows.Forms.Padding(6);
            this.dgvIncomeCategoryNames.Name = "dgvIncomeCategoryNames";
            this.dgvIncomeCategoryNames.RowHeadersWidth = 24;
            this.dgvIncomeCategoryNames.Size = new System.Drawing.Size(182, 311);
            this.dgvIncomeCategoryNames.TabIndex = 1;
            this.dgvIncomeCategoryNames.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.CellBeginEdit);
            this.dgvIncomeCategoryNames.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIncomeCategoryNames_CellEndEdit);
            // 
            // dgvPaymentMethodNames
            // 
            this.dgvPaymentMethodNames.AllowUserToDeleteRows = false;
            this.dgvPaymentMethodNames.AllowUserToResizeRows = false;
            this.dgvPaymentMethodNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPaymentMethodNames.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPaymentMethodNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaymentMethodNames.Location = new System.Drawing.Point(9, 22);
            this.dgvPaymentMethodNames.Margin = new System.Windows.Forms.Padding(6);
            this.dgvPaymentMethodNames.Name = "dgvPaymentMethodNames";
            this.dgvPaymentMethodNames.RowHeadersWidth = 24;
            this.dgvPaymentMethodNames.Size = new System.Drawing.Size(182, 311);
            this.dgvPaymentMethodNames.TabIndex = 2;
            this.dgvPaymentMethodNames.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.CellBeginEdit);
            this.dgvPaymentMethodNames.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPaymentMethodNames_CellEndEdit);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.dgvExpenseCategoryNames);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 342);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Expense Categories";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.dgvIncomeCategoryNames);
            this.groupBox2.Location = new System.Drawing.Point(218, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 342);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Income Categories";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.dgvPaymentMethodNames);
            this.groupBox3.Location = new System.Drawing.Point(424, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 342);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Payment Methods";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(549, 360);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ViewCategoriesUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 395);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(652, 346);
            this.Name = "ViewCategoriesUI";
            this.Text = "Category Options";
            this.Load += new System.EventHandler(this.ViewCategoriesUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenseCategoryNames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncomeCategoryNames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentMethodNames)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvExpenseCategoryNames;
        private System.Windows.Forms.DataGridView dgvIncomeCategoryNames;
        private System.Windows.Forms.DataGridView dgvPaymentMethodNames;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
    }
}