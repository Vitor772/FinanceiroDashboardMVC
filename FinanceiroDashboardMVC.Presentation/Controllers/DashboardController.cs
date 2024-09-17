using FinanceiroDashboardMVC.Application.Services;
using FinanceiroDashboardMVC.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinanceiroDashboardMVC.Presentation.Controllers
{
    public class DashboardController : Controller
    {
        private readonly NotaFiscalService _notaFiscalService;

        public DashboardController(NotaFiscalService notaFiscalService)
        {
            _notaFiscalService = notaFiscalService;
        }

        // Exibe a lista de todas as notas fiscais e os valores totais por status
        public async Task<IActionResult> Index()
        {
            // Obter todas as notas fiscais
            var notasFiscais = await _notaFiscalService.GetAllNotasAsync();

            // Obter o valor total das notas emitidas
            var totalValorEmitidas = await _notaFiscalService.GetTotalValorEmitidasAsync();

            // Obter o valor total das notas pagas
            var totalValorPagas = await _notaFiscalService.GetTotalValorPagasAsync();

            // Obter o valor total das notas sem cobrança
            var totalValorSemCobranca = await _notaFiscalService.GetTotalValorSemCobrancaAsync();

            // Obter o valor total das notas inadimplentes (vencidas e não pagas)
            var totalValorInadimplencia = await _notaFiscalService.GetTotalValorInadimplenciaAsync();

            // Obter o valor total das notas a vencer
            var totalValorAVencer = await _notaFiscalService.GetTotalValorAVencerAsync();

            // Passar os valores para a View usando ViewBag
            ViewBag.TotalValorEmitidas = totalValorEmitidas;
            ViewBag.TotalValorPagas = totalValorPagas;
            ViewBag.TotalValorSemCobranca = totalValorSemCobranca;
            ViewBag.TotalValorInadimplencia = totalValorInadimplencia;
            ViewBag.TotalValorAVencer = totalValorAVencer;

            // Retornar a lista de notas para a View
            return View(notasFiscais);
        }

        // Exibe o formulário de criação de uma nova Nota Fiscal
        public IActionResult Create()
        {
            return View();
        }

        // Adiciona uma nova Nota Fiscal ao banco de dados
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

        // Exibe os detalhes de uma Nota Fiscal específica
        public async Task<IActionResult> Details(int id)
        {
            var notaFiscal = await _notaFiscalService.GetNotaByIdAsync(id);

            if (notaFiscal == null)
            {
                return NotFound();
            }

            return View(notaFiscal);
        }

        // Exibe o formulário para editar uma Nota Fiscal
        public async Task<IActionResult> Edit(int id)
        {
            var notaFiscal = await _notaFiscalService.GetNotaByIdAsync(id);
            if (notaFiscal == null)
            {
                return NotFound();
            }
            return View(notaFiscal);
        }

        // Atualiza os dados de uma Nota Fiscal existente
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
                try
                {
                    await _notaFiscalService.UpdateNotaAsync(notaFiscal);
                }
                catch
                {
                    if (!await NotaFiscalExists(notaFiscal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(notaFiscal);
        }

        // Exibe a confirmação de exclusão de uma Nota Fiscal
        public async Task<IActionResult> Delete(int id)
        {
            var notaFiscal = await _notaFiscalService.GetNotaByIdAsync(id);

            if (notaFiscal == null)
            {
                return NotFound();
            }

            return View(notaFiscal);
        }

        // Exclui uma Nota Fiscal do banco de dados
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _notaFiscalService.DeleteNotaAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Verifica se uma Nota Fiscal existe no banco de dados
        private async Task<bool> NotaFiscalExists(int id)
        {
            var notaFiscal = await _notaFiscalService.GetNotaByIdAsync(id);
            return notaFiscal != null;
        }
    }
}
