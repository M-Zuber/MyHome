using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using BusinessLogic;
using FrameWork;
using LocalTypes;
using System.Globalization;
using System.ComponentModel;

namespace MyHome2013
{
    /// <summary>
    /// Provides the data on income and expenses for the selected month
    /// </summary>
    public partial class DataViewUI : Form
    {
        static Tuple<string, ListSortDirection>[] baseSorting = new[]
        {
            Tuple.Create("ID", ListSortDirection.Ascending),
            Tuple.Create("Date", ListSortDirection.Ascending)
        };

        SortableBindingList<Expense> expenseData = new SortableBindingList<Expense>(baseSorting);
        SortableBindingList<Income> incomeData = new SortableBindingList<Income>(baseSorting);

        #region Properties

        /// <summary>
        /// Holds the list of income categories
        /// </summary>
        public Dictionary<string, double> IncomeCategoriesTotals { get; set; }

        /// <summary>
        /// Holds the list of expense categories
        /// </summary>
        public Dictionary<string, double> ExpenseCategoriesTotals { get; set; }

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

            this.dgOut.DataSource = expenseData;
            this.dgOut.Columns["ID"].Visible = false;
            this.dgOut.Columns["Date"].DefaultCellStyle.Format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;

            this.dgIn.DataSource = incomeData;
            this.dgIn.Columns["ID"].Visible = false;
            this.dgIn.Columns["Date"].DefaultCellStyle.Format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;

            // Due to only the month being displayed on the control, the day is set to '1',
            // so when going from a month with more days to a month with less an exception won't be thrown
            // Note: this triggers ValueChanged event
            this.dtPick.Value = new DateTime(this.dtPick.Value.Year, this.dtPick.Value.Month, 1);

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
            var grid = sender as DataGridView;
            if (grid.CurrentCell == null) return;

            using (var form = new ExpenseViewer(grid.CurrentCell.OwningRow.DataBoundItem as Expense))
            {
                form.ShowDialog();
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
            var grid = sender as DataGridView;
            if (grid.CurrentCell == null) return;

            using (var form = new IncomeViewer(grid.CurrentCell.OwningRow.DataBoundItem as Income))
            {
                form.ShowDialog();
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
            this.cmbIncomeCategories.DataSource = null;
            this.txtIncomeCategoryTotal.DataBindings.Clear();
            this.cmbExpenseCategories.DataSource = null;
            this.txtExpenseCategoryTotal.DataBindings.Clear();

            // Intializes the category total dictionarys
            this.IncomeCategoriesTotals = new Dictionary<string, double>();
            this.ExpenseCategoriesTotals = new Dictionary<string, double>();

            this.ExpenseCategoriesTotals.Add("Total Expenses", ExpenseHandler.GetMonthTotal(dtPick.Value));
            this.ExpenseCategoriesTotals.AddRange(ExpenseHandler.GetAllCategoryTotals(this.dtPick.Value));

            this.IncomeCategoriesTotals.Add("Total Income", IncomeHandler.GetMonthTotal(dtPick.Value));
            this.IncomeCategoriesTotals.AddRange(IncomeHandler.GetAllCategoryTotals(this.dtPick.Value));

            // Sets the bindings for the controls
            this.cmbIncomeCategories.DataSource = new ArrayList(this.IncomeCategoriesTotals);
            this.cmbIncomeCategories.DisplayMember = "KEY";
            this.txtIncomeCategoryTotal.DataBindings.Add("Text", this.cmbIncomeCategories.DataSource, "VALUE");

            this.cmbExpenseCategories.DataSource = new ArrayList(this.ExpenseCategoriesTotals);
            this.cmbExpenseCategories.DisplayMember = "KEY";
            this.txtExpenseCategoryTotal.DataBindings.Add("Text", this.cmbExpenseCategories.DataSource, "VALUE");
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
            expenseData.Load(ExpenseHandler.LoadOfMonth(dtPick.Value));
            incomeData.Load(IncomeHandler.LoadOfMonth(dtPick.Value));
        }

        #endregion
    }
}
