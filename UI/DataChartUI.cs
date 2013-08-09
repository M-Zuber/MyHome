using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using BL;
using FrameWork;

namespace MyHome2013
{
    /// <summary>
    /// Provides data per category for the last year
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
        /// Holds entity repesentation of each month in the range being looked at
        /// </summary>
        private ArrayList AllMonths { get; set; }

        /// <summary>
        /// Holds a list of all the category options that be can be looked at for the range
        /// </summary>
        private ArrayList AllCategoryOptions { get; set; }

        /// <summary>
        /// Holds the data of each month being looked at
        /// </summary>
        private DataTable YearData { get; set; }

        #endregion

        #region C'tor

        /// <summary>
        /// Default Ctor - intializes the properies of the form
        /// </summary>
        public DataChartUI()
        {
            // Intializes the local properties of the form
            this.AllMonths = new ArrayList();
            this.AllCategoryOptions = new ArrayList();
            this.YearData = new DataTable();

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
            
            // Defines the table, and fills in the array list of category options
            this.IntialSetup();

            // Sets up the table and array list with the rows needed to hold the data
            this.SecondarySetup();

            // Connects the combo box with the list of categories
            this.cmbCat.DataSource = this.AllCategoryOptions;

            // Loads the data into the local data table
            // based on the current date -per the default selected 
            // category in the combo box
            this.LoadMe();

            // Sets the display of the start time to the value of the stat time property
            this.dtpStartMonth.Value = this.StartDate;
        }

        /// <summary>
        /// Reloads the data into the chart based on the currently selected category
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event arg object</param>
        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Resets the data bindings to reflect the change in the selected category
            this.SetDataBindings();
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
                
                // Clears the date sensitive data for the new data
                this.AllMonths.Clear();
                this.YearData.Clear();

                // Refills the date sensitive data and reloads the chart
                this.SecondarySetup();
                this.LoadMe();
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

                // Clears the date sensitive data for the new data
                this.AllMonths.Clear();
                this.YearData.Clear();

