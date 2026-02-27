namespace InvoiceManager.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalClientes { get; set; }
        public int TotalFacturas { get; set; }
        public int FacturasPendientes { get; set; }
        public int FacturasPagadas { get; set; }
    }
}