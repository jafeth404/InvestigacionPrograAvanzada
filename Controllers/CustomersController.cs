using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvoiceManager.Data;
using InvoiceManager.Models;
using System.Threading.Tasks;
using System.Linq;

namespace InvoiceManager.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Customers
        // GET: /Customers
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers);
        }
        // GET: /Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        // GET: /Customers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

// POST: /Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            _context.Update(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

// GET: /Customers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

// POST: /Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}