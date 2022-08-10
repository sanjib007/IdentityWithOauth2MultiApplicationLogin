using DLL;
using Microsoft.Extensions.DependencyInjection;

namespace AppSolution.Extention
{
    public static class OpenIddictExtention
    {
        public static IServiceCollection AddopenIddictExtention(this IServiceCollection services)
        {
            services.AddCors();
            services.AddOpenIddict()

                // Register the OpenIddict core components.
                .AddCore(options =>
                {
                    // Configure OpenIddict to use the Entity Framework Core stores and models.
                    // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
                    options.UseEntityFrameworkCore()
                        .UseDbContext<AppDBContext>()
                        //.ReplaceDefaultEntities<long>()
                        ;
                    
                })

                // Register the OpenIddict server components.
                .AddServer(options =>
                {
                    // Enable the token endpoint.
                    options.SetTokenEndpointUris("/connect/token")
                        .SetAuthorizationEndpointUris("/connect/authorize")
                        .SetIntrospectionEndpointUris("/connect/introspect");

                    // Enable the password flow.
                    options.AllowAuthorizationCodeFlow()
                        .AllowClientCredentialsFlow()
                        .AllowPasswordFlow()
                        .AllowRefreshTokenFlow();

                    // Accept anonymous clients (i.e clients that don't send a client_id).
                    //options.AcceptAnonymousClients();

                    // Register the signing and encryption credentials.
                    options.AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();

                    // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                    options.UseAspNetCore()
                        .EnableTokenEndpointPassthrough()
                        .EnableLogoutEndpointPassthrough()
                        .EnableTokenEndpointPassthrough()
                        .EnableUserinfoEndpointPassthrough()
                        .EnableStatusCodePagesIntegration();
                })
                .AddValidation(options =>
                {
                    // Import the configuration from the local OpenIddict server instance.
                    options.UseLocalServer();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                });
            
            services.AddHostedService<TestData>();

            return services;
        }
    }
}