using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvoiceManager.Data;
using InvoiceManager.Models;
using System.Threading.Tasks;
using System.Linq;

namespace InvoiceManager.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Payments/Create?invoiceId=5
        public async Task<IActionResult> Create(int invoiceId)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);
            if (invoice == null)
            {
                return NotFound();
            }

            var payment = new Payment
            {
                InvoiceId = invoiceId,
                FechaPago = DateTime.Today
            };

            ViewBag.InvoiceId = invoiceId;
            return View(payment);
        }

        // POST: /Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Payment payment)
        {
            // Ignoramos navegación
            ModelState.Remove("Invoice");

            if (payment.Monto <= 0)
            {
                ModelState.AddModelError("Amount", "El monto debe ser mayor que 0.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.InvoiceId = payment.InvoiceId;
                return View(payment);
            }

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // Recalcular pagos y estado de la factura
            var invoice = await _context.Invoices
                .Include(i => i.Pagos)
                .FirstAsync(i => i.Id == payment.InvoiceId);

            var totalPagado = invoice.Pagos.Sum(p => p.Monto);

            if (totalPagado >= invoice.MontoTotal && invoice.MontoTotal > 0)
            {
                invoice.Estado = "Pagada";
            }
            else if (totalPagado > 0)
            {
                invoice.Estado = "Parcial";
            }
            else
            {
                invoice.Estado = "Pendiente";
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Invoices", new { id = payment.InvoiceId });
        }
    }
}