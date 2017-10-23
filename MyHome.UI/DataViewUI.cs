using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using MyHome.DataClasses;
using MyHome.DataRepository;
using MyHome.Infrastructure;
using MyHome.Persistence;
using MyHome.Services;

namespace MyHome.UI
{
    /// <inheritdoc />
    /// <summary>
    ///     Provides the data on income and expenses for the selected month
    /// </summary>
    public partial class DataViewUI : Form
    {
        private static readonly (string, ListSortDirection)[] BaseSorting =
        {
            ("Id", ListSortDirection.Ascending),
            ("Date", ListSortDirection.Ascending)
        };

        private readonly AccountingDataContext _dataContext;
        private readonly ExpenseService _expenseService;
        private readonly IncomeService _incomeService;
        private readonly SortableBindingList<Expense> _expenseData = new SortableBindingList<Expense>(BaseSorting);
        private readonly SortableBindingList<Income> _incomeData = new SortableBindingList<Income>(BaseSorting);

        public DataViewUI()
        {
            InitializeComponent();

            _dataContext = new AccountingDataContext();
            _expenseService = new ExpenseService(new ExpenseRepository(_dataContext));
            _incomeService = new IncomeService(new IncomeRepository(_dataContext));
        }

        /// <summary>
        ///     Holds the list of income categories
        /// </summary>
        public Dictionary<string, decimal> IncomeCategoriesTotals { get; set; }

        /// <summary>
        ///     Holds the list of expense categories
        /// </summary>
        public Dictionary<string, decimal> ExpenseCategoriesTotals { get; set; }

        /// <summary>
        ///     Connects the controls on the form with the data from the cache
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void DataViewUI_Load(object sender, EventArgs e)
        {
            // Automatically forces the window to be open to its max size
            WindowState = FormWindowState.Maximized;

            dgOut.DataSource = _expenseData;
            // ReSharper disable PossibleNullReferenceException
            dgOut.Columns["ID"].Visible = false;
            dgOut.Columns["Date"].DefaultCellStyle.Format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;

            dgIn.DataSource = _incomeData;
            dgIn.Columns["ID"].Visible = false;
            dgIn.Columns["Date"].DefaultCellStyle.Format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
            // ReSharper restore PossibleNullReferenceException

            // Due to only the month being displayed on the control, the day is set to '1',
            // so when going from a month with more days to a month with less an exception won't be thrown
            // Note: this triggers ValueChanged event
            dtPick.Value = new DateTime(dtPick.Value.Year, dtPick.Value.Month, 1);

            // Sets up the event for re-entering the form
            Enter += DataViewUI_Enter;
        }

        /// <summary>
        ///     Resets the data in the form based on the users date choice
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void DtPick_ValueChanged(object sender, EventArgs e)
        {
            DataBinding();
        }

        /// <summary>
        ///     Opens a viewer to edit the expense clicked on
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">standard MouseEvent object</param>
        private void DgOut_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid?.CurrentCell == null) return;

            using (var form = new ExpenseViewer(grid.CurrentCell.OwningRow.DataBoundItem as Expense))
            {
                form.ShowDialog();
            }

            DataBinding();
        }

        /// <summary>
        ///     Opens a viewer to edit the income clicked on
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">standard MouseEvent object</param>
        private void DgIn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid?.CurrentCell == null) return;

            using (var form = new IncomeViewer(grid.CurrentCell.OwningRow.DataBoundItem as Income))
            {
                form.ShowDialog();
            }

            DataBinding();
        }


        /// <summary>
        ///     Refreshes the data in the form every time it gains focus
        ///     -This is to deal with multiple views into the same month being open
        ///     and data being changed in a single one of them
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void DataViewUI_Enter(object sender, EventArgs e)
        {
            DataBinding();
        }

        /// <summary>
        ///     Sets up the data bindings to the combo box and the text box
        ///     Refreshes also -by deleting and rebinding
        /// </summary>
        private void CategryDataBinding()
        {
            // Clears any old data bindings
            cmbIncomeCategories.DataSource = null;
            txtIncomeCategoryTotal.DataBindings.Clear();
            cmbExpenseCategories.DataSource = null;
            txtExpenseCategoryTotal.DataBindings.Clear();

            // Initializes the category total dictionaries
            ExpenseCategoriesTotals = new Dictionary<string, decimal> {{"Total Expenses", _expenseService.GetMonthTotal(dtPick.Value)}};
            ExpenseCategoriesTotals.AddRange(_expenseService.GetAllCategoryTotals(dtPick.Value));

            IncomeCategoriesTotals = new Dictionary<string, decimal> {{"Total Income", _incomeService.GetMonthTotal(dtPick.Value)}};
            IncomeCategoriesTotals.AddRange(_incomeService.GetAllCategoryTotals(dtPick.Value));


            // Sets the bindings for the controls
            cmbIncomeCategories.DataSource = new ArrayList(IncomeCategoriesTotals);
            cmbIncomeCategories.DisplayMember = "KEY";
            txtIncomeCategoryTotal.DataBindings.Add("Text", cmbIncomeCategories.DataSource, "VALUE");

            cmbExpenseCategories.DataSource = new ArrayList(ExpenseCategoriesTotals);
            cmbExpenseCategories.DisplayMember = "KEY";
            txtExpenseCategoryTotal.DataBindings.Add("Text", cmbExpenseCategories.DataSource, "VALUE");
        }

        /// <summary>
        ///     Sets up the data bindings for the form
        /// </summary>
        private void DataBinding()
        {
            MonthlyDataBinding();
            CategryDataBinding();
        }

        /// <summary>
        ///     Sets up the data bindings for the expense and income charts
        /// </summary>
        private void MonthlyDataBinding()
        {
            _expenseData.Load(_expenseService.LoadOfMonth(dtPick.Value));
            _incomeData.Load(_incomeService.LoadOfMonth(dtPick.Value));
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _dataContext?.Dispose();
        }
    }
}