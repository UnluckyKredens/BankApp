using BankAppModels.Entities.Payment;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentDomain.Enums;
using PaymentInfrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentEngine.Queries.Handlers
{
    public class GetTransactionsFilterQueryHandler : IRequestHandler<GetTransactionsFilterQuery, IEnumerable<Transaction>>
    {
        private readonly DataContext _context;

        public GetTransactionsFilterQueryHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> Handle(GetTransactionsFilterQuery request, CancellationToken cancellationToken)
        {
            if (await _context.BankAccounts.FirstOrDefaultAsync(x => x.Number == request.AccountNumber && x.UserId == request.UserId) == null)
                throw new InvalidOperationException();

            var userTransactions = _context.Transactions.
                Where(x => x.CreatedAt > request.From && x.CreatedAt <= request.To
                && (x.RecipentAccountNumber == request.AccountNumber || x.SenderAccountNumber == request.AccountNumber));

            if (request.Searcher != string.Empty && request.Searcher != request.AccountNumber)
            {
                userTransactions = userTransactions.Where(x => 
                x.RecipentName.Contains(request.Searcher) || x.SenderName.Contains(request.Searcher) || 
                x.RecipentAccountNumber.Contains(request.Searcher) || x.SenderAccountNumber.Contains(request.Searcher) || 
                x.Title.Contains(request.Searcher));
            }

            if (request.AmountFrom != 0 && request.AmountTo != 0  && request.AmountFrom < request.AmountTo)
                userTransactions = userTransactions.Where(x => x.Amount >= request.AmountFrom && x.Amount <= request.AmountTo);

            switch (request.TransactionType)
            {
                case TransactionTypeEnum.All:
                    return await userTransactions.ToListAsync();
                case TransactionTypeEnum.Recognition:
                    return await userTransactions.Where(x => x.SenderAccountNumber != request.AccountNumber).ToListAsync();
                case TransactionTypeEnum.Debit:
                    return await userTransactions.Where(x => x.SenderAccountNumber == request.AccountNumber).ToListAsync();
                default:
                    return await userTransactions.ToListAsync();
            }
        }
    }
}
