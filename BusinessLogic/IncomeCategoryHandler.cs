using System.Collections.Generic;
using LocalTypes;
using System.Linq;

namespace BusinessLogic
{
    /// <summary>
    /// Holds methods for sorting and making calculations on the data of income categories
    /// Is also the bridge from the UI to the Dal
    /// </summary>
    public class IncomeCategoryHandler : BaseCategoryHandler
    {
        IRepository<IncomeCategory, int> icRepository;

        public IncomeCategoryHandler(IRepository<IncomeCategory, int> icRepository)
        {
            this.icRepository = icRepository;
        }

        #region CRUD Methods

        #region Read Methods

        /// <summary>
        /// Loads a specific Income Category from the cache
        /// </summary>
        /// <param name="id">The id of the Income category wanted</param>
        /// <returns>The Income category as it is in the cache</returns>
        public IncomeCategory LoadById(int id)
        {
            return icRepository.LoadById(id);
        }

        /// <summary>
        /// Loads all the Income Categories from the cache
        /// </summary>
        /// <returns>All the Income Categories as they are in the cache in generic-based
        /// list
        /// </returns>
        public override List<BaseCategory> LoadAll()
        {
            return icRepository.LoadAll().ToList<BaseCategory>();
        }

        #endregion
        
        #region Create Methods

        public override int AddNewCategory(string categoryName)
        {
            var result = icRepository.Save(new IncomeCategory { Name = categoryName });
            return (result != null ? result.Id : default(int));
        }

        #endregion

        #region Update Methods

        public override bool Save(BaseCategory categoryToSave)
        {
            var result = icRepository.Save(categoryToSave as IncomeCategory);
            return result != null;
        }

        #endregion
        
        #endregion
    }
}
