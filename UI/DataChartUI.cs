using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyHome2013
{
    /// <summary>
    /// Provides data per category for the range of dates given
    /// -the default is the past year
    /// </summary>
    public partial class DataChartUI : Form
    {
        #region Properties

        /// <summary>
        /// The start date for the range of time data is being looked at
        /// </summary>
        private DateTime StartDate { get; set; }
        
        /// <summary>
        /// The end date for the range of time data is being looked at
        /// </summary>
        private DateTime EndDate { get; set; }

        /// <summary>
        /// Holds a list of all the category options that be can be looked at for the range
        /// </summary>
        private List<string> CategoryNames { get; set; }

        /// <summary>
        /// Holds the data for each category by month in range being looked at
        /// </summary>
        private Dictionary<string, Dictionary<DateTime, double>> MonthData { get; set; }

        #endregion

        #region C'tor

        /// <summary>
        /// Default Ctor - intializes the properies of the form
        /// </summary>
        public DataChartUI()
        {
            // Intializes the local properties of the form
            this.CategoryNames = new List<string>();
            this.MonthData = new Dictionary<string, Dictionary<DateTime, double>>();

            // Auto generated code for the form
            InitializeComponent();
        }

        #endregion

        #region Control Event Methods

        /// <summary>
        /// Sets up the infrastructure and connects the controls on the form 
        /// with the properties when the form loads
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event arg object</param>
        private void DataChartUI_Load(object sender, EventArgs e)
        {
            // Sets the end date property to the date now
            this.EndDate = DateTime.Now;

            // The standard start date is a year before the end date
            this.StartDate = this.EndDate.Subtract(new TimeSpan(365, 0, 0, 0));

            // Sets the display of the date time pickers to the value of the corresponding property
            this.dtpStartMonth.Value = this.StartDate;
            this.dtpEndMonth.Value = this.EndDate;

            // Gets the category names
            this.CategoryNames = GeneralCategoryHandler.GetAllCategoryNames();

            // Sets the data bindings of the controls -excluding the chart
            this.SetupDataBindings();
            
            // Gets the data for the current range selected
            this.SetupDataSource();

            // Connects the data to the chart
            this.ShowDataOnChart();
        }

        /// <summary>
        /// Reloads the data into the chart based on the currently selected category
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event arg object</param>
        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Resets the data bindings to reflect the change in the selected category
            this.ShowDataOnChart();
        }

        /// <summary>
        /// Reloads the data into the chart based on the date selected
        ///  -if there is a change in the date that affects the data
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void dtpEndMonth_ValueChanged(object sender, EventArgs e)
        {
            // Checks that either the month or the year was changed
            // if it was the day there is no reason to reload
            if ((this.EndDate.Month != this.dtpEndMonth.Value.Month) ||
                (this.EndDate.Year != this.dtpEndMonth.Value.Year))
            {
                // Sets the end date with the new value
                this.EndDate = this.dtpEndMonth.Value;

                // Refills the date sensitive data and reloads the chart
                this.SetupDataSource();
                this.ShowDataOnChart();
            }
        }

        /// <summary>
        /// Reloads the data into the chart based on the date selected
        ///  -if there is a change in the date that affects the data
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void dtpStartMonth_ValueChanged(object sender, EventArgs e)
        {
            // Checks that either the month or the year was changed
            // if it was the day there is no reason to reload
            if ((this.StartDate.Month != this.dtpStartMonth.Value.Month) ||
                (this.StartDate.Year != this.dtpStartMonth.Value.Year))
            {
                // Sets the end date with the new value
                this.StartDate = this.dtpStartMonth.Value;

                // Refills the date sensitive data and reloads the chart
                this.SetupDataSource();
                this.ShowDataOnChart();
            }
        }

        #endregion

        #region Other Methods

        /// <summary>
        /// Sets the static data bindings for the form controls
        /// </summary>
        private void SetupDataBindings()
        {
            this.cmbCat.DataSource = this.CategoryNames;

            // This prevents crossovers on the data time pickers
            this.dtpStartMonth.DataBindings.Add("MaxDate", this.dtpEndMonth, "Value");
            this.dtpEndMonth.DataBindings.Add("MinDate", this.dtpStartMonth, "Value");
        }

        /// <summary>
        /// Sets up the data source as a dictionary with the key being the category name
        ///  and value being a dictionary of totals keyed by date
        /// </summary>
        private void SetupDataSource()
        {
            MonthData.Clear();

            // Adds the category names as keys
            foreach (string curCategoryName in CategoryNames)
            {
                MonthData.Add(curCategoryName, new Dictionary<DateTime, double>());
            }

            // Gets a list with the data of the months in the range
            Dictionary<DateTime, Dictionary<string, double>> monthData = GetDataForMonthsInRange();

            int monthRange = MonthsRange();

            // Goes over each category getting the total for each month in the range being looked at
            foreach (KeyValuePair<string, Dictionary<DateTime, double>> curCategoryData in MonthData)
            {
                DateTime curDate = this.StartDate;

                // For each month in the range gets the total of the current category
                for (int monthIndex = 0; monthIndex < monthRange; monthIndex++)
                {
                    curCategoryData.Value.Add(curDate, monthData[curDate][curCategoryData.Key]);
                    curDate = curDate.AddMonths(1);
                }
            }
        }

        /// <summary>
        /// Gets a dictionary of the totals of each category, per month in the range
        /// </summary>
        /// <returns>A dictionary keyed by date represented, value is the total of each category</returns>
        private Dictionary<DateTime, Dictionary<string, double>> GetDataForMonthsInRange()
        {
            Dictionary<DateTime, Dictionary<string, double>> monthData = 
                new Dictionary<DateTime, Dictionary<string, double>>();

            DateTime curDate = this.StartDate;
            for (int monthIndex = 0; monthIndex < MonthsRange(); monthIndex++)
            {
                monthData.Add(curDate, (new MonthHandler(curDate)).GetTotalsOfMonthByCategory());
                curDate = curDate.AddMonths(1);
            }

            return monthData;
        }

        
        /// <summary>
        /// Sets the data bindings for the chart
        /// </summary>
        private void ShowDataOnChart()
        {
            // Shows the month in easy to read human format
            // -in the future a value can be passed into this method that will control the format
            List<string> monthsStringRepresentation = 
                this.MonthData[cmbCat.Text].Keys.ToList<DateTime>()
                    .Select(curDate => curDate.ToString("MMM--yyyy")).ToList<string>();

            // Attaches the Month data to points collection of the series
            this.crtGraph.Series[0].Points.DataBindXY(
                monthsStringRepresentation,
                this.MonthData[cmbCat.Text].Values);
            this.crtGraph.Series[0].Name =
                this.cmbCat.SelectedItem.ToString();
        }

        /// <summary>
        /// Calculates the months in the range from the start date to the end date
        /// </summary>
        /// <returns>The number of months in the range</returns>
        private int MonthsRange()
        {
            // Calculates the months in the range, taking the year into account
            // plus one so that if the dates are the same day, it will still return one
            return (((this.EndDate.Year - this.StartDate.Year) * 12) + 
                                        (this.EndDate.Month - this.StartDate.Month) + 1);
        }

        #endregion
    }
}
