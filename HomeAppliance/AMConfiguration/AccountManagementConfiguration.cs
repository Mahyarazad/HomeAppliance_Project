using Microsoft.Extensions.DependencyInjection;
using System;
using AM.Application;
using AM.Application.Contracts;
using AM.Domain;
using AM.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AMConfiguration
{
    public class AccountManagementConfiguration
    {
        public static void Config(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountApplication, AccountApplication>();

            services.AddDbContext<AMContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
