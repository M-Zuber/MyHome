using System.Collections.Generic;
namespace BL
{
    /// <summary>
    /// Abstract base class for the Bl classes representing specific tables
    /// </summary>
    public abstract class BaseBL
    {
        #region Properties

        /// <summary>
        /// The name of the item in the table the class represents
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The id of the item in table the class represents
        /// </summary>
        public virtual int ID { get; internal set; }

        #endregion

        #region Other Methods

        /// <summary>
        /// Saves the data of the table
        /// </summary>
        public abstract void Save();

        #endregion
    }
}
