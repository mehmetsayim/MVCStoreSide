using Microsoft.EntityFrameworkCore;
using MVCStoreData;
using MVCStoreeWeb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(config => {
    config.UseLazyLoadingProxies();
    var provider = builder.Configuration.GetValue<string>("DpProvider");

    switch (provider)
    {
        case "SqlServer":
         config.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServer"),
        options => options.MigrationsAssembly("MigrationSqlServer")
        );
        break;
        case "NpgSql":
        default:
            config.UseNpgsql(builder.Configuration.GetConnectionString("Npgsql"),
        options => options.MigrationsAssembly("MigrationNpgsqlServer")
      );
            break;
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMVCStoree();

app.MapAreaControllerRoute("admin", "Admin", "admin/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
