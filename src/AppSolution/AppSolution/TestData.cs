using System;
using System.Threading;
using System.Threading.Tasks;
using DLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;

namespace AppSolution
{
    public class TestData : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public TestData(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();
            await context.Database.EnsureCreatedAsync(cancellationToken);

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("Test", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "Test",
                    ClientSecret = "test123",
                    DisplayName = "Test",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,
                        OpenIddictConstants.Permissions.Endpoints.Introspection
                        

                        //OpenIddictConstants.Permissions.Prefixes.Scope + "api"
                    }
                }, cancellationToken);
            }
            if (await manager.FindByClientIdAsync("Client1", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "Client1",
                    ClientSecret = "client123",
                    DisplayName = "Client1",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Introspection
                    }
                }, cancellationToken);
            }
            if (await manager.FindByClientIdAsync("Client2", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "Client2",
                    ClientSecret = "client123",
                    DisplayName = "Client2",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Introspection
                    }
                }, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}