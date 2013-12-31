﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using FrameWork;

namespace BL
{
    #region Delegate Region

    public delegate void TableProgressDelegate();

    public delegate void AllDataProgressDelegate();
    
    #endregion

    public class Backup
    {
        #region Event Members

        public event TableProgressDelegate TableProgress;

        public event AllDataProgressDelegate AllDataProgress;
        
        #endregion

        #region Data Members

        private DirectoryInfo backupFolder = new DirectoryInfo("./Backup Files");
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

        public Backup()
        {
            if (!this.backupFolder.Exists)
            {
                backupFolder.Create();
            }

            foreach (KeyValuePair<string, FileInfo> CurrbackupFile in this.backupFiles)
            {
                if (!CurrbackupFile.Value.Exists)
                {
                    using (StreamWriter stwrAppend = CurrbackupFile.Value.AppendText())
                    {
                        stwrAppend.WriteLine("The file was created on: " + DateTime.Now.ToString());
                    }
                }
            }
        }
        
        #endregion

        #region Event Methods

        private void OnTableComplete()
        {
            if (this.AllDataProgress != null)
            {
                this.AllDataProgress();
            }
        }

        private void OnLineWritten()
        {
            if (this.TableProgress != null)
            {
                this.TableProgress();
            }
        }

        #endregion

        #region Other Methods

        public void BackupData()
        {
            foreach (KeyValuePair<string, FileInfo> CurrbackupFile in this.backupFiles)
            {
                this.BackupTable(CurrbackupFile.Key);
            }
        }

        private void BackupTable(string tableName)
        {
            using (StreamWriter stwrAppend =
                        new StreamWriter(backupFiles[tableName].Open(FileMode.Create)))
            {
                foreach (DataRow CurrRow in Cache.SDB.Tables[tableName].Rows)
                {
                    foreach (var dataPiece in CurrRow.ItemArray)
                    {
                        stwrAppend.WriteLine(dataPiece);
                    }
                    this.OnLineWritten();
                }
            }
            this.OnTableComplete();
        }
        
        #endregion
    }
}