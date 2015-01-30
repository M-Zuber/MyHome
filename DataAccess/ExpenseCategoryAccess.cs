using System.Collections.Generic;
using LocalTypes;
using System.Linq;
using Dapper;
using System.Data.Common;

namespace DataAccess
{
    /// <summary>
    /// Contains methods neccesary for CRUD methods of expense categories
    /// -Before sending to the next tier translates into Local types
    /// </summary>
    public class ExpenseCategoryAccess : IRepository<ExpenseCategory>
    {
        DbProviderFactory factory;

        public ExpenseCategoryAccess(DbProviderFactory factory)
        {
            this.factory = factory;
        }

        public ExpenseCategory LoadById(int id)
        {
            using (var conn = this.factory.CreateConnection())
            {
                return conn.Query<ExpenseCategory>("SELECT id, name FROM t_expenses_category WHERE id = @id;", new { id })
                    .FirstOrDefault();
            }
        }

        public List<ExpenseCategory> LoadAll()
        {
            using (var conn = this.factory.CreateConnection())
            {
                return conn.Query<ExpenseCategory>("SELECT id, name FROM t_expenses_category;").ToList();
            }
        }

        public ExpenseCategory Save(ExpenseCategory item)
        {
            using (var conn = this.factory.CreateConnection())
            {
                // No Id means the item is new, and should be inserted
                if (item.Id == default(int))
                {
                    return conn.Query<ExpenseCategory>("INSERT INTO t_expenses_category (name) VALUES (@Name); SELECT id, name FROM t_expenses_category WHERE id = LAST_INSERT_ID();", new { item.Name })
                        .FirstOrDefault();
                }

                // Update the item
                int result = conn.Execute("UPDATE t_expenses_category SET name = @Name WHERE id = @Id;", new { item.Id, item.Name });
                return (result == 1 ? item : null);
            }
        }
    }

    public class CachedExpenseCategoryRepository : IRepository<ExpenseCategory>
    {
        Dictionary<int, ExpenseCategory> cache = new Dictionary<int, ExpenseCategory>();
        IRepository<ExpenseCategory> source;

        public CachedExpenseCategoryRepository(IRepository<ExpenseCategory> source)
        {
            this.source = source;
        }

        public ExpenseCategory LoadById(int id)
        {
            if (!cache.ContainsKey(id))
                cache[id] = source.LoadById(id);

            return cache[id];
        }

        public List<ExpenseCategory> LoadAll()
        {
            return source.LoadAll();
        }

        public ExpenseCategory Save(ExpenseCategory item)
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
