using System.Collections.Generic;
using System.Linq;
using Data;
using LocalTypes;

namespace DataAccess
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
            return _context.PaymentMethods.FirstOrDefault(i => i.Id == id);
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
