using FinanceiroDashboardMVC.Domain.Entities;
using FinanceiroDashboardMVC.Domain.Interfaces;
using FinanceiroDashboardMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceiroDashboardMVC.Infrastructure.Repositories
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        private readonly ApplicationDbContext _context;

        public NotaFiscalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotaFiscal>> GetAllAsync()
        {
            return await _context.NotasFiscais.ToListAsync();
        }

        public async Task<NotaFiscal> GetByIdAsync(int id)
        {
            return await _context.NotasFiscais.FindAsync(id);
        }

        public async Task AddAsync(NotaFiscal notaFiscal)
        {
            await _context.NotasFiscais.AddAsync(notaFiscal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(NotaFiscal notaFiscal)
        {
            _context.NotasFiscais.Update(notaFiscal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var notaFiscal = await GetByIdAsync(id);
            if (notaFiscal != null)
            {
                _context.NotasFiscais.Remove(notaFiscal);
                await _context.SaveChangesAsync();
            }
        }

        // Retorna o valor total de todas as notas emitidas
        public async Task<decimal> GetTotalValorEmitidasAsync()
        {
            return await _context.NotasFiscais.SumAsync(nf => nf.Valor);
        }

        // Retorna o valor total de notas pagas
        public async Task<decimal> GetTotalValorPagasAsync()
        {
            return await _context.NotasFiscais
                .Where(nf => nf.Status == "paga")
                .SumAsync(nf => nf.Valor);
        }

        // Retorna o valor total de notas sem cobrança
        public async Task<decimal> GetTotalValorSemCobrancaAsync()
        {
            return await _context.NotasFiscais
                .Where(nf => nf.Status == "sem_cobranca")
                .SumAsync(nf => nf.Valor);
        }

        // Retorna o valor total de notas inadimplentes (vencidas e não pagas)
        public async Task<decimal> GetTotalValorInadimplenciaAsync()
        {
            return await _context.NotasFiscais
                .Where(nf => nf.Status == "vencida")
                .SumAsync(nf => nf.Valor);
        }

        // Retorna o valor total de notas a vencer
        public async Task<decimal> GetTotalValorAVencerAsync()
        {
            return await _context.NotasFiscais
                .Where(nf => nf.Status == "a_vencer")
                .SumAsync(nf => nf.Valor);
        }
    }
}
