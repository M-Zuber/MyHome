using System;
namespace Data
{
    
    
    public partial class StaticDataSet {
        partial class viwDataTable
        {
            /// <summary>
            /// Filters the view by the date given -based on the month and the year
            /// </summary>
            /// <param name="dtDateUntil">The month being filtered for</param>
            /// <returns>A filtered view</returns>
            public viwDataTable SearchByMonth(DateTime dtDateUntil)
            {
                viwDataTable viwFilteredTable = new viwDataTable();

                foreach (viwRow CurrRow in this)
                {
                    if (CurrRow.Expense_date.Month == dtDateUntil.Month &&
                        CurrRow.Expense_date.Year == dtDateUntil.Year)
                    {
                        viwFilteredTable.ImportRow(CurrRow);
                    }
                }

                return viwFilteredTable;
            }
        }
    
        partial class viwinDataTable
        {
            /// <summary>
            /// Filters the view by the date given -based on the month and the year
            /// </summary>
            /// <param name="dtDateUntil">The month being filtered for</param>
            /// <returns>A filtered view</returns>
            public viwinDataTable SearchByMonth(DateTime dtDateUntil)
            {
                viwinDataTable viwinFilteredTable = new viwinDataTable();

                foreach (viwinRow CurrRow in this)
                {
                    if (CurrRow.Income_Date.Month == dtDateUntil.Month &&
                        CurrRow.Income_Date.Year == dtDateUntil.Year)
                    {
                        viwinFilteredTable.ImportRow(CurrRow);
                    }
                }

                return viwinFilteredTable;
            }
        }
    }
}
