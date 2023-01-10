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
        private readonly IBankAccountRepository _bankAccountrepository;
        private readonly ITransactionRepository _transactionRepository;

        public ExecuteTransactionCommandHandler(IBankAccountRepository repository, ITransactionRepository transactionRepository)
        {
            _bankAccountrepository = repository;
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> Handle(ExecuteTransactionCommand request, CancellationToken cancellationToken)
        {
            if (await _bankAccountrepository.Get(Guid.Parse(request.RecipentAccountNumber)) == null)
                throw new NotFoundException("User not found");

            var transacton = new Transaction
            {
                SenderAccountNumber = request.SenderAccountNumber,
                RecipentName = request.RecipentName,
                RecipentAccountNumber = request.RecipentAccountNumber,
                Amount = request.Amount,
                CreatedAt = DateTime.Now,
                Title = request.Title,
            };

            await _transactionRepository.ExecuteTransaction(transacton);
            await _bankAccountrepository.ChangeBalance(transacton.SenderAccountNumber, transacton.RecipentAccountNumber, transacton.Amount);

            return transacton;          
        }
    }
}
