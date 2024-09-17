using FinanceiroDashboardMVC.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceiroDashboardMVC.Domain.Interfaces
{
    public interface INotaFiscalRepository
    {
        Task<IEnumerable<NotaFiscal>> GetAllAsync();
        Task<NotaFiscal> GetByIdAsync(int id);
        Task AddAsync(NotaFiscal notaFiscal);
        Task UpdateAsync(NotaFiscal notaFiscal);
        Task DeleteAsync(int id);

        // Métodos para obter valores totais das notas fiscais de acordo com o status
        Task<decimal> GetTotalValorEmitidasAsync();
        Task<decimal> GetTotalValorPagasAsync();
        Task<decimal> GetTotalValorSemCobrancaAsync();
        Task<decimal> GetTotalValorInadimplenciaAsync();
        Task<decimal> GetTotalValorAVencerAsync();
    }
}
