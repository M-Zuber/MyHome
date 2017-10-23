using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MyHome.DataRepository;
using MyHome.Persistence;
using MyHome.Services;

namespace MyHome.UI
{
    /// <inheritdoc />
    /// <summary>
    ///     A pie chart view of the flow for the requested month
    ///     - split by income and expense and then within each side, by category
    /// </summary>
    public partial class MonthChartUI : Form
    {
        private readonly AccountingDataContext _dataContext;
        private readonly ExpenseService _expenseService;
        private readonly IncomeService _incomeService;

        // Data members
        private DateTime _dtMonth;

        /// <inheritdoc />
        /// <summary>
        ///     Ctor that also sets the data member of the month being viewed
        ///     with the value given
        /// </summary>
        /// <param name="dtMonth">The month to view data for</param>
        public MonthChartUI(DateTime dtMonth)
        {
            _dtMonth = dtMonth;
            InitializeComponent();

            _dataContext = new AccountingDataContext();
            _expenseService = new ExpenseService(new ExpenseRepository(_dataContext));
            _incomeService = new IncomeService(new IncomeRepository(_dataContext));
        }

        /// <summary>
        ///     Sets the value of the date selector to the month assigned in the ctor
        ///     and then loads the data onto the form
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void MonthChartUI_Load(object sender, EventArgs e)
        {
            // Sets the current value of the date selector to the value previously
            // assigned to the form
            dtPick.Value = _dtMonth;

            // Loads the data for the requested month and displays it on the form
            LoadMe();
        }

        /// <summary>
        ///     Resets the value of the data member representing the date and reloads the
        ///     data for the new date
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void DtPick_ValueChanged(object sender, EventArgs e)
        {
            // Sets the value of the date in the form to the value from the
            // date selector
            _dtMonth = dtPick.Value;

            // Loads the data for the requested month and displays it on the form
            LoadMe();
        }

        /// <summary>
        ///     Loads the data for the requested month and connects it to the form
        /// </summary>
        private void LoadMe()
        {
            // Updates the label to display the name of the month being viewed
            lblMonth.Text = _dtMonth.GetDateTimeFormats('Y')[0];

            // Connects the data of the expenses to the corresponding chart
            var expenseData = _expenseService.GetAllCategoryTotals(_dtMonth);
            crtExpenses.Series[0].Points.DataBind(expenseData, "KEY", "VALUE", "");
            UpdatePoints(crtExpenses.Series[0].Points);

            // Connects the data of the income to the corresponding chart
            var incomeData = _incomeService.GetAllCategoryTotals(_dtMonth);
            crtIncome.Series[0].Points.DataBind(incomeData, "KEY", "VALUE", "");
            UpdatePoints(crtIncome.Series[0].Points);
        }

        /// <summary>
        ///     Turns off the label on the chart of any data point that has no value to be displayed
        ///     -leaving the label in the legend
        /// </summary>
        /// <param name="dpcPointsToRefine">The data points collection to be refined</param>
        private void UpdatePoints(DataPointCollection dpcPointsToRefine)
        {
            // Goes over every data point in the collection given
            foreach (var currPoint in dpcPointsToRefine)
            {
                // If the value of the data point is nothing
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (currPoint.YValues[0] == 0.0)
                {
                    // Turns off the label that sits on top of the chart
                    currPoint.CustomProperties = "PieLabelStyle=Disabled";
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _dataContext?.Dispose();
        }
    }
}