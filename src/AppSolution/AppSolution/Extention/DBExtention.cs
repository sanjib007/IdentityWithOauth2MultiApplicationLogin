using System;
using DLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AppSolution.Extention
{
    public static class DBExtention
    {
        public static IServiceCollection AddDBExtention(this IServiceCollection services, IConfiguration Configuration)
        {
            // Replace with your connection string.
//            var connectionString = Configuration["ConnectionStrings:Default"];
            
            services.AddDbContext<AppDBContext>(options =>
            {
                // Configure the context to use Microsoft SQL Server.
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnection"));

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });
            
            return services;
        }
        
    }
}