using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BusinessLogic;
using System.Collections.Generic;

namespace MyHome2013
{
    /// <summary>
    /// A pie chart view of the flow for the requested month
    /// - split by income and expense and then within each side, by category
    /// </summary>
    public partial class MonthChartUI : Form
    {
        #region Data Members
        
        // Data members
        private DateTime m_dtMonth;

        #endregion

        #region C'tor

        /// <summary>
        /// Ctor that also sets the data member of the month being viewed
        /// with the value given
        /// </summary>
        /// <param name="dtMonth">The month to view data for</param>
        public MonthChartUI(DateTime dtMonth)
        {
            this.m_dtMonth = dtMonth;
            InitializeComponent();
        }
        
        #endregion

        #region Control Event Methods

        /// <summary>
        /// Sets the value of the date selector to the month assigned in the ctor
        /// and then loads the data onto the form
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void MonthChartUI_Load(object sender, EventArgs e)
        {
            // Sets the current value of the date selector to the value previously
            // assigned to the form
            this.dtPick.Value = this.m_dtMonth;
            
            // Loads the data for the requested month and displays it on the form
            this.LoadMe();
        }

        /// <summary>
        /// Resets the value of the data member representing the date and reloads the
        /// data for the new date
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void dtPick_ValueChanged(object sender, EventArgs e)
        {
            // Sets the value of the date in the form to the value from the
            // date selector
            this.m_dtMonth = this.dtPick.Value;

            // Loads the data for the requested month and displays it on the form
            this.LoadMe();
        }
 
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
            Dictionary<string, double> expenseData = ExpenseHandler.GetCategoryTotals(this.m_dtMonth);
            expenseData.Remove("Total Expenses");
            this.crtExpenses.Series[0].Points.DataBind(expenseData, "KEY", "VALUE", "");
            this.UpdatePoints(this.crtExpenses.Series[0].Points);
            this.crtExpenses.ResetAutoValues();

            // Connects the data of the income to the corrosponding chart
            Dictionary<string, double> incomeData = IncomeHandler.GetCategoryTotals(this.m_dtMonth);
            incomeData.Remove("Total Income");
            this.crtIncome.Series[0].Points.DataBind(incomeData, "KEY", "VALUE", "");
            this.UpdatePoints(this.crtIncome.Series[0].Points);
            this.crtIncome.ResetAutoValues();
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
