using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Reflection;
using PaymentEngine.Identity;

namespace PaymentEngine
{
    public static class ConfigureService
    {
        public static IServiceCollection AddEngineService(this IServiceCollection services)
        {
            services.AddTransient<IPermissionAccess, PermissionAccess>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
