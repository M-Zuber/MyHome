using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FrameWork
{
    public class Log
    {
        private FileInfo LogFile { get; set; }

        public Log(string strLogFileName)
        {
            this.LogFile = new FileInfo(strLogFileName);

            if (!this.LogFile.Exists)
            {
                using (StreamWriter stwrAppend = this.LogFile.AppendText())
                {
                    stwrAppend.WriteLine("The file was created on: " + DateTime.Now.ToString());
                }   
            }
        }

        public void AddMessage(string strMessage)
        {
            using (StreamWriter stwrAppend = this.LogFile.AppendText())
            {
                stwrAppend.WriteLine(strMessage);
            }
        }

        public void AddMessages(params string[] Messages)
        {
            foreach (string CurrMessage in Messages)
	        {
                this.AddMessage(CurrMessage);
	        }
        }

        public void AddError(int nErrNo, string strMessage, DateTime dtErrorTime)
        {
            using (StreamWriter stwrAppend = this.LogFile.AppendText())
            {
                stwrAppend.WriteLine(nErrNo.ToString() + "\t" +
                                     strMessage + "\t" +
                                     dtErrorTime.ToString());
            }
        }

        public void AddError(Globals.ErrorCodes enErrorNumber, string strMessage, DateTime dtErrorTime)
        {
            using (StreamWriter stwrAppend = this.LogFile.AppendText())
            {
                stwrAppend.WriteLine(((int)enErrorNumber).ToString() + "\t" +
                                     strMessage + "\t" +
                                     dtErrorTime.ToString());
            }
        }
    }
}
