using FinanceiroDashboardMVC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceiroDashboardMVC.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<NotaFiscal> NotasFiscais { get; set; }
    }
}
