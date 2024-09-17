using FinanceiroDashboardMVC.Application.Services;
using FinanceiroDashboardMVC.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceiroDashboardMVC.Presentation.Controllers
{
    public class NotaFiscalController : Controller
    {
        private readonly NotaFiscalService _notaFiscalService;

        public NotaFiscalController(NotaFiscalService notaFiscalService)
        {
            _notaFiscalService = notaFiscalService;
        }

        // Lista todas as notas fiscais com filtros
        public async Task<IActionResult> Index(DateTime? filtroMesEmissao, DateTime? filtroMesCobranca, DateTime? filtroMesPagamento, string filtroStatus)
        {
            var notasFiscais = await _notaFiscalService.GetAllNotasAsync();

            // Filtrar pelo mês de emissão
            if (filtroMesEmissao.HasValue)
            {
                notasFiscais = notasFiscais.Where(n => n.Data.Month == filtroMesEmissao.Value.Month && n.Data.Year == filtroMesEmissao.Value.Year);
            }

            // Filtrar pelo mês de cobrança
            if (filtroMesCobranca.HasValue)
            {
                notasFiscais = notasFiscais.Where(n => n.DataCobranca.HasValue && n.DataCobranca.Value.Month == filtroMesCobranca.Value.Month && n.DataCobranca.Value.Year == filtroMesCobranca.Value.Year);
            }

            // Filtrar pelo mês de pagamento
            if (filtroMesPagamento.HasValue)
            {
                notasFiscais = notasFiscais.Where(n => n.DataPagamento.HasValue && n.DataPagamento.Value.Month == filtroMesPagamento.Value.Month && n.DataPagamento.Value.Year == filtroMesPagamento.Value.Year);
            }

            // Filtrar pelo status da nota
            if (!string.IsNullOrEmpty(filtroStatus))
            {
                notasFiscais = notasFiscais.Where(n => n.Status.Equals(filtroStatus, StringComparison.OrdinalIgnoreCase));
            }

            // Passando os valores dos filtros para a View
            ViewBag.FiltroMesEmissao = filtroMesEmissao?.ToString("yyyy-MM");
            ViewBag.FiltroMesCobranca = filtroMesCobranca?.ToString("yyyy-MM");
            ViewBag.FiltroMesPagamento = filtroMesPagamento?.ToString("yyyy-MM");
            ViewBag.FiltroStatus = filtroStatus;

            return View(notasFiscais);
        }

        public IActionResult Create()
        {
            return View();
        }

        // Cria uma nova nota fiscal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NotaFiscal notaFiscal)
        {
            if (ModelState.IsValid)
            {
                await _notaFiscalService.AddNotaAsync(notaFiscal);
                return RedirectToAction(nameof(Index));
            }
            return View(notaFiscal);
        }

        public async Task<IActionResult> Details(int id)
        {
            var notaFiscal = await _notaFiscalService.GetNotaByIdAsync(id);
            if (notaFiscal == null)
            {
                return NotFound();
            }
            return View(notaFiscal);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var notaFiscal = await _notaFiscalService.GetNotaByIdAsync(id);
            if (notaFiscal == null)
            {
                return NotFound();
            }
            return View(notaFiscal);
        }

        // Atualiza uma nota fiscal existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NotaFiscal notaFiscal)
        {
            if (id != notaFiscal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _notaFiscalService.UpdateNotaAsync(notaFiscal);
                return RedirectToAction(nameof(Index));
            }
            return View(notaFiscal);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var notaFiscal = await _notaFiscalService.GetNotaByIdAsync(id);
            if (notaFiscal == null)
            {
                return NotFound();
            }

            return View(notaFiscal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _notaFiscalService.DeleteNotaAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
