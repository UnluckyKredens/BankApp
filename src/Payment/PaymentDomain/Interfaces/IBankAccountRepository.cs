using BankAppModels.Entities.Payment;
using PaymentDomain.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentDomain.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<Guid> Add(BankAccount bankAccount);
        Task<List<BankAccount>> Get(Guid userId);
        Task<BankAccount> GetAccountByNumber(string number);
        Task<Transaction> ExecuteTransaction(Transaction transaction);
    }
}
