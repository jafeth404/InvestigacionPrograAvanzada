namespace InvoiceManager.Models
{
    public class Payment
    {
        public int Id { get; set; }

        // Relación con factura
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string Metodo { get; set; }
    }
}