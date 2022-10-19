using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCStoreData;
using MVCStoreeWeb;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

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
    config.Password.RequiredLength = builder.Configuration.GetValue<int>("PasswordSettings:RequiredLength");
    config.Password.RequireLowercase = builder.Configuration.GetValue<bool>("PasswordSettings:RequireLowercase"); 
    config.Password.RequireUppercase = builder.Configuration.GetValue<bool>("PasswordSettings:RequireUppercase");
    config.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("PasswordSettings:RequireNonAlphanumeric");
    config.Password.RequiredUniqueChars = builder.Configuration.GetValue<int>("PasswordSe   ttings:RequiredUniqueChars");

    config.SignIn.RequireConfirmedPhoneNumber = false;
    config.SignIn.RequireConfirmedEmail = true;

    config.Lockout.MaxFailedAccessAttempts = 3;    
    config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);

})



    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<CustomIdentityErrorDescriber>();
builder.Services.AddAuthentication().AddCookie(); 

builder.Services.AddMailKit(optionBuilder =>
{
    optionBuilder.UseMailKit(new MailKitOptions()
    {
        //get options from sercets.json
        Server =  builder.Configuration.GetValue<string>("EmailSettings:Server"),
        Port = builder.Configuration.GetValue<int>("EmailSettings:Port"),
        SenderName = builder.Configuration.GetValue<string>("EmailSettings:SenderName"),
        SenderEmail = builder.Configuration.GetValue<string>("EmailSettings:SenderEmail"),

        // can be optional with no authentication 
        Account = builder.Configuration.GetValue<string>("EmailSettings:Account"),
        Password = builder.Configuration.GetValue<string>("EmailSettings:Password"),
        // enable ssl or tls
        Security = builder.Configuration.GetValue<bool>("EmailSettings:Security"),
    });
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
 
app.UseAuthentication();

app.UseAuthorization();

app.UseMVCStoree();

app.MapAreaControllerRoute("admin", "Admin", "admin/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
