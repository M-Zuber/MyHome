using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FrameWork
{
    public class SettingsManager
    {
        private FileInfo SettingsFile { get; set; }

        public SettingsManager(string strLogFileName)
        {
            this.SettingsFile = new FileInfo(strLogFileName);
        }

        public Dictionary<string, string> GetAllSettings()
        {
            Dictionary<string, string> allSettings = new Dictionary<string, string>();

            using (StreamReader settingsReader = new StreamReader(this.SettingsFile.FullName))
            {
                while (!settingsReader.EndOfStream)
	            {
                    allSettings.Add(settingsReader.ReadLine(), settingsReader.ReadLine()); 
	            }
            }

            return allSettings;
        }

        public void SaveSettings(Dictionary<string, string> allSettings)
        {
            using (StreamWriter settingsWriter = 
                                    new StreamWriter(this.SettingsFile.Open(FileMode.Create)))
            {
                foreach (KeyValuePair<string, string> CurrSetting in allSettings)
                {
                    settingsWriter.WriteLine(CurrSetting.Key);
                    settingsWriter.WriteLine(CurrSetting.Value);
                }
            }
        }
    }
}
