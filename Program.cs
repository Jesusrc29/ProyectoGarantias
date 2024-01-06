using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectGarantia.Data;
using ProyectGarantia.Data.Data_Acces;
using ProyectGarantia.Data.DataAcces;
using ProyectGarantia.Data.Interfaces;
using ProyectGarantia.Models;
using ProyectGarantia.Services;
using static ProyectGarantia.Data.ApplicationDbContext;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Usuario>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDADetalleLoteModelo, DADetalleLoteModelo>();
builder.Services.AddScoped<IDALote, DALote>();
builder.Services.AddScoped<IDAAgencia, DAAgencia>();
builder.Services.AddScoped<IDADocumento, DADocumento>();
builder.Services.AddScoped<IDAGarantia, DAGarantia>();
builder.Services.AddScoped<IHTTPRequest, HTTPRequestImpl>();

builder.Services.AddAuthorization(
    options => options.AddPolicy(
        "AllowLayoutOperador",
        policy => policy.RequireRole("Operador")));
builder.Services.AddAuthorization(
    options => options.AddPolicy(
        "AllowLayoutSupervisora",
        policy => policy.RequireRole("Supervisora")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Identity",
    areaName: "Identity",
    pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}");

app.MapRazorPages();



app.Run();
