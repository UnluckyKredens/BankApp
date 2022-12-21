using BankAppModels.Entities.Payment;
using MediatR;
using PaymentDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentEngine.Queries.Handlers
{
    public class GetAccountsByUserQueryHandler : IRequestHandler<GetAccountsByUserQuery, List<BankAccount>>
    {
        private readonly IBankAccountRepository _repository;

        public GetAccountsByUserQueryHandler(IBankAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BankAccount>> Handle(GetAccountsByUserQuery request, CancellationToken cancellationToken)
        { 
           var accounts = await _repository.Get(request.UserId);

            return accounts;  
        }
    }
}