                // Refills the date sensitive data and reloads the chart
                this.SecondarySetup();
                this.LoadMe();
            }
        }

        #endregion

        #region Other Methods

        /// <summary>
        /// Sets the chart on the form with the data -based on the selected category
        /// </summary>
        /// <param name="nNum">UnKnown</param>
        /// <param name="dtStart">The date until which to display data on the chart</param>
        private void LoadMe()
        {
            // An index variable to reach the row that corrosponds to the month currently being worked on
            int nIndex = 0;
            
            // Goes over every Month view in the local array list
            foreach (MonthViewBL mvcurr in this.AllMonths)
            {
                // Pulls up a table with the sum totals of the income, expense,
                // and within each expense category for the month
                DataTable dtCat = mvcurr.CuttingAll();

                // Goes over every row in the table with the sum totals
                // and adds the total to the corresponding column 
                foreach (DataRow drCurr in dtCat.Rows)
                {
                    this.YearData.Rows[nIndex][drCurr["KEY"].ToString()] =
                        drCurr["VALUE"];
                }

                // Pulls up the table with the sum total of each income category
                dtCat = mvcurr.CuttingInc();

                // Goes over every row in the table with the sum totals
                // and adds the total to the corresponding column 
                foreach (DataRow drCurr in dtCat.Rows)
                {
                    this.YearData.Rows[nIndex][drCurr["KEY"].ToString()] =
                        drCurr["VALUE"];
                }

                // Raises the index for the next months data
                nIndex++;      
            }

            // Flips the data table so that it will display from left to right (older to newer)
            this.YearData = this.FlipTable(this.YearData);

            // Sets the data bindings of the chart
            this.SetDataBindings();
        }

        /// <summary>
        /// Sets the data bindings for the chart
        /// </summary>
        private void SetDataBindings()
        {
            // Attaches the local data table to the chart on the form
            // and sets up the axis
            this.crtGraph.DataSource = this.YearData;
            this.crtGraph.Series[0].YValueMembers =
                this.cmbCat.SelectedItem.ToString();
            this.crtGraph.Series[0].XValueMember = "Month";
            this.crtGraph.Series[0].Name =
                this.cmbCat.SelectedItem.ToString();
            this.crtGraph.ResetAutoValues();
        }

        /// <summary>
        /// Sets up the data table and the aray list with the framework needed to hold the data
        /// for the time period requested
        /// </summary>
        /// <param name="nNum">the number of months being shown</param>
        /// <param name="dtStart">The date to start countng backwards from</param>
        private void SecondarySetup()
        {
            // Sets variables with the starting month and year
            // and gets the amount of months in the range
            int nMonth = this.EndDate.Month;
            int nYear = this.EndDate.Year;
            int nMonthsInRange = this.MonthsRange();

            // Loops once for each month of the year -setting up the data for the chart 
            // in the process
            for (int nMonthIndex = 0; nMonthIndex < nMonthsInRange; nMonthIndex++)
            {
                // Creates a new dat row to be added to the table
                DataRow drNew = this.YearData.NewRow();

                // Creates a variable with the date of the data currently being entered onto the
                // chart
                DateTime dtCurr = new DateTime(nYear, nMonth, 1);

                // Adds a cut of the data for the month given to the local array list
                this.AllMonths.Add(new MonthViewBL(dtCurr));

                // Adds the months number to the new row
                drNew["Month"] = dtCurr.GetDateTimeFormats('Y')[0];

                // Adds the new row into the local data table
                this.YearData.Rows.Add(drNew);

                // Moves to the previous month
                nMonth--;

                // If the variable for the month has reached zero
                // -the end of the calender year has been reached and the year has to be changed
                if (nMonth == 0)
                {
                    nMonth = 12;
                    nYear--;
                }
            }
        }

        /// <summary>
        /// Defines the local data table with the columns neccesary, and fills up 
        /// the array list of category options
        /// </summary>
        private void IntialSetup()
        {
            // Adds columns to the local data table for the months name and total expense and income
            this.YearData.Columns.Add("Month");
            this.YearData.Columns.Add("Total expenses");
            this.YearData.Columns.Add("Total income");

            // Adds items for total income and expenses to the local array list
            this.AllCategoryOptions.Add("Total expenses");
            this.AllCategoryOptions.Add("Total income");

            // Goes over every expense category in the database
            foreach (KeyValuePair<int, BL.ExpCatBL> CurrCat in ExpCatBL.GetAll())
            {
                // Adds a column to the local table and an item to the local array list
                // representing the current expense category
                this.YearData.Columns.Add(CurrCat.Value.Name);
                this.AllCategoryOptions.Add(CurrCat.Value.Name);
            }

            // Goes over every income category in the database
            foreach (StaticDataSet.t_incomes_categoryRow drCurrCat in Cache.SDB.t_incomes_category)
            {
                // Adds a column to the local table and an item to the local array list
                // representing the current income category
                this.YearData.Columns.Add(drCurrCat.NAME);
                this.AllCategoryOptions.Add(drCurrCat.NAME);
            }
        }
        
        /// <summary>
        /// Turns the data table given upside down
        /// </summary>
        /// <param name="dtSource">The table to be flipped</param>
        /// <returns>The table after the flipping</returns>
        private DataTable FlipTable(DataTable dtSource)
        {
            // Copies the structure of the table onto the return variable
            DataTable dtReturn = dtSource.Clone();

            // Goes over the from the bottom to the top
            for (int nRowIndex = dtSource.Rows.Count; nRowIndex > 0; nRowIndex--)
            {
                // Imports the row from the source into the return variable
                dtReturn.ImportRow(dtSource.Rows[nRowIndex - 1]);
            }

            // Returns the flipped table
            return (dtReturn);
        }

        /// <summary>
        /// Calculates the months in the range from the start date to the end date
        /// </summary>
        /// <returns>The number of months in the range</returns>
        private int MonthsRange()
        {
            // Calculates the months in the range, taking the year into account
            return (((this.EndDate.Year - this.StartDate.Year) * 12) + 
                                        (this.EndDate.Month - this.StartDate.Month) + 1);
        }

        #endregion       
    }
}
