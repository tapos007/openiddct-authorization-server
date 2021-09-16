using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer.Infrastructure
{
    
    public class SeedUserWithRole 
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }
    public class UserSeedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public UserSeedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            await AddRoleInDb(scope, cancellationToken);
            await AddUserInDb(scope, cancellationToken);
            
        }
       

        private async Task AddUserInDb(IServiceScope scope, CancellationToken cancellationToken)
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var userList = new List<SeedUserWithRole>()
            {
                new SeedUserWithRole()
                {
                    Name = "admin01@gmail.com",
                    Role = "admin"
                },
                new SeedUserWithRole()
                {
                    Name = "admin02@gmail.com",
                    Role = "admin"
                },
                new SeedUserWithRole()
                {
                    Name = "checker01@gmail.com",
                    Role = "checker"
                },
                new SeedUserWithRole()
                {
                    Name = "checker02@gmail.com",
                    Role = "checker"
                },
                new SeedUserWithRole()
                {
                    Name = "maker01@gmail.com",
                    Role = "maker"
                },
                new SeedUserWithRole()
                {
                    Name = "maker02@gmail.com",
                    Role = "maker"
                },
                new SeedUserWithRole()
                {
                    Name = "customer01@gmail.com",
                    Role = "customer"
                },
                new SeedUserWithRole()
                {
                    Name = "customer02@gmail.com",
                    Role = "customer"
                },
                new SeedUserWithRole()
                {
                    Name = "manager01@gmail.com",
                    Role = "manager"
                },
                new SeedUserWithRole()
                {
                    Name = "manager02@gmail.com",
                    Role = "manager"
                }
            };
            foreach (var user in userList)
            {
                var userExists = await userManager.FindByEmailAsync(user.Name);

                if (userExists == null)
                {
                    var appUser = new ApplicationUser()
                    {
                        UserName = user.Name,
                        Email = user.Name,
                        FullName = user.Name,

                    };
                   var result =  await userManager.CreateAsync(appUser, "Rcis123$..");
                   if (result.Succeeded)
                   {
                       await userManager.AddToRoleAsync(appUser, user.Role);
                   }
                }
            }
        }

        private async Task AddRoleInDb(IServiceScope scope, CancellationToken cancellationToken)
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var roleList = new List<string>()
            {
                "admin",
                "checker",
                "maker",
                "customer",
                "manager"
                
            };

            foreach (var role in roleList)
            {
                var isRoleExists = await roleManager.FindByNameAsync(role);
                if (isRoleExists == null)
                {
                    await roleManager.CreateAsync(new ApplicationRole()
                    {
                        Name = role
                    });
                }
            }
            
            
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}