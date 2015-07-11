using System.Collections.Generic;
using MyHome.DataRepository;
using MyHome.Persistence;

namespace MyHome.Services
{
    public class CategoryService
    {
        private readonly Dictionary<int, ICategoryService> _categoryServicesById;
        
        public CategoryService(AccountingDataContext context)
        {
            var context1 = context;
            var expenseCategoryService = new ExpenseCategoryService(new ExpenseCategoryRepository(context1));
            var incomeCategoryService = new IncomeCategoryService(new IncomeCategoryRepository(context1));
            var paymentMethodService = new PaymentMethodService(new PaymentMethodRepository(context1));

            _categoryServicesById = new Dictionary<int, ICategoryService>
            {
                {1, expenseCategoryService},
                {2, incomeCategoryService},
                {3, paymentMethodService}
            };
        }
    

        private static readonly Dictionary<int, string> CategoryTypeNames =
           new Dictionary<int, string>
           {
                {1, "Expense Categories"},
                {2, "Income Categories"},
                {3, "Payment Methods"}
           };


        public Dictionary<int, ICategoryService> CategoryHandlers
        {
            get
            {
                return _categoryServicesById;
            }
        }


        public Dictionary<int, string> CategoryTypeNamesById
        {
            get
            {
                return CategoryTypeNames; 
            }
        }
    }
}
