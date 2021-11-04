using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Domain;
using SM.Application;
using SM.Application.Contracts;
using SM.Application.Contracts.Product;
using SM.Domain.ProductAgg;
using SM.Infrastructure.Repositories;

namespace SM.Infrastructure.Core
{
    public class ShopManagementConfiguration
    {
        public static void Config(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductApplication, ProductApplication>();

            services.AddDbContext<SMContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
