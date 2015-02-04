using Dapper;
using MyHome2013.Core.LocalTypes;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DataAccess
{
    /// <summary>
    /// Contains methods neccesary for CRUD methods of payment methods
    /// -Before sending to the next tier translates into Local types
    /// </summary>
    public class KeyValueRepository<T> : IRepository<T>
        where T : BaseCategory
    {
        DbProviderFactory factory;
        string tablename;

        public KeyValueRepository(string tablename, DbProviderFactory factory)
        {
            this.tablename = tablename;
            this.factory = factory;
        }

        public T LoadById(int id)
        {
            using (var conn = this.factory.CreateConnection())
            {
                return conn.Query<T>("SELECT id, name FROM " + this.tablename + " WHERE id = @id;", new { id }).FirstOrDefault();
            }
        }

        public List<T> LoadAll()
        {
            using (var conn = this.factory.CreateConnection())
            {
                return conn.Query<T>("SELECT id, name FROM " + this.tablename + ";").ToList();
            }
        }

        public T Save(T item)
        {
            using (var conn = this.factory.CreateConnection())
            {
                // No Id means the item is new, and should be inserted
                if (item.Id == default(int))
                {
                    return conn.Query<T>("INSERT INTO " + this.tablename + " (name) VALUES (@Name); SELECT id, name FROM " + this.tablename + " WHERE ROWID = LAST_INSERT_ROWID();", new { item.Name })
                        .FirstOrDefault();
                }
                
                // Update the item
                int result = conn.Execute("UPDATE " + this.tablename + " SET name = @Name WHERE id = @Id;", new { item.Id, item.Name });
                return (result == 1 ? item : null);
            }
        }
    }

    public class PaymentMethodAccess : KeyValueRepository<PaymentMethod>
    {
        public PaymentMethodAccess(DbProviderFactory factory) : base("t_payment_methods", factory) { }
    }

    public class IncomeCategoryAccess : KeyValueRepository<IncomeCategory>
    {
        public IncomeCategoryAccess(DbProviderFactory factory) : base("t_incomes_category", factory) { }
    }
    
    public class ExpenseCategoryAccess : KeyValueRepository<ExpenseCategory>
    {
        public ExpenseCategoryAccess(DbProviderFactory factory) : base("t_expenses_category", factory) { }
    }
}
