using FinanceiroDashboardMVC.Application.Services;
using FinanceiroDashboardMVC.Domain.Interfaces;
using FinanceiroDashboardMVC.Infrastructure.Data;
using FinanceiroDashboardMVC.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do banco de dados e do assembly para migra��es
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("FinanceiroDashboardMVC.Presentation") // Especificando o assembly de migra��es
    ));

builder.Services.AddScoped<NotaFiscalService>();

// Registrar o reposit�rio
builder.Services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();

// Outros servi�os...
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
