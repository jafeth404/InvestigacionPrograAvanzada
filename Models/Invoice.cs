using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace InvoiceManager.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        // Relación con cliente
        [Required]                    // aquí sí queremos que lo pida
        public int CustomerId { get; set; }

        [ValidateNever]               // navegación, no la validamos en el form
        public Customer? Customer { get; set; }

        [Required]
        public DateTime FechaEmision { get; set; }

        [Required]
        public DateTime FechaLimite { get; set; }

        // Total calculado de la factura
        public decimal MontoTotal { get; set; }

        // Pendiente / Parcial / Pagada (opcional en el form)
        public string? Estado { get; set; }

        // Lista de líneas dentro de la factura
        [ValidateNever]
        public List<InvoiceLine> Lineas { get; set; } = new();

        // Lista de pagos aplicados a la factura
        [ValidateNever]
        public List<Payment> Pagos { get; set; } = new();
    }
}