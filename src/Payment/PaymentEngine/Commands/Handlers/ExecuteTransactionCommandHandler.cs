using BankAppModels.Entities.Payment;
using MediatR;
using PaymentDomain.Exceptions;
using PaymentDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentEngine.Commands.Handlers
{
    public class ExecuteTransactionCommandHandler : IRequestHandler<ExecuteTransactionCommand, Transaction>
    {
        private readonly IBankAccountRepository _repository;

        public ExecuteTransactionCommandHandler(IBankAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Transaction> Handle(ExecuteTransactionCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.GetAccountByNumber(request.RecipentAccountNumber) == null)
                throw new NotFoundException("User not found");

            var transacton = new Transaction
            {
                SenderAccountNumber = request.SenderAccountNumber,
                RecipentName = request.RecipentName,
                RecipentAccountNumber = request.RecipentAccountNumber,
                Amount = request.Amount,
                CreatedAt = DateTime.UtcNow,
                Title = request.Title,
            };

            await _repository.ExecuteTransaction(transacton);

            return transacton;          
        }
    }
}
