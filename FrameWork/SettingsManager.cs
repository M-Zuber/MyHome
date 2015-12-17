using System.Collections.Generic;
using System.IO;

namespace FrameWork
{
    /// <summary>
    /// Represents an instance of a settings file
    /// </summary>
    public class SettingsManager
    {
        #region Data Members

        // Instance of the settings file
        private FileInfo SettingsFile { get; set; }
        
        #endregion

        #region C'Tor

        /// <summary>
        /// Verifys that the folder and file exist on creation of any instance of the class
        /// </summary>
        /// <param name="settingsFileName">The name of the settings file -including the directory</param>
        public SettingsManager(string settingsFileName)
        {
            // Intializes the instance of the setting file
            this.SettingsFile = new FileInfo(settingsFileName);

            if (!this.SettingsFile.Directory.Exists)
            {
                // Creates the directory of the setting file if does not exist
                Directory.CreateDirectory(this.SettingsFile.Directory.FullName);
            }

            if (!this.SettingsFile.Exists)
            {
                // If the file doesnt exist, creates it.
                // No actual value is written in, this is just the simplest way to create
                // the file without locking up the resource
                using (StreamWriter stwrAppend = this.SettingsFile.AppendText()){}
            }
        }
        
        #endregion

        #region Other Methods

        /// <summary>
        /// Checks if the settings in the list have been saved into the settings file
        /// </summary>
        /// <param name="settingNames">The list of settings being searched for</param>
        /// <returns>True only if all the settings are set</returns>
        public bool AreSettingsSet(List<string> settingNames)
        {
            // Gets all the settings of the file the class instance is place holding for
            Dictionary<string, string> allSettings = GetAllSettings();

            // Checks that every setting in the list of requested settings
            // was in the file
            foreach (string CurrSetting in settingNames)
            {
                if (!allSettings.ContainsKey(CurrSetting))
                {
                    // If the list of settings from the file does not contain 
                    // the current setting - returns false
                    return false;
                }
            }

            // All the requested settings are in the file
            return true;
        }

        /// <summary>
        /// Reads all the settings currently in the file into memory
        /// </summary>
        /// <returns>A string keyed dictionary of the settings</returns>
        public Dictionary<string, string> GetAllSettings()
        {
            var allSettings = new Dictionary<string, string>();

            using (StreamReader settingsReader = new StreamReader(this.SettingsFile.FullName))
            {
                while (!settingsReader.EndOfStream)
                {
                    // Reads the name of the setting and the value, and saves them into the return variable
                    allSettings.Add(settingsReader.ReadLine(), settingsReader.ReadLine());
                }
            }

            // Returns the dictionary with the settings in the file
            return allSettings;
        }

        /// <summary>
        /// Saves the settings into the file - each key:value on consecutive lines
        /// </summary>
        /// <param name="allSettings">The settings to be saved to the file</param>
        public void SaveSettings(Dictionary<string, string> allSettings)
        {
            // Opens the file and cleans it from all previous data
            using (StreamWriter settingsWriter =
                                    new StreamWriter(this.SettingsFile.Open(FileMode.Create)))
            {
                // Goes over each key:value in the settings dictionary
                foreach (KeyValuePair<string, string> CurrSetting in allSettings)
                {
                    // The key and corrosponding value are written in consecutive lines
                    settingsWriter.WriteLine(CurrSetting.Key);
                    settingsWriter.WriteLine(CurrSetting.Value);
                }
            }
        }

        #endregion
    }
}
