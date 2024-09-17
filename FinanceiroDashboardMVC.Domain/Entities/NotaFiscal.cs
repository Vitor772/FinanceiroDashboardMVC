using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceiroDashboardMVC.Domain.Entities
{
    public class NotaFiscal
    {
        public int Id { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public string Categoria { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime Data { get; set; }  // Data de emissão

        public DateTime? DataCobranca { get; set; }  // Data de cobrança (opcional)

        public DateTime? DataPagamento { get; set; }  // Data de pagamento (opcional)
    }
}
