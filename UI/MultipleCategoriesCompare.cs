using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BusinessLogic;
using Data;
using DataAccess;

namespace MyHome2013
{
    public partial class MultipleCategoriesCompare : Form
    {
        private readonly AccountingDataContext _dataContext;
        private readonly GeneralCategoryHandler _generalCategoryHandler;

        #region C'Tor

        public MultipleCategoriesCompare()
        {
            InitializeComponent();

            MonthData = new Dictionary<string, Dictionary<DateTime, decimal>>();
            _dataContext = new AccountingDataContext();
            _generalCategoryHandler = new GeneralCategoryHandler(_dataContext);
            CategoryNames = _generalCategoryHandler.GetAllCategoryNames().ToList();
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
        ///     Keeps track of the color of the current series being customized
        /// </summary>
        private Color CurrentSeriesColor { get; set; }

        /// <summary>
        ///     The category names that can be displayed
        /// </summary>
        private List<string> CategoryNames { get; }

        /// <summary>
        ///     Holds the data for each category by month in range being looked at
        /// </summary>
        private Dictionary<string, Dictionary<DateTime, decimal>> MonthData { get; }

        #endregion

        #region Control Event Methods

        /// <summary>
        ///     Setsup the data bindings on the form
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void PaymentMethodsOverTime_Load(object sender, EventArgs e)
        {
            // Sets the end date property to the date now
            EndDate = DateTime.Now;

            // The standard start date is a year before the end date
            StartDate = EndDate.Subtract(new TimeSpan(365, 0, 0, 0));

            // Sets the display of the date time pickers to the value of the corresponding property
            startDateValue.Value = StartDate;
            endDateValue.Value = EndDate;

            // Does nothing after setting the data bindings, due to the fact that no data is displayed
            // until the user specifically calls for it
            SetupDataBindings();
        }

        /// <summary>
        ///     Lets the user change the display color of the selected series
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void chooseColorButton_Click(object sender, EventArgs e)
        {
            if (seriesNameValues.Items.Count > 0)
            {
                // The color picker opens with the current color already highlighted
                colorDialog1.Color = CurrentSeriesColor;
                colorDialog1.ShowDialog();
                CurrentSeriesColor = colorDialog1.Color;

                seriesColorValue.BackColor = CurrentSeriesColor;
                categoryData.Series[seriesNameValues.SelectedItem.ToString()].Color = CurrentSeriesColor;
            }
        }

        /// <summary>
        ///     Sets the property of the form with the value of the DateTime picker
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void startDateValue_ValueChanged(object sender, EventArgs e)
        {
            // Sets the end date with the new value
            StartDate = startDateValue.Value;
        }

        /// <summary>
        ///     Sets the property of the form with the value of the DateTime picker
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void endDateValue_ValueChanged(object sender, EventArgs e)
        {
            // Sets the end date with the new value
            EndDate = endDateValue.Value;
        }

        /// <summary>
        ///     If the series has a custom color, changes the indicator, otherwise sets it to transparent
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void seriesNameValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryData.Series[seriesNameValues.SelectedItem.ToString()].Color != new Color())
            {
                CurrentSeriesColor = categoryData.Series[seriesNameValues.SelectedItem.ToString()].Color;
                seriesColorValue.BackColor = CurrentSeriesColor;
            }
            else
            {
                seriesColorValue.BackColor = new Color();
            }
        }

        /// <summary>
        ///     Loads the combo box with the names of the series
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void seriesNameValues_Click(object sender, EventArgs e)
        {
            seriesNameValues.DataSource = categoryData.Series.Select(series => series.Name).ToList();
        }

        /// <summary>
        ///     Clears the chart from all data
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            categoryData.Series.Clear();
        }

        /// <summary>
        ///     Display the data per the users selection
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void calculateButton_Click(object sender, EventArgs e)
        {
            var recalculate = DialogResult.Yes;

            // If there is any data it checks that the user wnats to reset the chart
            if (categoryData.Series.Count > 0)
            {
                recalculate = MessageBox.Show("If you recalculate the data, the chart will be reset\n" +
                                              "To continue press yes",
                    "Calculating...",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
            }

            if (recalculate == DialogResult.Yes)
            {
                categoryData.Series.Clear();

                foreach (string curCategory in categoryPicker.CheckedItems)
                {
                    categoryData.Series.Add(curCategory);

                    categoryData.Series[curCategory].Legend = categoryData.Legends[0].Name;
                    categoryData.Series[curCategory].ChartType = SeriesChartType.Line;
                    categoryData.Series[curCategory].BorderWidth = 3;
                }

                CalculateData();
                DisplayData();
            }
        }

        #endregion

        #region Other Methods

        /// <summary>
        ///     Sets up the data bindings of the DateTime pickers and the category picker
        /// </summary>
        private void SetupDataBindings()
        {
            startDateValue.DataBindings.Clear();
            endDateValue.DataBindings.Clear();
            categoryPicker.Items.Clear();

            categoryPicker.Items.AddRange(CategoryNames.ToArray<string>());

            // This prevents crossovers on the data time pickers
            startDateValue.DataBindings.Add("MaxDate", endDateValue, "Value");
            endDateValue.DataBindings.Add("MinDate", startDateValue, "Value");
        }

        /// <summary>
        ///     Calculates the data for all the categories
        /// </summary>
        private void CalculateData()
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
            foreach (KeyValuePair<string, Dictionary<DateTime, decimal>> curCategoryData in MonthData)
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
        ///     Gets a dictionary of the totals of each payment method, per month in the range
        /// </summary>
        /// <returns>A dictionary keyed by date represented, value is the total of each payment method</returns>
        private Dictionary<DateTime, Dictionary<string, decimal>> GetDataForMonthsInRange()
        {
            var monthData =
                new Dictionary<DateTime, Dictionary<string, decimal>>();

            var curDate = StartDate;
            for (var monthIndex = 0; monthIndex < MonthsRange(); monthIndex++)
            {
                monthData.Add(curDate, (new MonthHandler(curDate)).GetTotalsOfMonthByCategory());
                curDate = curDate.AddMonths(1);
            }

            return monthData;
        }

        /// <summary>
        ///     Displays the data of all the series chosen by the user
        /// </summary>
        private void DisplayData()
        {
            // Shows the month in easy to read human format
            // -in the future a value can be passed into this method that will control the format
            // Because all the categories are for the same range it just takes the first one
            var monthsStringRepresentation =
                MonthData[CategoryNames[0]].Keys.ToList()
                    .Select(curDate => curDate.ToString("MMM--yyyy")).ToList();

            // Adds the data of each series currently being displayed
            foreach (var curSeries in categoryData.Series)
            {
                // Attaches the Month data to points collection of the series
                categoryData.Series[curSeries.Name].Points.DataBindXY(
                    monthsStringRepresentation,
                    MonthData[curSeries.Name].Values);
            }
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
            _dataContext?.Dispose();
        }

        #endregion
    }
}