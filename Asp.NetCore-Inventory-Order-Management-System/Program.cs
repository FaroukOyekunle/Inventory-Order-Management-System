using Newtonsoft.Json;
using Microsoft.AspNetCore;
using System.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Asp.NetCore_Inventory_Order_Management_System.Data;
using Asp.NetCore_Inventory_Order_Management_System.Models;
using Asp.NetCore_Inventory_Order_Management_System.Services;

var builder = WebApplication.CreateBuilder(args);

//Adding DB Context with MSSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
/*  b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));*/

// Get Identity Default Options
IConfigurationSection identityDefaultOptionsConfigurationSection = builder.Configuration.GetSection("IdentityDefaultOptions");

builder.Services.Configure<IdentityDefaultOptions>(identityDefaultOptionsConfigurationSection);

var identityDefaultOptions = identityDefaultOptionsConfigurationSection.Get<IdentityDefaultOptions>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = identityDefaultOptions.PasswordRequireDigit;
    options.Password.RequiredLength = identityDefaultOptions.PasswordRequiredLength;
    options.Password.RequireNonAlphanumeric = identityDefaultOptions.PasswordRequireNonAlphanumeric;
    options.Password.RequireUppercase = identityDefaultOptions.PasswordRequireUppercase;
    options.Password.RequireLowercase = identityDefaultOptions.PasswordRequireLowercase;
    options.Password.RequiredUniqueChars = identityDefaultOptions.PasswordRequiredUniqueChars;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identityDefaultOptions.LockoutDefaultLockoutTimeSpanInMinutes);
    options.Lockout.MaxFailedAccessAttempts = identityDefaultOptions.LockoutMaxFailedAccessAttempts;
    options.Lockout.AllowedForNewUsers = identityDefaultOptions.LockoutAllowedForNewUsers;

    // User settings
    options.User.RequireUniqueEmail = identityDefaultOptions.UserRequireUniqueEmail;

    // email confirmation require
    options.SignIn.RequireConfirmedEmail = identityDefaultOptions.SignInRequireConfirmedEmail;
})
   .AddEntityFrameworkStores<ApplicationDbContext>()
   .AddDefaultTokenProviders();

// cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = identityDefaultOptions.CookieHttpOnly;
/*    options.Cookie.Expiration = TimeSpan.FromDays(identityDefaultOptions.CookieExpiration);*/
    options.ExpireTimeSpan = TimeSpan.FromDays(50);
    options.LoginPath = identityDefaultOptions.LoginPath; // If the LoginPath is not set here, ASP.NET Core will set the default to /Account/Login
    options.LogoutPath = identityDefaultOptions.LogoutPath; // If the LogoutPath is not set here, ASP.NET Core will set the default to /Account/Logout
    options.AccessDeniedPath = identityDefaultOptions.AccessDeniedPath; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
    options.SlidingExpiration = identityDefaultOptions.SlidingExpiration;
});

// Get SendGrid configuration options
builder.Services.Configure<SendGridOptions>(builder.Configuration.GetSection("SendGridOptions"));

// Get SMTP configuration options
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("SmtpOptions"));

// Get Super Admin Default options
builder.Services.Configure<SuperAdminDefaultOptions>(builder.Configuration.GetSection("SuperAdminDefaultOptions"));

// Add email services.
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddTransient<INumberSequence, Asp.NetCore_Inventory_Order_Management_System.Services.NumberSequence>();

builder.Services.AddTransient<IRoles, Roles>();

builder.Services.AddTransient<IFunctional, Functional>();

builder.Services.AddMvc()
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    //pascal case json
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});
builder.Services.AddControllersWithViews();

// This method gets called by the runtime. Use this method to add services to the container.

var app = builder.Build();

// This method gets called by the runtime. Use this method to Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    SeedDatabase();
    app.UseDeveloperExceptionPage();
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserRole}/{action=UserProfile}/{id?}");

app.Run();

void SeedDatabase() //can be placed at the very bottom under app.Run()
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var functional = services.GetRequiredService<IFunctional>();

            DbInitializer.Initialize(context, functional).Wait();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }
}
