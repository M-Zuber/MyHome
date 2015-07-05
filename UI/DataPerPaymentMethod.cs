using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogic;
using System.Windows.Forms.DataVisualization.Charting;

namespace MyHome2013
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
            this.m_dtMonth = dtMonth;
            InitializeComponent();
        }

        #endregion

        private void DataPerPaymentMethod_Load(object sender, EventArgs e)
        {
            // Sets the current value of the date selector to the value previously
            // assigned to the form
            this.dtPick.Value = this.m_dtMonth;

            // Loads the data for the requested month and displays it on the form
            this.LoadMe();
        }

        private void dtPick_ValueChanged(object sender, EventArgs e)
        {
            // Sets the value of the date in the form to the value from the
            // date selector
            this.m_dtMonth = this.dtPick.Value;

            // Loads the data for the requested month and displays it on the form
            this.LoadMe();
        }

        #region Event Methods
        
        #endregion

        #region Other Methods

        /// <summary>
        /// Loads the data for the requested month and connects it to the form
        /// </summary>
        private void LoadMe()
        {
            // Updates the lable to display the name of the month being viewed
            this.lblMonth.Text = this.m_dtMonth.GetDateTimeFormats('Y')[0];

            // Connects the data of the expenses to the corrosponding chart
            Dictionary<string, double> expenseData = ExpenseService.GetAllPaymentMethodTotals(this.m_dtMonth);
            this.crtExpenses.Series[0].Points.DataBind(expenseData, "KEY", "VALUE", "");
            this.UpdatePoints(this.crtExpenses.Series[0].Points);

            // Connects the data of the income to the corrosponding chart
            Dictionary<string, double> incomeData = IncomeService.GetAllPaymentMethodTotals(this.m_dtMonth);
            this.crtIncome.Series[0].Points.DataBind(incomeData, "KEY", "VALUE", "");
            this.UpdatePoints(this.crtIncome.Series[0].Points);
        }

        /// <summary>
        /// Turns off the label on the chart of any data point that has no value to be displayed
        ///  -leaving the label in the legend
        /// </summary>
        /// <param name="dpcPointsToRefine">The data points collection to be refined</param>
        private void UpdatePoints(DataPointCollection dpcPointsToRefine)
        {
            // Goes over every data point in the collection given
            foreach (DataPoint CurrPoint in dpcPointsToRefine)
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
