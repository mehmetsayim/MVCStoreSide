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
            context.Database.Migrate();
            return builder;
        
        }
    }
}
