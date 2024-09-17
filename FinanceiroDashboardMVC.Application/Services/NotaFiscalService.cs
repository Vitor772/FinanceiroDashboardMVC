using FinanceiroDashboardMVC.Domain.Entities;
using FinanceiroDashboardMVC.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;  // Necessário para usar Sum
using System.Threading.Tasks;

namespace FinanceiroDashboardMVC.Application.Services
{
    public class NotaFiscalService
    {
        private readonly INotaFiscalRepository _notaFiscalRepository;

        public NotaFiscalService(INotaFiscalRepository notaFiscalRepository)
        {
            _notaFiscalRepository = notaFiscalRepository;
        }

        // Retorna todas as notas fiscais
        public async Task<IEnumerable<NotaFiscal>> GetAllNotasAsync()
        {
            return await _notaFiscalRepository.GetAllAsync();
        }

        // Retorna uma nota fiscal pelo ID
        public async Task<NotaFiscal> GetNotaByIdAsync(int id)
        {
            return await _notaFiscalRepository.GetByIdAsync(id);
        }

        // Adiciona uma nova nota fiscal
        public async Task AddNotaAsync(NotaFiscal notaFiscal)
        {
            await _notaFiscalRepository.AddAsync(notaFiscal);
        }

        // Atualiza uma nota fiscal existente
        public async Task UpdateNotaAsync(NotaFiscal notaFiscal)
        {
            await _notaFiscalRepository.UpdateAsync(notaFiscal);
        }

        // Exclui uma nota fiscal pelo ID
        public async Task DeleteNotaAsync(int id)
        {
            await _notaFiscalRepository.DeleteAsync(id);
        }

        // Retorna o valor total de todas as notas emitidas
        public async Task<decimal> GetTotalValorEmitidasAsync()
        {
            var notas = await _notaFiscalRepository.GetAllAsync();
            return notas.Sum(n => n.Valor);
        }

        // Retorna o valor total de notas fiscais pagas
        public async Task<decimal> GetTotalValorPagasAsync()
        {
            var notas = await _notaFiscalRepository.GetAllAsync();
            return notas.Where(n => n.Status == "paga").Sum(n => n.Valor);
        }

        // Retorna o valor total de notas sem cobrança
        public async Task<decimal> GetTotalValorSemCobrancaAsync()
        {
            var notas = await _notaFiscalRepository.GetAllAsync();
            return notas.Where(n => n.Status == "sem_cobranca").Sum(n => n.Valor);
        }

        // Retorna o valor total de notas inadimplentes (vencidas e não pagas)
        public async Task<decimal> GetTotalValorInadimplenciaAsync()
        {
            var notas = await _notaFiscalRepository.GetAllAsync();
            return notas.Where(n => n.Status == "vencida").Sum(n => n.Valor);
        }

        // Retorna o valor total de notas a vencer
        public async Task<decimal> GetTotalValorAVencerAsync()
        {
            var notas = await _notaFiscalRepository.GetAllAsync();
            return notas.Where(n => n.Status == "a_vencer").Sum(n => n.Valor);
        }
    }
}
