using MyHome.Services;
using System;
using System.Windows.Forms;

namespace MyHome.UI
{
    /// <inheritdoc />
    /// <summary>
    /// The main form -allows the user to view and edit data of their income and expenses
    /// </summary>
    public partial class MenuMDIUI : Form
    {
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

        /// <inheritdoc />
        /// <summary>
        /// Default ctor
        /// </summary>
        public MenuMDIUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes the form -on close FormClosing will activate and check for changes in
        /// the data base
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Shows or hides the tool bar
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void ShowToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = showToolBarToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Shows or hides the status bar
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void ShowStatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = showStatusBarToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Closes all child forms currently open
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var childForm in MdiChildren)
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
        private void ViewDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChilrenSum++;
            var childData = new DataViewUI {MdiParent = this};
            childData.Show();
            childData.FormClosed += MdiChildClosed;
        }

        /// <summary>
        /// Opens the form to allow the user to add a new income
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void NewIncomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (NewIncome == null)
            {
                MdiChilrenSum++;
                NewIncome = new InputINUI {MdiParent = this};
                NewIncome.Show();
                NewIncome.FormClosed += MdiChildClosed;
                NewIncome.FormClosed += NewIncomeClose; 
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
        private void NewExcpenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (NewExpense == null)
            {
                MdiChilrenSum++;
                NewExpense = new InputOutUI {MdiParent = this};
                NewExpense.Show();
                NewExpense.FormClosed += MdiChildClosed;
                NewExpense.FormClosed += NewExpenseClose;
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
        private void RecurringExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (NewRecurringExpense == null)
            {
                MdiChilrenSum++;
                var childData = new RecurringExpenseInput {MdiParent = this};
                childData.Show();
                childData.FormClosed += MdiChildClosed;
                childData.FormClosed += NewRecurringExpenseClose;
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
        private void RecurringIncomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (NewRecurringIncome == null)
            {
                MdiChilrenSum++;
                var childData = new RecurringIncomeInput {MdiParent = this};
                childData.Show();
                childData.FormClosed += MdiChildClosed;
                childData.FormClosed += NewRecurringIncomeClose;
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
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutUI().ShowDialog();
        }

        /// <summary>
        /// Shows the data for the month in a pie chart
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void CategoryPieChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChilrenSum++;
            var mcuNew = new MonthChartUI(DateTime.Now.Date) {MdiParent = this};
            mcuNew.Show();
            mcuNew.FormClosed += MdiChildClosed;
        }

        private void MethodPieChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChilrenSum++;
            var mcuNew = new DataPerPaymentMethod(DateTime.Now.Date) {MdiParent = this};
            mcuNew.Show();
            mcuNew.FormClosed += MdiChildClosed;
        }

        private void MethodGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChilrenSum++;
            var mcuNew = new MultipleCategoriesCompare {MdiParent = this};
            mcuNew.Show();
            mcuNew.FormClosed += MdiChildClosed;
        }

        /// <summary>
        /// Shows a list of the options in the expenses category group
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void ExCatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (ExpCatForm == null)
            {
                MdiChilrenSum++;
                ExpCatForm = new ViewCategoriesUI(CategoryType.Expense) {MdiParent = this};
                ExpCatForm.Show();
                ExpCatForm.FormClosed += MdiChildClosed;
                ExpCatForm.FormClosed += ExpCatClose;
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
        private void IncomeCatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (IncCatForm == null)
            {
                MdiChilrenSum++;
                IncCatForm = new ViewCategoriesUI(CategoryType.Income) {MdiParent = this};
                IncCatForm.Show();
                IncCatForm.FormClosed += MdiChildClosed;
                IncCatForm.FormClosed += IncCatClose;
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
        private void PaymentCatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the instance is not already open
            if (PaymentCatForm == null)
            {
                MdiChilrenSum++;
                PaymentCatForm = new ViewCategoriesUI(CategoryType.PaymentMethod) {MdiParent = this};
                PaymentCatForm.Show();
                PaymentCatForm.FormClosed += MdiChildClosed;
                PaymentCatForm.FormClosed += PaymentCatClose;
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
        private void CategoryGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChilrenSum++;
            var childData = new DataChartUI {MdiParent = this};
            childData.Show();
            childData.FormClosed += MdiChildClosed;
        }

        /// <summary>
        /// When form closes checks for changes to cache and asks if they could be saved
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
        private void SaveToolStripButton_Click(object sender, EventArgs e)
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
        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            int nResult;

            // Opens the form with the users choices
            using (var ncUserChoice = new NewChoice())
            {
                // the form is opened as ShowDialog to be able to preserve the
                // property values after it is closed
                ncUserChoice.ShowDialog();

                // Saves the users choice
                nResult = ncUserChoice.UserChoice;
            }

            // Opens the appropriate form based on the users choice that was sent back
            // Opening the form to add a new expense
            if (nResult == 1)
            {
                NewExcpenceToolStripMenuItem_Click(sender, e);
            }
            // Opening the form to add a new income
            else if (nResult == 2)
            {
                NewIncomeToolStripMenuItem_Click(sender, e);
            }
        }

        /// <summary>
        /// Backups the data currently in the cache, saving it to text files
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void BackupStripMenuItem_Click(object sender, EventArgs e)
        {
            // Performs a backup of all the data, each table gets its own file
            new ProgressForm().BackupAllData();
        }

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
    }
}
