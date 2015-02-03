using System.Collections.Generic;
using LocalTypes;
using System;
using System.Linq;
using System.Data;
using System.Data.Common;
using Dapper;

namespace DataAccess
{
    /// <summary>
    /// Contains methods neccesary for CRUD methods of incomes
    /// -Before sending to the next tier translates into Local types
    /// </summary>
    public class IncomeAccess : ITransactionRepository<Income>
    {
        DbProviderFactory factory;

        public IncomeAccess(DbProviderFactory factory)
        {
            this.factory = factory;
        }

        public List<Income> LoadMonth(DateTime month)
        {
            using (var conn = this.factory.CreateConnection())
            {
                var startdate = new DateTime(month.Year, month.Month, 1);
                var enddate = new DateTime(month.Year,
                                           month.Month,
                                           DateTime.DaysInMonth(month.Year, month.Month),
                                           23, 59, 59, 999);

                var sql = @"SELECT e.id, e.amount, e.inc_date as 'date', e.comments as 'comment',
                                   c.id, c.name,
                                   m.id, m.name
                            FROM t_incomes as e
                            INNER JOIN t_incomes_category as c ON e.category = c.id
                            INNER JOIN t_payment_methods as m ON e.method = m.id
                            WHERE e.inc_date BETWEEN @startdate AND @enddate;";

                // Note: When using multi-mapping the order of the returned columns
                //       must match the order of types specified in the query method's
                //       generic paramenters, and begin with the id column.
                return conn.Query<Income, IncomeCategory, PaymentMethod, Income>(
                        sql,
                        (e, c, m) => { e.Category = c; e.Method = m; return e; },
                        new { startdate, enddate })
                    .ToList();
            }
        }

        public Income LoadById(int id)
        {
            using (var conn = this.factory.CreateConnection())
            {
                var sql = @"SELECT e.id, e.amount, e.inc_date as 'date', e.comments as 'comment',
                                   c.id, c.name,
                                   m.id, m.name
                            FROM t_incomes as e
                            INNER JOIN t_incomes_category as c ON e.category = c.id
                            INNER JOIN t_payment_methods as m ON e.method = m.id
                            WHERE e.id = @id;";

                // Note: When using multi-mapping the order of the returned columns
                //       must match the order of types specified in the query method's
                //       generic paramenters, and begin with the id column.
                return conn.Query<Income, IncomeCategory, PaymentMethod, Income>(
                        sql,
                        (e, c, m) => { e.Category = c; e.Method = m; return e; },
                        new { id })
                    .FirstOrDefault();
            }
        }

        public List<Income> LoadAll()
        {
            using (var conn = this.factory.CreateConnection())
            {
                var sql = @"SELECT e.id, e.amount, e.inc_date as 'date', e.comments as 'comment',
                                   c.id, c.name,
                                   m.id, m.name
                            FROM t_incomes as e
                            INNER JOIN t_incomes_category as c ON e.category = c.id
                            INNER JOIN t_payment_methods as m ON e.method = m.id
                            ORDER BY e.inc_date ASC, e.id ASC;";

                // Note: When using multi-mapping the order of the returned columns
                //       must match the order of types specified in the query method's
                //       generic paramenters, and begin with the id column.
                return conn.Query<Income, IncomeCategory, PaymentMethod, Income>(
                        sql,
                        (e, c, m) => { e.Category = c; e.Method = m; return e; })
                    .ToList();
            }
        }

        public Income Save(Income item)
        {
            using (var conn = this.factory.CreateConnection())
            {
                var itemData = new
                {
                    Amount = item.Amount,
                    Date = item.Date,
                    Category = item.Category.Id,
                    Method = item.Method.Id,
                    Comment = item.Comment
                };

                // No Id means the item is new, and should be inserted
                if (item.ID == default(int))
                {
                    string insertsql = @"INSERT INTO t_incomes (amount, inc_date, category, method, comments)
                                         VALUES (@Amount, @Date, @Category, @Method, @Comment);";

                    string selectsql = @"SELECT e.id, e.amount, e.inc_date as 'date', e.comments,
                                                c.id, c.name,
                                                m.id, m.name
                                         FROM t_incomes as e
                                         INNER JOIN t_incomes_category as c ON e.category = c.id
                                         INNER JOIN t_payment_methods as m ON e.method = m.id
                                         WHERE e.ROWID = LAST_INSERT_ROWID();";

                    return conn.Query<Income, IncomeCategory, PaymentMethod, Income>(
                            insertsql + selectsql,
                            (e, c, m) => { e.Category = c; e.Method = m; return e; },
                            itemData)
                        .FirstOrDefault();

                }

                // Update the item
                string sql = @"UPDATE t_incomes
                               SET amount = @Amount, inc_date = @Date, category = @Category, method = @Method, comments = @Comment
                               WHERE id = @Id;";
                int result = conn.Execute(sql, itemData);
                return (result == 1 ? item : null);
            }
        }

        public void Remove(Income item)
        {
            this.Remove(item.ID);
        }

        public void Remove(int id)
        {
            if (id == default(int)) return;

            using (var conn = this.factory.CreateConnection())
            {
                conn.Execute("DELETE FROM t_incomes WHERE id = @id;", new { id });
            }
        }
    }


    public class CachedIncomeRepository : ITransactionRepository<Income>
    {
        Dictionary<int, Income> cache = new Dictionary<int, Income>();
        ITransactionRepository<Income> source;

        public CachedIncomeRepository(ITransactionRepository<Income> source)
        {
            this.source = source;
        }

        public List<Income> LoadMonth(DateTime month)
        {
            return source.LoadMonth(month);
        }

        public Income LoadById(int id)
        {
            if (!cache.ContainsKey(id))
                cache[id] = source.LoadById(id);

            return cache[id];
        }

        public List<Income> LoadAll()
        {
            return source.LoadAll();
        }

        public Income Save(Income item)
        {
            var result = source.Save(item);

            // If the save fails and the item is not new, remove the item from the cache
            if (result == null && item.ID != default(int) && cache.ContainsKey(item.ID))
                cache.Remove(item.ID);

            // If the save succeeded, update the cache
            if (result != null)
                cache[result.ID] = result;

            return result;
        }

        public void Remove(Income item)
        {
            this.Remove(item.ID);
        }

        public void Remove(int id)
        {
            if (id != default(int) && cache.ContainsKey(id))
                cache.Remove(id);

            source.Remove(id);
        }
    }
}
