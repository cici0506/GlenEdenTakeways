using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GlenEdenTakeways.Areas.Identity.Data;
using GlenEdenTakeways.Models;
using Microsoft.AspNetCore.Authorization;

namespace GlenEdenTakeways.Controllers
{
    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly IdentityContext _context;

        public PaymentsController(IdentityContext context)
        {
            _context = context;
        }
        // GET: Payments
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Payment == null)
            {
                return Problem("Entity set 'IdentityContext'.Payments' is null");
            }

            IQueryable<Payment> payments = _context.Payment; // Use IQueryable instead of var for explicit typing

            if (!String.IsNullOrEmpty(searchString)) // search filter for payment dates
            {
                payments = payments.Where(p => p.PaymentDate.ToString().Contains(searchString));
            }

            payments = payments.Include(p => p.Customer).Include(p => p.PaymentType); // Include related entities

            var paymentList = await payments.ToListAsync();
            return View(paymentList);
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FullName"); // display customer full name instead of customer id
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,CustomerId,OrderId,PaymentDate,TotalAmount,PaymentTypeId")] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,CustomerId,OrderId,PaymentDate,TotalAmount,PaymentTypeId")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Payment == null)
            {
                return Problem("Entity set 'IdentityContext.Payment'  is null.");
            }
            var payment = await _context.Payment.FindAsync(id);
            if (payment != null)
            {
                _context.Payment.Remove(payment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
          return (_context.Payment?.Any(e => e.PaymentId == id)).GetValueOrDefault();
        }
    }
}
