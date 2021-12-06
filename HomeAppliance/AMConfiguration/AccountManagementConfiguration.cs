using Microsoft.Extensions.DependencyInjection;
using System;
using AM.Application;
using AM.Application.Contracts.Account;
using AM.Application.Contracts.Role;
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

            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IRoleApplication, RoleApplication>();

            services.AddDbContext<AMContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
