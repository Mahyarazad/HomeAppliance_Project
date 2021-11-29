using System;
using System.ComponentModel;
using DM.Application;
using DM.Application.Contracts;
using DM.Domian;
using DM.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DM.Infrastructure.Core
{
    public class DiscountManagementConfiguration
    {
        public static void Config(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IEndUserDiscountRepository, EndUserDiscountRepository>();
            services.AddTransient<IEndUserDiscountApplication, EndUserDiscountApplication>();

            services.AddDbContext<DMContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
