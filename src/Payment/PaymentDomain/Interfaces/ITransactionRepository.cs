using BankAppModels.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentDomain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> ExecuteTransaction(Transaction transaction);
    }
}
