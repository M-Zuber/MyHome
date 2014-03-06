using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
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
        public Dictionary<string, double> CategoryTotalsList { get; set; }

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
            // Automatically forces the window to be open to its max size
            this.WindowState = FormWindowState.Maximized;
            
            // Binds the cache data to the form
            this.DataBinding();

            // Sets up the event for re-entering the form
            this.Enter += this.DataViewUI_Enter;
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
            if (this.dgOut.CurrentCell != null)
            {
                using (ExpenseViewer viewAndEditExpense =
                        new ExpenseViewer((Expense)this.dgOut.CurrentCell.OwningRow.DataBoundItem))
                {
                    viewAndEditExpense.ShowDialog();
                }

                this.DataBinding(); 
            }
        }

        /// <summary>
        /// Opens a viewer to edit the income clicked on
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">standard MouseEvent object</param>
        private void dgIn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dgIn.CurrentCell != null)
            {
                using (IncomeViewer viewAndEditIncome =
                        new IncomeViewer((Income)this.dgIn.CurrentCell.OwningRow.DataBoundItem))
                {
                    viewAndEditIncome.ShowDialog();
                }

                this.DataBinding(); 
            }
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

            // Refreshes the data table with the category list and refreshes the data bindings
            this.CategoryTotalsList = new Dictionary<string, double>();            
            this.CategoryTotalsList.AddRange(ExpenseHandler.GetCategoryTotals(this.dtPick.Value));
            this.CategoryTotalsList.AddRange(IncomeHandler.GetCategoryTotals(this.dtPick.Value));

            // Binds the data table with the list of categorys
            // and binds the text box to display the total for the given category
            this.cmbCategory.DataSource = new ArrayList(this.CategoryTotalsList);
            this.cmbCategory.DisplayMember = "KEY";
            this.txtCategoryTotal.DataBindings.Add("Text", this.cmbCategory.DataSource, "VALUE");
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
        } 

        #endregion
    }
}
