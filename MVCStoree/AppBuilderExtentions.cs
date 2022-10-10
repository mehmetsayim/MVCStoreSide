using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCStoreData;

namespace MVCStoreeWeb
{
    public static class AppBuilderExtentions
    {
        public static IApplicationBuilder UseMVCStoree(this IApplicationBuilder builder)
        {

            using var scope = builder.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            context.Database.Migrate();

            new[]       
            {
                new ApplicationRole{Name="Administrators" },
                new ApplicationRole{Name="ProductManagers" },
                new ApplicationRole{Name="OrderManagers" },
                new ApplicationRole{Name="Members" }, 
            }
            .ToList()
            .ForEach(p => {
                if (!roleManager.RoleExistsAsync(p.Name).Result)               
                roleManager.CreateAsync(p).Wait();              
            });

            var user = new ApplicationUser

            {
                Name = config.GetValue<string>("DefaultUser:Name"),
                UserName = config.GetValue<string>("DefautUser:UserName"),
                Email = config.GetValue<string>("DefaultUser:Email"),
                EmailConfirmed = true


            };
            userManager.CreateAsync(user, config.GetValue<string>("DefaultUser:Password")).Wait();
            userManager.AddToRoleAsync(user, "Administrators").Wait();
            return builder;
        
        }
    }
}
