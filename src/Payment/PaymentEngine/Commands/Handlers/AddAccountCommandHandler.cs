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
    public class AddAccountCommandHandler : IRequestHandler<AddAccountCommand, Guid>
    {
        private readonly IBankAccountRepository _repository;

        public AddAccountCommandHandler(IBankAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            if(request.UserId == Guid.Empty) throw new FailInsertException("Could not add bank account");

            var createdAt = DateTimeOffset.Now;
            var account = new BankAccount
            {
                UserId = request.UserId,
                Name = request.Name,
                Balance = request.Amount,
                CreatedAt = createdAt,
                UpdatedAt = createdAt,
                Number = Guid.NewGuid().ToString(),
            };

            var id = await _repository.Add(account);

            if (id == Guid.Empty) 
                throw new FailInsertException("Could not add bank account");

            return id;
        }
    }
}
