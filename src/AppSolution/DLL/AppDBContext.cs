using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using DLL.ModelInterface;
using DLL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DLL
{
    public class AppDBContext : IdentityDbContext<AppUser, UserRoles, long,
        IdentityUserClaim<long>, ApplicationUserRole, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }
        
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        
        //dotnet ef migrations add "Initial" -p "DLL" -c  AppDBContext -s "AppSolution"
        //dotnet ef database update -p "DLL" -c  AppDBContext -s "AppSolution"


    }
    
    
}