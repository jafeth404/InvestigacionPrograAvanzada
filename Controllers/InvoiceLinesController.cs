using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvoiceManager.Data;
using InvoiceManager.Models;
using System.Threading.Tasks;
using System.Linq;

namespace InvoiceManager.Controllers
{
    public class InvoiceLinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceLinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /InvoiceLines/Create?invoiceId=5
        public async Task<IActionResult> Create(int invoiceId)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);
            if (invoice == null)
            {
                return NotFound();
            }

            var line = new InvoiceLine
            {
                InvoiceId = invoiceId
            };

            ViewBag.InvoiceId = invoiceId;
            return View(line);
        }

        // POST: /InvoiceLines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceLine line)
        {
            // Ignoramos navegación
            ModelState.Remove("Invoice");

            if (!ModelState.IsValid)
            {
                ViewBag.InvoiceId = line.InvoiceId;
                return View(line);
            }

            _context.InvoiceLines.Add(line);
            await _context.SaveChangesAsync();

            // Recalcular total de la factura
            var invoice = await _context.Invoices
                .Include(i => i.Lineas)
                .FirstAsync(i => i.Id == line.InvoiceId);

            invoice.MontoTotal = invoice.Lineas.Sum(l => l.Cantidad * l.PrecioUnidad);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Invoices", new { id = line.InvoiceId });
        }
    }
}