namespace MyHome2013
{
    partial class MenuMDIUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuMDIUI));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tslblMdiChildAmount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblMdiChildNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mainMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDetailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newIncomeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newExcpenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleExpenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recurringExpenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pieChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frameWorkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exCatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.incomeCatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paymentCatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showStatusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.helpToolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(585, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.viewDetailToolStripMenuItem_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "He&lp";
            this.helpToolStripButton.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblMdiChildAmount,
            this.tslblMdiChildNumber});
            this.statusStrip.Location = new System.Drawing.Point(0, 271);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(585, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tslblMdiChildAmount
            // 
            this.tslblMdiChildAmount.Name = "tslblMdiChildAmount";
            this.tslblMdiChildAmount.Size = new System.Drawing.Size(91, 17);
            this.tslblMdiChildAmount.Text = "Windows Open:";
            // 
            // tslblMdiChildNumber
            // 
            this.tslblMdiChildNumber.Name = "tslblMdiChildNumber";
            this.tslblMdiChildNumber.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.frameWorkToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.showToolBarToolStripMenuItem,
            this.showStatusBarToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(585, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mainMenuToolStripMenuItem
            // 
            this.mainMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewDetailToolStripMenuItem,
            this.newIncomeToolStripMenuItem,
            this.newExcpenceToolStripMenuItem,
            this.pieChartToolStripMenuItem,
            this.graphToolStripMenuItem});
            this.mainMenuToolStripMenuItem.Name = "mainMenuToolStripMenuItem";
            this.mainMenuToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.mainMenuToolStripMenuItem.Text = "Main Menu";
            // 
            // viewDetailToolStripMenuItem
            // 
            this.viewDetailToolStripMenuItem.Name = "viewDetailToolStripMenuItem";
            this.viewDetailToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.viewDetailToolStripMenuItem.Text = "View Detail";
            this.viewDetailToolStripMenuItem.Click += new System.EventHandler(this.viewDetailToolStripMenuItem_Click);
            // 
            // newIncomeToolStripMenuItem
            // 
            this.newIncomeToolStripMenuItem.Name = "newIncomeToolStripMenuItem";
            this.newIncomeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newIncomeToolStripMenuItem.Text = "New Income";
            this.newIncomeToolStripMenuItem.Click += new System.EventHandler(this.newIncomeToolStripMenuItem_Click);
            // 
            // newExcpenceToolStripMenuItem
            // 
            this.newExcpenceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singleExpenseToolStripMenuItem,
            this.recurringExpenseToolStripMenuItem});
            this.newExcpenceToolStripMenuItem.Name = "newExcpenceToolStripMenuItem";
            this.newExcpenceToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newExcpenceToolStripMenuItem.Text = "New Expense";
            // 
            // singleExpenseToolStripMenuItem
            // 
            this.singleExpenseToolStripMenuItem.Name = "singleExpenseToolStripMenuItem";
            this.singleExpenseToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.singleExpenseToolStripMenuItem.Text = "Single Expense";
            this.singleExpenseToolStripMenuItem.Click += new System.EventHandler(this.newExcpenceToolStripMenuItem_Click);
            // 
            // recurringExpenseToolStripMenuItem
            // 
            this.recurringExpenseToolStripMenuItem.Name = "recurringExpenseToolStripMenuItem";
            this.recurringExpenseToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.recurringExpenseToolStripMenuItem.Text = "Recurring Expense";
            this.recurringExpenseToolStripMenuItem.Click += new System.EventHandler(this.recurringExpenseToolStripMenuItem_Click);
            // 
            // pieChartToolStripMenuItem
            // 
            this.pieChartToolStripMenuItem.Name = "pieChartToolStripMenuItem";
            this.pieChartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pieChartToolStripMenuItem.Text = "Pie Chart";
            this.pieChartToolStripMenuItem.Click += new System.EventHandler(this.pieChartToolStripMenuItem_Click);
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.graphToolStripMenuItem.Text = "Graph";
            this.graphToolStripMenuItem.Click += new System.EventHandler(this.graphToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // frameWorkToolStripMenuItem
            // 
            this.frameWorkToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exCatToolStripMenuItem,
            this.incomeCatToolStripMenuItem,
            this.paymentCatToolStripMenuItem});
            this.frameWorkToolStripMenuItem.Name = "frameWorkToolStripMenuItem";
            this.frameWorkToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.frameWorkToolStripMenuItem.Text = "Framework";
            // 
            // exCatToolStripMenuItem
            // 
            this.exCatToolStripMenuItem.Name = "exCatToolStripMenuItem";
            this.exCatToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.exCatToolStripMenuItem.Text = "Expense Cat";
            this.exCatToolStripMenuItem.Click += new System.EventHandler(this.exCatToolStripMenuItem_Click);
            // 
            // incomeCatToolStripMenuItem
            // 
            this.incomeCatToolStripMenuItem.Name = "incomeCatToolStripMenuItem";
            this.incomeCatToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.incomeCatToolStripMenuItem.Text = "Income Cat";
            this.incomeCatToolStripMenuItem.Click += new System.EventHandler(this.incomeCatToolStripMenuItem_Click);
            // 
            // paymentCatToolStripMenuItem
            // 
            this.paymentCatToolStripMenuItem.Name = "paymentCatToolStripMenuItem";
            this.paymentCatToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.paymentCatToolStripMenuItem.Text = "Payment Cat";
            this.paymentCatToolStripMenuItem.Click += new System.EventHandler(this.paymentCatToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.closeAllToolStripMenuItem.Text = "Close All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
            // 
            // showToolBarToolStripMenuItem
            // 
            this.showToolBarToolStripMenuItem.Checked = true;
            this.showToolBarToolStripMenuItem.CheckOnClick = true;
            this.showToolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showToolBarToolStripMenuItem.Name = "showToolBarToolStripMenuItem";
            this.showToolBarToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.showToolBarToolStripMenuItem.Text = "Show Tool Bar";
            this.showToolBarToolStripMenuItem.Click += new System.EventHandler(this.showToolBarToolStripMenuItem_Click);
            // 
            // showStatusBarToolStripMenuItem
            // 
            this.showStatusBarToolStripMenuItem.Checked = true;
            this.showStatusBarToolStripMenuItem.CheckOnClick = true;
            this.showStatusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showStatusBarToolStripMenuItem.Name = "showStatusBarToolStripMenuItem";
            this.showStatusBarToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.showStatusBarToolStripMenuItem.Text = "Show Status Bar";
            this.showStatusBarToolStripMenuItem.Click += new System.EventHandler(this.showStatusBarToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // MenuMDIUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MyHome2013.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(585, 293);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MenuMDIUI";
            this.Text = "MyHome 2013";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuMDIUI_FormClosing);
            this.Load += new System.EventHandler(this.MenuMDIUI_Load);
            this.MdiChildActivate += new System.EventHandler(this.MenuMDIUI_MdiChildActivate);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewDetailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newIncomeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newExcpenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pieChartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frameWorkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem incomeCatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exCatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paymentCatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showStatusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripStatusLabel tslblMdiChildAmount;
        private System.Windows.Forms.ToolStripStatusLabel tslblMdiChildNumber;
        private System.Windows.Forms.ToolStripMenuItem singleExpenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recurringExpenseToolStripMenuItem;

    }
}

