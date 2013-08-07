using System;
using FrameWork;
using System.Collections.Generic;

namespace BL
{
    /// <summary>
    /// Provides methods for viewing data on income and expenses per month
    /// </summary>
    public class MonthViewBL
    {
        #region Properties

        /// <summary>
        /// Provides public access to the income table
        /// </summary>
        public StaticDataSet.t_incomesDataTable IncomeTable
        {
            get; private set;
        }

        /// <summary>
        /// Provides public access to the expenses table
        /// </summary>
        public StaticDataSet.t_expensesDataTable ExpenceTable
        {
            get; private set;
        } 

        #endregion

        #region C'tor

        /// <summary>
        /// Loads the tables with the only the data for the month given
        /// </summary>
        /// <param name="dtmMonthToLoad">The month to load the data for</param>
        public MonthViewBL(DateTime dtmMonthToLoad)
        {
            // Sets the data members with the ctor
            this.IncomeTable = new StaticDataSet.t_incomesDataTable();
            this.ExpenceTable = new StaticDataSet.t_expensesDataTable();

            // Fills the table with data according to the month recieved
            this.ExpencesFilter(dtmMonthToLoad);
            this.IncomeFilter(dtmMonthToLoad);
        }

        #endregion

        #region Other Methods

        /// <summary>
        /// Provides a table with values for the sum total of income, expenses,
        ///  and a sum total within each expense category
        /// </summary>
        /// <returns>A basic int keyed data table filled with the values</returns>
        public StaticDataSet.categoryviewDataTable CuttingAll()
        {
            // Intializes the return variable
            StaticDataSet.categoryviewDataTable dtCutting =
                new StaticDataSet.categoryviewDataTable();

            // Starts the counter variable at zero to total the expenses for the month
            double dSum = 0;

            // Goes over each row in the table that contains the expenses for the month
            foreach (StaticDataSet.t_expensesRow drCurr in this.ExpenceTable)
            {
                // Adds the amount of the expense to the counter
                dSum += double.Parse(drCurr.AMOUNT.ToString());
            }

            // Creates a row to contain the data of the expenses total, and adds the data
            StaticDataSet.categoryviewRow drNew =
                dtCutting.NewcategoryviewRow();
            drNew[0] = "Total expenses";
            drNew[1] = dSum;

            // Adds the data to the return variable, through the row
            dtCutting.AddcategoryviewRow(drNew);

            // Resets the counter in order to sum up the income for the month
            dSum = 0;

            // Goes over each row in the table that contains the income for the month
            foreach (StaticDataSet.t_incomesRow drCurr in this.IncomeTable)
            {
                // Adds the amount of the income to the counter
                dSum += double.Parse(drCurr.AMOUNT.ToString());
            }

            // Resets the row with the data of the income total
            drNew = dtCutting.NewcategoryviewRow();
            drNew[0] = "Total income";
            drNew[1] = dSum;

            // Adds the data to the return variable, through the row
            dtCutting.AddcategoryviewRow(drNew);

            // Goes through each category in the expenses category table 
            // to get the sum of that category
            foreach (KeyValuePair<int, BL.ExpCatBL> CurrCat in ExpCatBL.GetAll())
            {
                // Resets the sum for each category
                dSum = 0;

                // Goes over each expense in the table with the data for the month
                foreach (StaticDataSet.t_expensesRow drCurrExp in this.ExpenceTable)
                {
                    // Checks if the current expense is in the category being summed now
                    if (drCurrExp.CATEGORY.ToString() == CurrCat.Value.ID.ToString())
                    {
                        // Adds the amount to the counter for this category
                        dSum += double.Parse(drCurrExp.AMOUNT.ToString());
                    }
                }

                // Resets the row with the total of the current category
                drNew = dtCutting.NewcategoryviewRow();
                drNew[0] = CurrCat.Value.Name.ToString();
                drNew[1] = dSum;

                // Adds the data to the return variable, through the row
                dtCutting.AddcategoryviewRow(drNew);
            }

            // Returns the filled table to the calling function
            return (dtCutting);
        }

