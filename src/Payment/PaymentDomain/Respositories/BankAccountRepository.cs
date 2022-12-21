using BankAppModels.Entities.Payment;
using MediatR;
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
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly DataContext _context;

        public BankAccountRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(BankAccount bankAccount)
        {
            await _context.AddAsync(bankAccount);
            await _context.SaveChangesAsync();
            return bankAccount.Id;
        }

        public async Task<List<BankAccount>> Get(Guid userId)
        {
            return await _context.BankAccounts.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<BankAccount> GetAccountByNumber(string number)
        {
            return await _context.BankAccounts.FirstOrDefaultAsync(x => x.Number == number);
        }

        public async Task<Transaction> ExecuteTransaction(Transaction transaction)
        {
            var sender = await GetAccountByNumber(transaction.SenderAccountNumber);
            var recipent = await GetAccountByNumber(transaction.RecipentAccountNumber);

            sender.Balance -= transaction.Amount;
            recipent.Balance += transaction.Amount;

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }
    }
}
