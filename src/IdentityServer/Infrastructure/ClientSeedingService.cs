using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Writers;
using OpenIddict.Abstractions;

namespace IdentityServer.Infrastructure
{
    public class ClientSeedingService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public ClientSeedingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            // var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            // await context.Database.EnsureCreatedAsync(cancellationToken);

            await RegisterScopes(scope);
            await RegisterApplication(cancellationToken, scope);
        }

        private async Task RegisterScopes(IServiceScope scope)
        {
            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

            if (await manager.FindByNameAsync(ApplicationConstants.Scopes.MobileApplication) == null)
            {
                var descriptor = new OpenIddictScopeDescriptor
                {
                    Name = ApplicationConstants.Scopes.MobileApplication,
                    Resources =
                    {
                        ApplicationConstants.Resources.MobileApi
                    }
                };

                await manager.CreateAsync(descriptor);
            }

            if (await manager.FindByNameAsync(ApplicationConstants.Scopes.AdminApplication) == null)
            {
                var descriptor = new OpenIddictScopeDescriptor
                {
                    Name = ApplicationConstants.Scopes.AdminApplication,
                    Resources =
                    {
                        ApplicationConstants.Resources.AdminApi
                    }
                };


                await manager.CreateAsync(descriptor);
            }

            if (await manager.FindByNameAsync(ApplicationConstants.Scopes.AtmApplication) == null)
            {
                var descriptor = new OpenIddictScopeDescriptor
                {
                    Name = ApplicationConstants.Scopes.AtmApplication,
                    Resources =
                    {
                        ApplicationConstants.Resources.AtmApi
                    }
                };

                await manager.CreateAsync(descriptor);
            }
        }

        private async Task RegisterApplication(CancellationToken cancellationToken, IServiceScope scope)
        {
            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("postman1", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "postman1",
                    ClientSecret = "postman-secret1",
                    DisplayName = "Postman-01",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.Endpoints.Introspection,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles,
                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Prefixes.Scope + ApplicationConstants.Scopes.AdminApplication,
                        
                    },
                    
                    
                }, cancellationToken);
            }
            
            if (await manager.FindByClientIdAsync("postman2", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "postman2",
                    ClientSecret = "postman-secret2",
                    DisplayName = "Postman-02",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Introspection,
                    },
                    
                    
                }, cancellationToken);
            }
            
            if (await manager.FindByClientIdAsync("postman2", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "postman2",
                    ClientSecret = "postman-secret2",
                    DisplayName = "Postman-02",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Introspection,
                    },
                    
                    
                }, cancellationToken);
            }
            
            if (await manager.FindByClientIdAsync("postman3", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "postman3",
                    ClientSecret = "postman-secret3",
                    DisplayName = "Postman-03",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Introspection,
                        
                    },
                    
                    
                    
                }, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}