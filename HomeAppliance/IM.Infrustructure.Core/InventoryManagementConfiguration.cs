using System;
using IM.Application;
using IM.Application.Contracts;
using IM.Domain;
using IM.Infrastructure;
using IM.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Infrustructure.Core
{
    public class InventoryManagementConfiguration
    {
        public static void Config(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IInventoryApplication, InventoryApplication>();

            services.AddDbContext<IMContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
