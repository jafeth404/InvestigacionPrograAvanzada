namespace InvoiceManager.Models
{
    public class InvoiceLine
    {
        public int Id { get; set; }

        // Relación con factura
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public string Descripcion{ get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnidad { get; set; }

        // Total de esa línea
        public decimal LineTotal => Cantidad * PrecioUnidad;
    }
}