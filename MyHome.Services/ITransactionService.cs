using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyHome.DataClasses;

namespace MyHome.Services
{
    public interface ITransactionService
    {
        void Create(Transaction transaction);
        IEnumerable<Transaction> GetAll();
    }
}
