using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MyHome.DataRepository;
using MyHome.Persistence;
using MyHome.Services;

namespace MyHome.UI
{
    /// <summary>
    ///     Provides data per category for the range of dates given
    ///     -the default is the past year
    /// </summary>
    public partial class DataChartUI : Form
    {
        private readonly AccountingDataContext _dataContext;
        private readonly GeneralCategoryHandler _generalCategoryHandler;
        private readonly MonthService _monthService;

        #region C'tor

        /// <summary>
        ///     Default Ctor - intializes the properies of the form
        /// </summary>
        public DataChartUI()
        {
            // Intializes the local properties of the form
            CategoryNames = new List<string>();
            MonthData = new Dictionary<string, Dictionary<DateTime, decimal>>();

            // Auto generated code for the form
            InitializeComponent();

            _dataContext = new AccountingDataContext();
            _generalCategoryHandler = new GeneralCategoryHandler(_dataContext);
            _monthService = new MonthService(_dataContext);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The start date for the range of time data is being looked at
        /// </summary>
        private DateTime StartDate { get; set; }

        /// <summary>
        ///     The end date for the range of time data is being looked at
        /// </summary>
        private DateTime EndDate { get; set; }

        /// <summary>
        ///     Holds a list of all the category options that be can be looked at for the range
        /// </summary>
        private List<string> CategoryNames { get; set; }

        /// <summary>
        ///     Holds the data for each category by month in range being looked at
        /// </summary>
        private Dictionary<string, Dictionary<DateTime, decimal>> MonthData { get; set; }

        #endregion

        #region Control Event Methods

        /// <summary>
        ///     Sets up the infrastructure and connects the controls on the form
        ///     with the properties when the form loads
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event arg object</param>
        private void DataChartUI_Load(object sender, EventArgs e)
        {
            // Sets the end date property to the date now
            EndDate = DateTime.Now;

            // The standard start date is a year before the end date
            StartDate = EndDate.Subtract(new TimeSpan(365, 0, 0, 0));

            // Sets the display of the date time pickers to the value of the corresponding property
            dtpStartMonth.Value = StartDate;
            dtpEndMonth.Value = EndDate;

            // Gets the category names
            CategoryNames = _generalCategoryHandler.GetAllCategoryNames().ToList();

            // Sets the data bindings of the controls -excluding the chart
            SetupDataBindings();

            // Gets the data for the current range selected
            SetupDataSource();

            // Connects the data to the chart
            ShowDataOnChart();
        }

        /// <summary>
        ///     Reloads the data into the chart based on the currently selected category
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event arg object</param>
        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Resets the data bindings to reflect the change in the selected category
            ShowDataOnChart();
        }

        /// <summary>
        ///     Reloads the data into the chart based on the date selected
        ///     -if there is a change in the date that affects the data
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void dtpEndMonth_ValueChanged(object sender, EventArgs e)
        {
            // Checks that either the month or the year was changed
            // if it was the day there is no reason to reload
            if ((EndDate.Month != dtpEndMonth.Value.Month) ||
                (EndDate.Year != dtpEndMonth.Value.Year))
            {
                // Sets the end date with the new value
                EndDate = dtpEndMonth.Value;

                // Refills the date sensitive data and reloads the chart
                SetupDataSource();
                ShowDataOnChart();
            }
        }

        /// <summary>
        ///     Reloads the data into the chart based on the date selected
        ///     -if there is a change in the date that affects the data
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void dtpStartMonth_ValueChanged(object sender, EventArgs e)
        {
            // Checks that either the month or the year was changed
            // if it was the day there is no reason to reload
            if ((StartDate.Month != dtpStartMonth.Value.Month) ||
                (StartDate.Year != dtpStartMonth.Value.Year))
            {
                // Sets the end date with the new value
                StartDate = dtpStartMonth.Value;

                // Refills the date sensitive data and reloads the chart
                SetupDataSource();
                ShowDataOnChart();
            }
        }

        #endregion

        #region Other Methods

        /// <summary>
        ///     Sets the static data bindings for the form controls
        /// </summary>
        private void SetupDataBindings()
        {
            cmbCat.DataSource = CategoryNames;

            // This prevents crossovers on the data time pickers
            dtpStartMonth.DataBindings.Add("MaxDate", dtpEndMonth, "Value");
            dtpEndMonth.DataBindings.Add("MinDate", dtpStartMonth, "Value");
        }

        /// <summary>
        ///     Sets up the data source as a dictionary with the key being the category name
        ///     and value being a dictionary of totals keyed by date
        /// </summary>
        private void SetupDataSource()
        {
            MonthData.Clear();

            // Adds the category names as keys
            foreach (var curCategoryName in CategoryNames)
            {
                MonthData.Add(curCategoryName, new Dictionary<DateTime, decimal>());
            }

            // Gets a list with the data of the months in the range
            var monthData = GetDataForMonthsInRange();

            var monthRange = MonthsRange();

            // Goes over each category getting the total for each month in the range being looked at
            foreach (var curCategoryData in MonthData)
            {
                var curDate = StartDate;

                // For each month in the range gets the total of the current category
                for (var monthIndex = 0; monthIndex < monthRange; monthIndex++)
                {
                    curCategoryData.Value.Add(curDate, monthData[curDate][curCategoryData.Key]);
                    curDate = curDate.AddMonths(1);
                }
            }
        }

        /// <summary>
        ///     Gets a dictionary of the totals of each category, per month in the range
        /// </summary>
        /// <returns>A dictionary keyed by date represented, value is the total of each category</returns>
        private Dictionary<DateTime, Dictionary<string, decimal>> GetDataForMonthsInRange()
        {
            var monthData = new Dictionary<DateTime, Dictionary<string, decimal>>();

            var curDate = StartDate;
            for (var monthIndex = 0; monthIndex < MonthsRange(); monthIndex++)
            {
                monthData.Add(curDate, _monthService.GetTotalFlowPerCategoriesForMonth(curDate));
                curDate = curDate.AddMonths(1);
            }

            return monthData;
        }

        /// <summary>
        ///     Sets the data bindings for the chart
        /// </summary>
        private void ShowDataOnChart()
        {
            // Shows the month in easy to read human format
            // -in the future a value can be passed into this method that will control the format
            var monthsStringRepresentation =
                MonthData[cmbCat.Text].Keys.ToList()
                    .Select(curDate => curDate.ToString("MMM--yyyy")).ToList();

            // Attaches the Month data to points collection of the series
            crtGraph.Series[0].Points.DataBindXY(
                monthsStringRepresentation,
                MonthData[cmbCat.Text].Values);
            crtGraph.Series[0].Name =
                cmbCat.SelectedItem.ToString();
        }

        /// <summary>
        ///     Calculates the months in the range from the start date to the end date
        /// </summary>
        /// <returns>The number of months in the range</returns>
        private int MonthsRange()
        {
            // Calculates the months in the range, taking the year into account
            // plus one so that if the dates are the same day, it will still return one
            return (((EndDate.Year - StartDate.Year)*12) +
                    (EndDate.Month - StartDate.Month) + 1);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (_dataContext != null)
            {
                _dataContext.Dispose();    
            }
            
        }

        #endregion
    }
}