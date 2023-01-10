using BankAppModels.Entities.Payment;
using Microsoft.EntityFrameworkCore;
using PaymentDomain.Interfaces;
using PaymentInfrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentDomain.Respositories
{
    public class TransactionRepository : ITransactionRepository
    { 
        private readonly DataContext _context;
        public TransactionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Transaction> ExecuteTransaction(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }
    }
}
