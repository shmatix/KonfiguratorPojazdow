using KonfiguratorPojazdow.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Konfiguracja us�ugi DbContext dla klasy MapContext, u�ywaj�c po��czenia o nazwie "DangerMapConnection" pobranego z konfiguracji
builder.Services.AddDbContext<ConfigurationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConfigurationConnection")));

builder.Services.AddControllersWithViews();

// Utworzenie obiektu CultureInfo dla j�zyka angielskiego (en-US)
var cultureInfo = new CultureInfo("en-US");

// Konfiguracja symbolu waluty na "�"
cultureInfo.NumberFormat.CurrencySymbol = "�";

// Konfiguracja separatora dziesi�tnego na "."
cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

// Ustawienie domy�lnej kultury dla bie��cego w�tku na cultureInfo
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;

// Ustawienie domy�lnej kultury interfejsu u�ytkownika dla bie��cego w�tku na cultureInfo
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await SeedRoles.Initialize(services);
    await SeedUsers.InitializeAsync(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
