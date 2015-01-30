using System.Collections.Generic;
using LocalTypes;
using System.Data.Common;
using System.Linq;
using Dapper;

namespace DataAccess
{
    /// <summary>
    /// Contains methods neccesary for CRUD methods of income categories
    /// -Before sending to the next tier translates into Local types
    /// </summary>
    public class IncomeCategoryAccess : IRepository<IncomeCategory>
    {
        DbProviderFactory factory;

        public IncomeCategoryAccess(DbProviderFactory factory)
        {
            this.factory = factory;
        }

        public IncomeCategory LoadById(int id)
        {
            using (var conn = this.factory.CreateConnection())
            {
                return conn.Query<IncomeCategory>("SELECT id, name FROM t_incomes_category WHERE id = @id;", new { id })
                    .FirstOrDefault();
            }
        }

        public List<IncomeCategory> LoadAll()
        {
            using (var conn = this.factory.CreateConnection())
            {
                return conn.Query<IncomeCategory>("SELECT id, name FROM t_incomes_category;").ToList();
            }
        }

        public IncomeCategory Save(IncomeCategory item)
        {
            using (var conn = this.factory.CreateConnection())
            {
                // No Id means the item is new, and should be inserted
                if (item.Id == default(int))
                {
                    return conn.Query<IncomeCategory>("INSERT INTO t_incomes_category (name) VALUES (@Name); SELECT id, name FROM t_incomes_category WHERE id = LAST_INSERT_ID();", new { item.Name })
                        .FirstOrDefault();
                }
                
                // Update the item
                int result = conn.Execute("UPDATE t_incomes_category SET name = @Name WHERE id = @Id;", new { item.Id, item.Name });
                return (result == 1 ? item : null);
            }
        }
    }

    public class CachedIncomeCategoryRepository : IRepository<IncomeCategory>
    {
        Dictionary<int, IncomeCategory> cache = new Dictionary<int, IncomeCategory>();
        IRepository<IncomeCategory> source;

        public CachedIncomeCategoryRepository(IRepository<IncomeCategory> source)
        {
            this.source = source;
        }

        public IncomeCategory LoadById(int id)
        {
            if (!cache.ContainsKey(id))
                cache[id] = source.LoadById(id);

            return cache[id];
        }

        public List<IncomeCategory> LoadAll()
        {
            return source.LoadAll();
        }

        public IncomeCategory Save(IncomeCategory item)
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
