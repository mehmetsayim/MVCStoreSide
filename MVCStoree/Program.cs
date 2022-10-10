using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCStoreData;
using MVCStoreeWeb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

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

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
{
    config.Password.RequiredLength = 6;
    config.Password.RequireLowercase = true;
    config.Password.RequireUppercase = true;
    config.Password.RequireNonAlphanumeric = true;
    config.Password.RequiredUniqueChars = 3;
        

    config.SignIn.RequireConfirmedPhoneNumber = false;
    config.SignIn.RequireConfirmedEmail = true;
    config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);

})
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAuthentication().AddCookie(); 
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
 
app.UseAuthentication();

app.UseAuthorization();

app.UseMVCStoree();

app.MapAreaControllerRoute("admin", "Admin", "admin/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