        /// <summary>
        /// Gets a sum total of the expenses of the month -per category
        /// </summary>
        /// <returns>A basic int keyed data table with the values</returns>
        public StaticDataSet.categoryviewDataTable CuttingExp()
        {
            // Intializes the return variable
            StaticDataSet.categoryviewDataTable dtCutting =
                new StaticDataSet.categoryviewDataTable();

            // Goes through each category in the expenses category table 
            // to get the sum of that category
            foreach (KeyValuePair<int, BL.ExpCatBL> CurrCat in ExpCatBL.GetAll())
            {
                // Resets the sum for each category
                double dSum = 0;

                // Goes over each expense in the table with the data for the month
                foreach (StaticDataSet.t_expensesRow drCurrExp in this.ExpenceTable)
                {
                    // Checks if the current expense is in the category being summed now
                    if (drCurrExp.CATEGORY.ToString() == CurrCat.Value.ID.ToString())
                    {
                        // Adds the amount to the counter for this category
                        dSum += double.Parse(drCurrExp.AMOUNT.ToString());
                    }
                }

                // Creates a row to contain the data of the total of the current category,
                // and adds the data
                StaticDataSet.categoryviewRow drNew =
                dtCutting.NewcategoryviewRow();
                drNew[0] = CurrCat.Value.Name.ToString();
                drNew[1] = dSum;

                // Adds the data to the return variable, through the row
                dtCutting.AddcategoryviewRow(drNew);
            }

            // Returns the filled table to the calling function
            return (dtCutting);
        }

        /// <summary>
        /// Gets a sum total of the income of the month -per category
        /// </summary>
        /// <returns>A basic int keyed data table with the values</returns>
        public StaticDataSet.categoryviewDataTable CuttingInc()
        {
            // Intializes the return variable
            StaticDataSet.categoryviewDataTable dtCutting =
                new StaticDataSet.categoryviewDataTable();

            // Goes through each category in the income category table 
            // to get the sum of that category
            foreach (StaticDataSet.t_incomes_categoryRow drCurrCat in Cache.SDB.t_incomes_category)
            {
                // Resets the sum for each category
                double dSum = 0;

                // Goes over each income in the table with the data for the month
                foreach (StaticDataSet.t_incomesRow drCurrExp in this.IncomeTable)
                {
                    // Checks if the current income is in the category being summed now
                    if (drCurrExp.CATEGORY.ToString() == drCurrCat.ID.ToString())
                    {
                        // Adds the amount to the counter for this category
                        dSum += double.Parse(drCurrExp.AMOUNT.ToString());
                    }
                }

                // Creates a row to contain the data of the total of the current category,
                // and adds the data
                StaticDataSet.categoryviewRow drNew =
                dtCutting.NewcategoryviewRow();
                drNew[0] = drCurrCat.NAME.ToString();
                drNew[1] = dSum;

                // Adds the data to the return variable, through the row
                dtCutting.AddcategoryviewRow(drNew);
            }

            // Returns the filled table to the calling function
            return (dtCutting);
        }

        /// <summary>
        /// Filters the expenses table in the cache into the local table
        /// based on the month given
        /// </summary>
        /// <param name="dtmMonthToLoad">The month to filter the table by</param>
        private void ExpencesFilter(DateTime dtmMonthToLoad)
        {
            // Fills in the local table with the table for the requested month
            foreach (StaticDataSet.t_expensesRow CurrRow in Cache.SDB.t_expenses)
            {
                // Checks that the current row is from the wanted month
                // and the wanted year
                if ((CurrRow.EXP_DATE.Month == dtmMonthToLoad.Month) && 
                    (CurrRow.EXP_DATE.Year == dtmMonthToLoad.Year))
                {
                    this.ExpenceTable.ImportRow(CurrRow);
                }
            }
        }

        /// <summary>
        /// Filters the income table in the cache into the local table
        /// based on the month given
        /// </summary>
        /// <param name="dtmMonthToLoad">The month to filter the table by</param>
        private void IncomeFilter(DateTime dtmMonthToLoad)
        {
            // Fills in the local table with the table for the requested month
            foreach (StaticDataSet.t_incomesRow CurrRow in Cache.SDB.t_incomes)
            {
                // Checks that the current row is from the wanted month
                // and the wanted year
                if ((CurrRow.INC_DATE.Month == dtmMonthToLoad.Month) &&
                    (CurrRow.INC_DATE.Year == dtmMonthToLoad.Year))
                {
                    this.IncomeTable.ImportRow(CurrRow);
                }
            }
        } 

        #endregion
    }
}
