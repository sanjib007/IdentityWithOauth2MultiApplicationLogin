using DLL;
using DLL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AppSolution.Extention
{
    public static class IdentityExtention
    {
        public static IServiceCollection AddIdentityExtention(this IServiceCollection services)
        {
            
            services.AddIdentity<AppUser, UserRoles>(
                    options => {
                        options.SignIn.RequireConfirmedAccount = false;
                        options.Stores.MaxLengthForKeys = 85;
                        options.Password.RequireDigit = true;
                        options.Password.RequireLowercase = true;
                        options.Password.RequireNonAlphanumeric = true;
                        options.Password.RequireUppercase = true;
                        options.Password.RequiredLength = 6;
                        options.Password.RequiredUniqueChars = 1;
                    }
                )
                .AddEntityFrameworkStores<AppDBContext>()
                .AddDefaultTokenProviders();
            
            return services;
        }
    }
}