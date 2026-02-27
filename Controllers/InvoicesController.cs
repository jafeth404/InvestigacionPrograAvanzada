using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using InvoiceManager.Data;
using InvoiceManager.Models;
using System.Threading.Tasks;
using System.Linq;

namespace InvoiceManager.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Invoices
        public async Task<IActionResult> Index()
        {
            var invoices = await _context.Invoices
                .Include(i => i.Customer)
                .ToListAsync();

            return View(invoices);
        }

        // GET: /Invoices/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Lineas)
                .Include(i => i.Pagos)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: /Invoices/Create
        // GET: Invoices/Create
        public IActionResult Create()
        {
            // Llenar el combo de clientes
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,FechaEmision,FechaLimite,MontoTotal,Estado")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                // 👇 AQUI LA MAGIA
                if (string.IsNullOrWhiteSpace(invoice.Estado))
                {
                    invoice.Estado = "Pendiente"; // o "Parcial", lo que quieras
                }

                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers.ToList(), "Id", "Nombre", invoice.CustomerId);
            return View(invoice);
        }
    }
}