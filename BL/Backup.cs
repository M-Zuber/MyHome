using System.Collections.Generic;
using System.Data;
using System.IO;
using FrameWork;

namespace BL
{
    #region Delegate Region

    /// <summary>
    /// Delegate for progress in a specific table
    /// </summary>
    public delegate void TableProgressDelegate();

    /// <summary>
    /// Delegate for progress in the data-set as a whole
    /// </summary>
    public delegate void AllDataProgressDelegate();
    
    #endregion

    /// <summary>
    /// Saves the data currently in the database into text files
    /// </summary>
    public class Backup
    {
        #region Event Members

        // Event for in-table progress
        public event TableProgressDelegate TableProgress;

        // Event for overall progress in the data-set
        public event AllDataProgressDelegate AllDataProgress;
        
        #endregion

        #region Data Members

        // Instance of the folder for the backup files
        private DirectoryInfo backupFolder = new DirectoryInfo("./Backup Files");

        // List of instances of the backup files - one per table
        private Dictionary<string, FileInfo> backupFiles = new Dictionary<string, FileInfo>()
        {
            {"t_expenses_category",new FileInfo("./Backup Files/ExpenseCategories.txt")},
            {"t_incomes_category",new FileInfo("./Backup Files/IncomeCategories.txt")},
            {"t_payment_methods",new FileInfo("./Backup Files/PaymentMethods.txt")},
            {"t_expenses",new FileInfo("./Backup Files/Expenses.txt")},
            {"t_incomes",new FileInfo("./Backup Files/Incomes.txt")}
        };
        
        #endregion

        #region C'Tor

        /// <summary>
        /// Verifys that the folder and files exist before a backup is performed
        /// </summary>
        public Backup()
        {
            // If the folder doesnt exist yet -creates it
            if (!this.backupFolder.Exists)
            {
                backupFolder.Create();
            }

            // Goes over every file in the list to make sure it exists
            foreach (KeyValuePair<string, FileInfo> CurrbackupFile in this.backupFiles)
            {
                if (!CurrbackupFile.Value.Exists)
                {
                    // If the file doesnt exist, creates it.
                    // No actual value is written in, this is just the simplest way to create
                    // the file without locking up the resource
                    using (StreamWriter stwrAppend = CurrbackupFile.Value.AppendText()){}
                }
            }
        }
        
        #endregion

        #region Event Methods

        /// <summary>
        /// Only raises the AllDataProgress event if someone is registered
        /// </summary>
        private void OnTableComplete()
        {
            if (this.AllDataProgress != null)
            {
                this.AllDataProgress();
            }
        }

        /// <summary>
        /// Only raises the TableProgress event if someone is registered
        /// </summary>
        private void OnLineWritten()
        {
            if (this.TableProgress != null)
            {
                this.TableProgress();
            }
        }

        #endregion

        #region Other Methods

        /// <summary>
        /// Saves the all the data into text files
        /// </summary>
        public void BackupData()
        {
            // Goes over every file in the list and writes into it the data of the corrsponding
            // table
            foreach (KeyValuePair<string, FileInfo> CurrbackupFile in this.backupFiles)
            {
                this.BackupTable(CurrbackupFile.Key);
            }
        }

        /// <summary>
        /// Saves the data of the requested table into the corrsponding file
        /// </summary>
        /// <param name="tableName">The name of the table to be saved</param>
        private void BackupTable(string tableName)
        {
            // Opens the appropriate file, clears the file of all previous data 
            using (StreamWriter stwrAppend =
                        new StreamWriter(backupFiles[tableName].Open(FileMode.Create)))
            {
                // Goes over every row in the selected table
                foreach (DataRow CurrRow in Cache.SDB.Tables[tableName].Rows)
                {
                    // Goes over the data of each coloumn
                    foreach (var dataPiece in CurrRow.ItemArray)
                    {
                        // Saves the data of each coloumn on a new line
                        stwrAppend.WriteLine(dataPiece);
                    }

                    // Raises the event of in-table progress
                    this.OnLineWritten();
                }
            }

            // Raise the event of data-set progress
            this.OnTableComplete();
        }
        
        #endregion
    }
}
