using MyHome.Services;
using System;
using System.Windows.Forms;

namespace MyHome.UI
{
    /// <summary>
    /// The main form -allows the user to view and edit data of their income and expenses
    /// </summary>
    public partial class MenuMDIUI : Form
    {
        #region Properties

        /// <summary>
        /// The amount of Mdi children open
        /// </summary>
        public int MdiChilrenSum { get; set; }

        /// <summary>
        /// Single instance of expense categories form
        /// </summary>
        public ViewCategoriesUI ExpCatForm { get; set; }

        /// <summary>
        /// Single instance of income categories form
        /// </summary>
        public ViewCategoriesUI IncCatForm { get; set; }

        /// <summary>
        /// Single instance of payment method categories form
        /// </summary>
        public ViewCategoriesUI PaymentCatForm { get; set; }

        /// <summary>
        /// Single instance of new income form
        /// </summary>
        public InputINUI NewIncome { get; set; }

        /// <summary>
        /// Single instance of new expense form
        /// </summary>
        public InputOutUI NewExpense { get; set; }

        /// <summary>
        /// Single instance of new recurring expense form
        /// </summary>
        public RecurringExpenseInput NewRecurringExpense { get; set; }

        /// <summary>
        /// Single instance of new recurring income form
        /// </summary>
        public RecurringIncomeInput NewRecurringIncome { get; set; }

        #endregion

        #region C'tor

        /// <summary>
        /// Default ctor
        /// </summary>
        public MenuMDIUI()
        {
            InitializeComponent();
        }

        #endregion

        #region Control Event Methods

        /// <summary>
        /// Closes the form -on close FormClosing will activate and check for changes in
        /// the data base
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Shows or hides the tool bar
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void showToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = showToolBarToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Shows or hides the status bar
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void showStatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = showStatusBarToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Closes all child forms currently open
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        /// <summary>
        /// When the form loads the data is loaded from the data base into the cache
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void MenuMDIUI_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Opens the form to view the data for the whole month
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void viewDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChilrenSum++;
            DataViewUI ChildData = new DataViewUI();
            ChildData.MdiParent = this;
            ChildData.Show();
            ChildData.FormClosed += new FormClosedEventHandler(MdiChildClosed);
        }

