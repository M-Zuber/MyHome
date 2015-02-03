using Dapper;
using LocalTypes;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DataAccess
{
    /// <summary>
    /// Contains methods neccesary for CRUD methods of payment methods
    /// -Before sending to the next tier translates into Local types
    /// </summary>
    public class PaymentMethodAccess : IRepository<PaymentMethod>
    {
        DbProviderFactory factory;

        public PaymentMethodAccess(DbProviderFactory factory)
        {
            this.factory = factory;
        }

        public PaymentMethod LoadById(int id)
        {
            using (var conn = this.factory.CreateConnection())
            {
                return conn.Query<PaymentMethod>("SELECT id, name FROM t_payment_methods WHERE id = @id;", new { id }).FirstOrDefault();
            }
        }

        public List<PaymentMethod> LoadAll()
        {
            using (var conn = this.factory.CreateConnection())
            {
                return conn.Query<PaymentMethod>("SELECT id, name FROM t_payment_methods;").ToList();
            }
        }

        public PaymentMethod Save(PaymentMethod item)
        {
            using (var conn = this.factory.CreateConnection())
            {
                // No Id means the item is new, and should be inserted
                if (item.Id == default(int))
                {
                    return conn.Query<PaymentMethod>("INSERT INTO t_payment_methods (name) VALUES (@Name); SELECT id, name FROM t_payment_methods WHERE ROWID = LAST_INSERT_ROWID();", new { item.Name })
                        .FirstOrDefault();
                }
                
                // Update the item
                int result = conn.Execute("UPDATE t_payment_methods SET name = @Name WHERE id = @Id;", new { item.Id, item.Name });
                return (result == 1 ? item : null);
            }
        }
    }


    public class CachedPaymentMethodRepository : IRepository<PaymentMethod>
    {
        Dictionary<int, PaymentMethod> cache = new Dictionary<int, PaymentMethod>();
        IRepository<PaymentMethod> source;

        public CachedPaymentMethodRepository(IRepository<PaymentMethod> source)
        {
            this.source = source;
        }

        public PaymentMethod LoadById(int id)
        {
            if (!cache.ContainsKey(id))
                cache[id] = source.LoadById(id);

            return cache[id];
        }

        public List<PaymentMethod> LoadAll()
        {
            return source.LoadAll();
        }

        public PaymentMethod Save(PaymentMethod item)
        {
            var result = source.Save(item);
            
            // If the save fails and the item is not new, remove the item from the cache
            if (result == null && item.Id != default(int) && cache.ContainsKey(item.Id))
                cache.Remove(item.Id);

            // If the save succeeded, update the cache
            if (result != null)
                cache[result.Id] = result;

            return result;
        }
    }
}
