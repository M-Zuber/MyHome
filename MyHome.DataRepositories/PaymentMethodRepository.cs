using System.Collections.Generic;
using System.Linq;
using MyHome.DataClasses;
using MyHome.Persistence;

namespace MyHome.DataRepository
{
    public class PaymentMethodRepository
    {
        private readonly AccountingDataContext _context;

        public PaymentMethodRepository(AccountingDataContext context)
        {
            _context = context;
        }

        public PaymentMethod GetById(int id)
        {
            return _context.PaymentMethods.FirstOrDefault(x => x.Id == id);
        }

        public PaymentMethod GetByName(string name)
        {
            return _context.PaymentMethods.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<PaymentMethod> GetAll()
        {
            return _context.PaymentMethods.ToList();
        }

        public void Save(PaymentMethod paymentMethod)
        {
            if (paymentMethod.Id != 0)
            {
                Update(paymentMethod);
            }
            else
            {
                Create(paymentMethod);
            }
        }

        public void Update(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Attach(paymentMethod);
            _context.SaveChanges();
        }

        public void Create(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Add(paymentMethod);
            _context.SaveChanges();
        }
    }
}