        /// <summary>
        /// Opens the form to allow the user to add a new income
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void newIncomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (NewIncome == null)
            {
                MdiChilrenSum++;
                NewIncome = new InputINUI();
                NewIncome.MdiParent = this;
                NewIncome.Show();
                NewIncome.FormClosed += new FormClosedEventHandler(MdiChildClosed);
                NewIncome.FormClosed += new FormClosedEventHandler(NewIncomeClose); 
            }
            // Forces the form to the front
            else
            {
                NewIncome.BringToFront();
            }
        }

        /// <summary>
        /// Opens the form to allow the user to add a new expense
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void newExcpenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (NewExpense == null)
            {
                MdiChilrenSum++;
                NewExpense = new InputOutUI();
                NewExpense.MdiParent = this;
                NewExpense.Show();
                NewExpense.FormClosed += new FormClosedEventHandler(MdiChildClosed);
                NewExpense.FormClosed += new FormClosedEventHandler(NewExpenseClose);
            }
            // Forces the form to the front
            else
            {
                NewExpense.BringToFront();
            }
        }

        /// <summary>
        /// Opens the form to allow the user to add a new recurring expense
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void recurringExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (NewRecurringExpense == null)
            {
                MdiChilrenSum++;
                RecurringExpenseInput ChildData = new RecurringExpenseInput();
                ChildData.MdiParent = this;
                ChildData.Show();
                ChildData.FormClosed += new FormClosedEventHandler(MdiChildClosed);
                ChildData.FormClosed += new FormClosedEventHandler(NewRecurringExpenseClose);
            }
            // Forces the form to the front
            else
            {
                NewRecurringExpense.BringToFront();
            }
        }

        /// <summary>
        /// Opens the form to allow the user to add a new recurring income
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void recurringIncomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (NewRecurringIncome == null)
            {
                MdiChilrenSum++;
                RecurringIncomeInput ChildData = new RecurringIncomeInput();
                ChildData.MdiParent = this;
                ChildData.Show();
                ChildData.FormClosed += new FormClosedEventHandler(MdiChildClosed);
                ChildData.FormClosed += new FormClosedEventHandler(NewRecurringIncomeClose);
            }
            // Forces the form to the front
            else
            {
                NewRecurringIncome.BringToFront();
            }
        }

        /// <summary>
        /// Displays information about the program
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutUI()).ShowDialog();
        }

        /// <summary>
        /// Shows the data for the month in a pie chart
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void categoryPieChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChilrenSum++;
            MonthChartUI mcuNew = new MonthChartUI(DateTime.Now.Date);
            mcuNew.MdiParent = this;
            mcuNew.Show();
            mcuNew.FormClosed += new FormClosedEventHandler(MdiChildClosed);
        }

        private void methodPieChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChilrenSum++;
            DataPerPaymentMethod mcuNew = new DataPerPaymentMethod(DateTime.Now.Date);
            mcuNew.MdiParent = this;
            mcuNew.Show();
            mcuNew.FormClosed += new FormClosedEventHandler(MdiChildClosed);
        }

        private void methodGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChilrenSum++;
            MultipleCategoriesCompare mcuNew = new MultipleCategoriesCompare();
            mcuNew.MdiParent = this;
            mcuNew.Show();
            mcuNew.FormClosed += new FormClosedEventHandler(MdiChildClosed);
        }

        /// <summary>
        /// Shows a list of the options in the expenses category group
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void exCatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (ExpCatForm == null)
            {
                MdiChilrenSum++;
                ExpCatForm = new ViewCategoriesUI(CategoryType.Expense);
                ExpCatForm.MdiParent = this;
                ExpCatForm.Show();
                ExpCatForm.FormClosed += new FormClosedEventHandler(MdiChildClosed);
                ExpCatForm.FormClosed += new FormClosedEventHandler(ExpCatClose);
            }
            // Forces the form to the front
            else
            {
                ExpCatForm.BringToFront();
            }
        }

        /// <summary>
        /// Shows a list of the options in the income category group
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void incomeCatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (IncCatForm == null)
            {
                MdiChilrenSum++;
                IncCatForm = new ViewCategoriesUI(CategoryType.Income);
                IncCatForm.MdiParent = this;
                IncCatForm.Show();
                IncCatForm.FormClosed += new FormClosedEventHandler(MdiChildClosed);
                IncCatForm.FormClosed += new FormClosedEventHandler(IncCatClose);
            }
            // Forces the form to the front
            else
            {
                IncCatForm.BringToFront();
            }
        }

        /// <summary>
        /// Shows a list of the options in the payment method category group
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void paymentCatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (PaymentCatForm == null)
            {
                MdiChilrenSum++;
                PaymentCatForm = new ViewCategoriesUI(CategoryType.PaymentMethod);
                PaymentCatForm.MdiParent = this;
                PaymentCatForm.Show();
                PaymentCatForm.FormClosed += new FormClosedEventHandler(MdiChildClosed);
                PaymentCatForm.FormClosed += new FormClosedEventHandler(PaymentCatClose);
            }
            // Forces the form to the front
            else
            {
                PaymentCatForm.BringToFront();
            }
        }

        /// <summary>
        /// Shows a flow chart of the last year per selected category
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void categoryGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChilrenSum++;
            DataChartUI ChildData = new DataChartUI();
            ChildData.MdiParent = this;
            ChildData.Show();
            ChildData.FormClosed += new FormClosedEventHandler(MdiChildClosed);
        }

        /// <summary>
        /// When form closes checks for changes to cache and asks if they chould be saved
        ///  - also allows the user to cancel the exit
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void MenuMDIUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If the cache has any changes
            //if (DataStatusHandler.DataHasChanges())
            //{
            //    DialogResult = MessageBox.Show("Changes detected\nDo you want to save the changes?",
            //                                   "Closing...",
            //                                   MessageBoxButtons.YesNoCancel,
            //                                   MessageBoxIcon.Question,
            //                                   MessageBoxDefaultButton.Button1);

            //    // If the user is saving the changes
            //    // if the user wants to exit but not save changes the form will just close
            //    if (DialogResult == DialogResult.Yes)
            //    {
            //        GlobalHandler.SaveData();
            //    }
            //    // If the user does not want to exit the program
            //    else if (DialogResult == DialogResult.Cancel)
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }

        /// <summary>
        /// Saves changes to the data base if the user confirms
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            // If the cache has any changes
            //if (DataStatusHandler.DataHasChanges())
            //{
            //    DialogResult = MessageBox.Show("Changes detected\nDo you want to save the changes?",
            //                                   "Saving...",
            //                                   MessageBoxButtons.YesNo,
            //                                   MessageBoxIcon.Question,
            //                                   MessageBoxDefaultButton.Button1);

            //    // If the user is saving the changes
            //    if (DialogResult == DialogResult.Yes)
            //    {
            //        GlobalHandler.SaveData();
            //    }
            //}
        }

        /// <summary>
        /// Gives the user a choice of type of new item to add
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            int nResult;

            // Opens the form with the users choices
            using (NewChoice ncUserChoice = new NewChoice())
            {
                // the form is opened as ShowDialog to be able to preserve the
                // property values after it is closed
                ncUserChoice.ShowDialog();

                // Saves the users choice
                nResult = ncUserChoice.UserChoice;
            }

            // Opens the appropiate form based on the users choice that was sent back
            // Opening the form to add a new expense
            if (nResult == 1)
            {
                newExcpenceToolStripMenuItem_Click(sender, e);
            }
            // Opening the form to add a new income
            else if (nResult == 2)
            {
                newIncomeToolStripMenuItem_Click(sender, e);
            }
        }

        /// <summary>
        /// Backups the data currently in the cache, saving it to text files
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void backupStripMenuItem_Click(object sender, EventArgs e)
        {
            // Performs a backup of all the data, each table gets its own file
            (new ProgressForm()).BackupAllData();
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// When any form is opened updates the status bar with the number of mdi children
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void MenuMDIUI_MdiChildActivate(object sender, EventArgs e)
        {
            tslblMdiChildNumber.Text = MdiChilrenSum.ToString();
        }

        /// <summary>
        /// When the new income form is closed, sets the main forms property to null
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void NewIncomeClose(object sender, FormClosedEventArgs e)
        {
            NewIncome = null;
        }

        /// <summary>
        /// When the new expense form is closed, sets the main forms property to null
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void NewExpenseClose(object sender, FormClosedEventArgs e)
        {
            NewExpense = null;
        }

        /// <summary>
        /// When the new recurring expense form is closed, sets the main forms property to null
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void NewRecurringExpenseClose(object sender, FormClosedEventArgs e)
        {
            NewRecurringExpense = null;
        }

        /// <summary>
        /// When the new recurring income form is closed, sets the main forms property to null
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void NewRecurringIncomeClose(object sender, FormClosedEventArgs e)
        {
            NewRecurringIncome = null;
        }

        /// <summary>
        /// When the expense categories form is closed, sets the main forms property to null
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void ExpCatClose(object sender, FormClosedEventArgs e)
        {
            ExpCatForm = null;
        }

        /// <summary>
        /// When the income categories form is closed, sets the main forms property to null
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void IncCatClose(object sender, FormClosedEventArgs e)
        {
            IncCatForm = null;
        }

        /// <summary>
        /// When the payment method categories form is closed, sets the main forms property to null
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void PaymentCatClose(object sender, FormClosedEventArgs e)
        {
            PaymentCatForm = null;
        }

        /// <summary>
        /// For any mdi child form that closes updates the variable with the sum, 
        /// and updates the status bar
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void MdiChildClosed(object sender, FormClosedEventArgs e)
        {
            MdiChilrenSum--;
            tslblMdiChildNumber.Text = MdiChilrenSum.ToString();
        }

        #endregion      

    }
}
