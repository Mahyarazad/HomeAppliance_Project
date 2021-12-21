using System.Collections.Generic;
using _0_Framework.Application;
using AMConfiguration;
using DM.Infrastructure.Core;
using IM.Infrustructure.Core;
using IM.Presentation.API;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Query.Contracts;
using Query.Query;
using SM.Infrastructure.Core;
using SM.Presentation.API;

namespace ServiceHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            var connectionString = Configuration.GetConnectionString("SMContext");
            ShopManagementConfiguration.Config(services, connectionString);
            DiscountManagementConfiguration.Config(services, connectionString);
            InventoryManagementConfiguration.Config(services, connectionString);
            AccountManagementConfiguration.Config(services, connectionString);
            services.AddTransient<IFileUploader, FileUploader>();
            services.AddTransient<IAutenticateHelper, AuthenticateHelper>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddTransient<ICalculateCart, CalculateCartQuery>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, c =>
                {
                    c.LoginPath = new PathString("/Authenticate");
                    c.LogoutPath = new PathString("/Index");
                    c.AccessDeniedPath = new PathString("/AccessDenied");
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminArea", builder =>
                    builder.RequireRole(
                        new List<string> { AuthorizationRoles.Admin, AuthorizationRoles.ContentProducer }));
                options.AddPolicy("Inventory", builder =>
                    builder.RequireRole(AuthorizationRoles.Admin));
            });


            services.AddRazorPages().AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Administrator", "/", "AdminArea");
                    options.Conventions.AuthorizeAreaFolder("Administrator", "/Inventory", "Inventory");
                })
                .AddApplicationPart(typeof(ProductController).Assembly)
                .AddApplicationPart(typeof(InventoryController).Assembly);


            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.IsEssential = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseCookiePolicy();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
