using Microsoft.Extensions.DependencyInjection;
using PaymentDomain.Interfaces;
using PaymentDomain.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PaymentDomain
{
    public static class ConfigureDomain
    {
        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {
            services.AddTransient<IBankAccountRepository, BankAccountRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            return services;
        }
    }
}
