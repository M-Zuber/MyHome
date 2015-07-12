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
        #region Data Members

        // Data members
        private DateTime m_dtMonth;

        #endregion

        #region C'Tor

        public DataPerPaymentMethod(DateTime dtMonth)
        {
            m_dtMonth = dtMonth;
            InitializeComponent();
        }

        #endregion

        private void DataPerPaymentMethod_Load(object sender, EventArgs e)
        {
            // Sets the current value of the date selector to the value previously
            // assigned to the form
            dtPick.Value = m_dtMonth;

            // Loads the data for the requested month and displays it on the form
            LoadMe();
        }

        private void dtPick_ValueChanged(object sender, EventArgs e)
        {
            // Sets the value of the date in the form to the value from the
            // date selector
            m_dtMonth = dtPick.Value;

            // Loads the data for the requested month and displays it on the form
            LoadMe();
        }

        #region Event Methods

        #endregion

        #region Other Methods

        /// <summary>
        ///     Loads the data for the requested month and connects it to the form
        /// </summary>
        private void LoadMe()
        {
            // Updates the lable to display the name of the month being viewed
            lblMonth.Text = m_dtMonth.GetDateTimeFormats('Y')[0];

            using (var context = new AccountingDataContext())
            {
                var expenseService = new ExpenseService(new ExpenseRepository(context));
                var incomeService = new ExpenseService(new ExpenseRepository(context));
                var expenseData = expenseService.GetAllPaymentMethodTotals(m_dtMonth);
                var incomeData = incomeService.GetAllPaymentMethodTotals(m_dtMonth);

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
        private void UpdatePoints(DataPointCollection dpcPointsToRefine)
        {
            // Goes over every data point in the collection given
            foreach (var CurrPoint in dpcPointsToRefine)
            {
                // If the value of the data point is nothing
                if (CurrPoint.YValues[0] == 0.0)
                {
                    // Turns off the label that sits on top of the chart
                    CurrPoint.CustomProperties = "PieLabelStyle=Disabled";
                }
            }
        }

        #endregion
    }
}