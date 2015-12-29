using System;
using System.IO;

namespace FrameWork
{
    /// <summary>
    /// Represents an instance of a file used for logging
    /// </summary>
    public class Log
    {
        #region Data Members

        // Instance of file being logged into
        private FileInfo LogFile { get; set; }
        
        #endregion

        #region C'Tor

        /// <summary>
        /// Verifys that the folder and file exist before any log is written 
        /// </summary>
        /// <param name="strLogFileName">The name of the log file -including the directory</param>
        public Log(string strLogFileName)
        {
            // Intializes the instance of the log file
            LogFile = new FileInfo(strLogFileName);

            if (!LogFile.Directory.Exists)
            {
                // Creates the directory of the log file if does not exist
                Directory.CreateDirectory(LogFile.Directory.FullName);
            }


            if (!LogFile.Exists)
            {
                // If the log file does not exist, creates the file -adding a creation timestamp to the top
                using (StreamWriter stwrAppend = LogFile.AppendText())
                {
                    stwrAppend.WriteLine("The file was created on: " + DateTime.Now.ToString());
                }
            }
        }
        
        #endregion

        #region Logging Methods
        
        #region Message Logging
        
        /// <summary>
        /// Adds a message to the end of the file
        /// </summary>
        /// <param name="strMessage">The message to be logged</param>
        public void AddMessage(string strMessage)
        {
            using (StreamWriter stwrAppend = LogFile.AppendText())
            {
                stwrAppend.WriteLine(strMessage);
            }
        }

        /// <summary>
        /// Adds a list of messages to the end of the file -each one on a new line
        /// </summary>
        /// <param name="Messages">The messages to be logged</param>
        public void AddMessages(params string[] Messages)
        {
            using (StreamWriter logWriter = LogFile.AppendText()) 
            {
                foreach (string message in Messages)
                {
                    // Each message in the argument list is added on a seperate line
                    logWriter.WriteLine(message);
                }
            }
        }
        
        #endregion

        #region Error Logging

        /// <summary>
        /// Logs a standard error
        /// </summary>
        /// <param name="nErrNo">The error number -local or from a system exception</param>
        /// <param name="strMessage">The error message</param>
        /// <param name="dtErrorTime">The time of the error</param>
        public void AddError(int nErrNo, string strMessage, DateTime dtErrorTime)
        {
            using (StreamWriter stwrAppend = LogFile.AppendText())
            {
                stwrAppend.WriteLine(nErrNo.ToString() + "\t" +
                                     strMessage + "\t" +
                                     dtErrorTime.ToString());
            }
        }

        /// <summary>
        /// Logs an error that is locally defined
        /// </summary>
        /// <param name="enErrorNumber">Error code from the localy defined enum</param>
        /// <param name="strMessage">The error message</param>
        /// <param name="dtErrorTime">The time of the error</param>
        public void AddError(Globals.ErrorCodes enErrorNumber, string strMessage, DateTime dtErrorTime)
        {
            using (StreamWriter stwrAppend = LogFile.AppendText())
            {
                stwrAppend.WriteLine(((int)enErrorNumber).ToString() + "\t" +
                                     strMessage + "\t" +
                                     dtErrorTime.ToString());
            }
        }
        
        #endregion

        #endregion
    }
}
