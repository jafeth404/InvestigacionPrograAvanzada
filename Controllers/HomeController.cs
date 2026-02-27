using InvoiceManager.Data;
using InvoiceManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var totalClientes = await _context.Customers.CountAsync();
            var totalFacturas = await _context.Invoices.CountAsync();
            var facturasPendientes = await _context.Invoices
                .CountAsync(i => i.Estado == "Pendiente");
            var facturasPagadas = await _context.Invoices
                .CountAsync(i => i.Estado == "Pagada");

            var vm = new DashboardViewModel
            {
                TotalClientes = totalClientes,
                TotalFacturas = totalFacturas,
                FacturasPendientes = facturasPendientes,
                FacturasPagadas = facturasPagadas
            };

            return View(vm);
        }
    }
}