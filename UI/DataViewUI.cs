using System;
using System.Data;
using System.Windows.Forms;
using BL;
using BusinessLogic;
using FrameWork;
using LocalTypes;

namespace MyHome2013
{
    /// <summary>
    /// Provides the data on income and expenses for the selected month
    /// </summary>
    public partial class DataViewUI : Form
    {
        #region Properties

        /// <summary>
        /// Holds a category list to bind to the combo box
        /// </summary>
        public DataTable CategoryList { get; set; }

        #endregion

        #region C'tor

        /// <summary>
        /// Standard default Ctor
        /// </summary>
        public DataViewUI()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Control Event Methods

        /// <summary>
        /// Connects the controls on the form with the data from the cache
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void DataViewUI_Load(object sender, EventArgs e)
        {
            // If there is any changes in the data asks the user if they want to save them
            if (Cache.SDB.HasChanges())
            {
                DialogResult = MessageBox.Show("Differences between the data in the program " +
                                               "and the saved data where detected\n" + 
                                               "To view the most update to-date information please save",
                                               "Loading...",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question,
                                               MessageBoxDefaultButton.Button1);

                // If the user is saving the changes
                if (DialogResult == DialogResult.Yes)
                {
                    GlobalBL.SaveFromCache();
                }
            }

            // Automatically forces the window to be open to its max size
            this.WindowState = FormWindowState.Maximized;
            
            // Binds the cache data to the form
            this.DataBinding();
        }

        /// <summary>
        /// Resets the data in the form based on the users date choice
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void dtPick_ValueChanged(object sender, EventArgs e)
        {
            this.DataBinding();
        }

        /// <summary>
        /// Opens a viewer to edit the expense clicked on
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">standard MouseEvent object</param>
        private void dgOut_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            using (ExpenseViewer viewAndEditExpense =
                new ExpenseViewer((Expense)this.dgOut.CurrentCell.OwningRow.DataBoundItem))
            {
                viewAndEditExpense.ShowDialog();
            }

            this.DataBinding();
        }

        /// <summary>
        /// Opens a viewer to edit the income clicked on
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">standard MouseEvent object</param>
        private void dgIn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            using (IncomeViewer viewAndEditIncome =
                new IncomeViewer((Income)this.dgIn.CurrentCell.OwningRow.DataBoundItem))
            {
                viewAndEditIncome.ShowDialog();
            }

            this.DataBinding();
        }

        /// <summary>
        /// Refreshes the data in the form every time it gains focus
        /// -This is to deal with multiple views into the same month being open
        /// and data being changed in a single one of them
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void DataViewUI_Enter(object sender, EventArgs e)
        {
            this.DataBinding();
        }

        #endregion

        #region Other Methods

        /// <summary>
        /// Sets up the data bindings to the combo box and the text box
        /// Refreshes also -by deleting and rebinding
        /// </summary>
        private void CategryDataBinding()
        {
            // Clears any old data bindings
            this.cmbCategory.DataSource = null;
            this.txtCategoryTotal.DataBindings.Clear();

            // Binds the data table with the list of categorys
            // and binds the text box to display the total for the given category
            this.cmbCategory.DataSource = this.CategoryList;
            this.cmbCategory.DisplayMember = "KEY";
            this.txtCategoryTotal.DataBindings.Add("Text", this.CategoryList, "VALUE");
        }

        /// <summary>
        /// Sets up the data bindings for the form
        /// </summary>
        private void DataBinding()
        {
            this.MonthlyDataBinding();
            this.CategryDataBinding();
        }

        /// <summary>
        /// Sets up the data bindings for the expense and income charts
        /// </summary>
        private void MonthlyDataBinding()
        {
            // Updates the data in the expense and income chart views
            this.dgOut.DataSource =
                ExpenseHandler.LoadOfMonth(dtPick.Value);
            this.dgOut.Columns["ID"].Visible = false;
            
            this.dgIn.DataSource =
                IncomeHandler.LoadOfMonth(dtPick.Value);
            this.dgIn.Columns["ID"].Visible = false;

            // Refreshes the data table with the category list and refreshes the data bindings
            this.CategoryList = new MonthViewBL(this.dtPick.Value).CuttingAll();
        } 

        #endregion
    }
}
