using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MyHome.DataRepository;
using MyHome.Persistence;
using MyHome.Services;

namespace MyHome.UI
{
    public partial class DataPerPaymentMethod : Form
    {
        // Data members
        private DateTime _dtMonth;

        public DataPerPaymentMethod(DateTime dtMonth)
        {
            _dtMonth = dtMonth;
            InitializeComponent();
        }

        private void DataPerPaymentMethod_Load(object sender, EventArgs e)
        {
            // Sets the current value of the date selector to the value previously
            // assigned to the form
            dtPick.Value = _dtMonth;

            // Loads the data for the requested month and displays it on the form
            LoadMe();
        }

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

            using (var context = new AccountingDataContext())
            {
                var expenseService = new ExpenseService(new ExpenseRepository(context));
                var incomeService = new ExpenseService(new ExpenseRepository(context));
                var expenseData = expenseService.GetAllPaymentMethodTotals(_dtMonth);
                var incomeData = incomeService.GetAllPaymentMethodTotals(_dtMonth);

                crtExpenses.Series[0].Points.DataBind(expenseData, "KEY", "VALUE", "");
                UpdatePoints(crtExpenses.Series[0].Points);

                crtIncome.Series[0].Points.DataBind(incomeData, "KEY", "VALUE", "");
                UpdatePoints(crtIncome.Series[0].Points);
            }
        }

        /// <summary>
        ///     Turns off the label on the chart of any data point that has no value to be displayed
        ///     -leaving the label in the legend
        /// </summary>
        /// <param name="dpcPointsToRefine">The data points collection to be refined</param>
        private static void UpdatePoints(DataPointCollection dpcPointsToRefine)
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
    }
}