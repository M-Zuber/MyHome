using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MyHome2013
{
    public partial class MultipleCategoriesCompare : Form
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
        /// Keeps track of the color of the current series being customized
        /// </summary>
        private Color CurrentSeriesColor { get; set; }

        /// <summary>
        /// The category names that can be displayed
        /// </summary>
        private List<string> CategoryNames { get; set; }

        /// <summary>
        /// Holds the data for each category by month in range being looked at
        /// </summary>
        private Dictionary<string, Dictionary<DateTime, decimal>> MonthData { get; set; }

        #endregion

        #region C'Tor

        public MultipleCategoriesCompare()
        {
            // Initialzation
            this.MonthData = new Dictionary<string, Dictionary<DateTime, decimal>>();
            this.CategoryNames = HelperMethods.GetAllCategoryNames();

            //Auto-generated code
            InitializeComponent();
        }

        #endregion

        #region Control Event Methods

        /// <summary>
        /// Setsup the data bindings on the form
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void PaymentMethodsOverTime_Load(object sender, EventArgs e)
        {
            // Sets the end date property to the date now
            this.EndDate = DateTime.Now;

            // The standard start date is a year before the end date
            this.StartDate = this.EndDate.Subtract(new TimeSpan(365, 0, 0, 0));

            // Sets the display of the date time pickers to the value of the corresponding property
            this.startDateValue.Value = this.StartDate;
            this.endDateValue.Value = this.EndDate;

            // Does nothing after setting the data bindings, due to the fact that no data is displayed
            // until the user specifically calls for it
            this.SetupDataBindings();
        }

        /// <summary>
        /// Lets the user change the display color of the selected series
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void chooseColorButton_Click(object sender, EventArgs e)
        {
            if (this.seriesNameValues.Items.Count > 0)
            {
                // The color picker opens with the current color already highlighted
                colorDialog1.Color = CurrentSeriesColor;
                colorDialog1.ShowDialog();
                CurrentSeriesColor = colorDialog1.Color;

                seriesColorValue.BackColor = CurrentSeriesColor;
                this.categoryData.Series[this.seriesNameValues.SelectedItem.ToString()].Color = CurrentSeriesColor; 
            }
        }

        /// <summary>
        /// Sets the property of the form with the value of the DateTime picker
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void startDateValue_ValueChanged(object sender, EventArgs e)
        {
            // Sets the end date with the new value
            this.StartDate = this.startDateValue.Value;
        }

        /// <summary>
        /// Sets the property of the form with the value of the DateTime picker
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void endDateValue_ValueChanged(object sender, EventArgs e)
        {
            // Sets the end date with the new value
            this.EndDate = this.endDateValue.Value;
        }

        /// <summary>
        /// If the series has a custom color, changes the indicator, otherwise sets it to transparent
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void seriesNameValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.categoryData.Series[this.seriesNameValues.SelectedItem.ToString()].Color != new Color())
            {
                CurrentSeriesColor = this.categoryData.Series[this.seriesNameValues.SelectedItem.ToString()].Color;
                seriesColorValue.BackColor = CurrentSeriesColor;
            }
            else
            {
                seriesColorValue.BackColor = new Color();
            }
        }

        /// <summary>
        /// Loads the combo box with the names of the series
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void seriesNameValues_Click(object sender, EventArgs e)
        {
            this.seriesNameValues.DataSource = this.categoryData.Series.Select(series => series.Name).ToList<string>();
        }

        /// <summary>
        /// Clears the chart from all data
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            this.categoryData.Series.Clear();
        }

        /// <summary>
        /// Display the data per the users selection
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard EventArgs object</param>
        private void calculateButton_Click(object sender, EventArgs e)
        {
            DialogResult recalculate = DialogResult.Yes;

            // If there is any data it checks that the user wnats to reset the chart
            if (this.categoryData.Series.Count > 0)
            {
                recalculate = MessageBox.Show("If you recalculate the data, the chart will be reset\n" +
                                              "To continue press yes",
                                              "Calculating...",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question); 
            }

            if (recalculate == DialogResult.Yes)
            {
                this.categoryData.Series.Clear();
                
                foreach (string curCategory in this.categoryPicker.CheckedItems)
                {
                    this.categoryData.Series.Add(curCategory);

                    this.categoryData.Series[curCategory].Legend = this.categoryData.Legends[0].Name;
                    this.categoryData.Series[curCategory].ChartType = SeriesChartType.Line;
                    this.categoryData.Series[curCategory].BorderWidth = 3;
                }

                CalculateData();
                DisplayData();
            }
        }

        #endregion

        #region Other Methods
        
        /// <summary>
        /// Sets up the data bindings of the DateTime pickers and the category picker
        /// </summary>
        private void SetupDataBindings()
        {
            this.startDateValue.DataBindings.Clear();
            this.endDateValue.DataBindings.Clear();
            this.categoryPicker.Items.Clear();

            this.categoryPicker.Items.AddRange(this.CategoryNames.ToArray<string>());

            // This prevents crossovers on the data time pickers
            this.startDateValue.DataBindings.Add("MaxDate", this.endDateValue, "Value");
            this.endDateValue.DataBindings.Add("MinDate", this.startDateValue, "Value");
        }

        /// <summary>
        /// Calculates the data for all the categories
        /// </summary>
        private void CalculateData()
        {
            MonthData.Clear();

            // Adds the category names as keys
            foreach (string curCategoryName in CategoryNames)
            {
                MonthData.Add(curCategoryName, new Dictionary<DateTime, decimal>());
            }

            // Gets a list with the data of the months in the range
            Dictionary<DateTime, Dictionary<string, decimal>> monthData = GetDataForMonthsInRange();

            int monthRange = MonthsRange();

            // Goes over each category getting the total for each month in the range being looked at
            foreach (KeyValuePair<string, Dictionary<DateTime, decimal>> curCategoryData in MonthData)
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
        /// Gets a dictionary of the totals of each payment method, per month in the range
        /// </summary>
        /// <returns>A dictionary keyed by date represented, value is the total of each payment method</returns>
        private Dictionary<DateTime, Dictionary<string, decimal>> GetDataForMonthsInRange()
        {
            var monthData = new Dictionary<DateTime, Dictionary<string, decimal>>();

            DateTime curDate = this.StartDate;
            for (int monthIndex = 0; monthIndex < MonthsRange(); monthIndex++)
            {
                var handler = Program.Container.GetInstance<DateTime, MonthHandler>(curDate);
                monthData.Add(curDate, handler.GetTotalsOfMonthByCategory());
                curDate = curDate.AddMonths(1);
            }

            return monthData;
        }

        /// <summary>
        /// Displays the data of all the series chosen by the user
        /// </summary>
        private void DisplayData()
        {
            // Shows the month in easy to read human format
            // -in the future a value can be passed into this method that will control the format
            // Because all the categories are for the same range it just takes the first one
            List<string> monthsStringRepresentation =
                this.MonthData[this.CategoryNames[0]].Keys.ToList<DateTime>()
                    .Select(curDate => curDate.ToString("MMM--yyyy")).ToList<string>();

            // Adds the data of each series currently being displayed
            foreach (var curSeries in this.categoryData.Series)
            {
                // Attaches the Month data to points collection of the series
                this.categoryData.Series[curSeries.Name].Points.DataBindXY(
                    monthsStringRepresentation,
                    this.MonthData[curSeries.Name].Values);
            }
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
