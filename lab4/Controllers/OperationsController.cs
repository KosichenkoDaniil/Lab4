using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab4;

namespace lab4.Controllers
{
    public class OperationsController : Controller
    {
        private readonly BankDeposits1Context _context;

        public OperationsController(BankDeposits1Context context)
        {
            _context = context;
        }

        // GET: Operations
        public async Task<IActionResult> Index()
        {
            var bankDeposits1Context = _context.Operations.Include(o => o.Deposit).Include(o => o.Emploee).Include(o => o.Investors);
            return View(await bankDeposits1Context.ToListAsync());
        }

        // GET: Operations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Operations == null)
            {
                return NotFound();
            }

            var operation = await _context.Operations
                .Include(o => o.Deposit)
                .Include(o => o.Emploee)
                .Include(o => o.Investors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        // GET: Operations/Create
        public IActionResult Create()
        {
            ViewData["Depositid"] = new SelectList(_context.Deposits, "Id", "Id");
            ViewData["Emploeeid"] = new SelectList(_context.Emploees, "Id", "Name + Surname + Middlename");
            ViewData["Investorsid"] = new SelectList(_context.Investors, "Id", "Name + Surname + Middlename");
            return View();
        }

        // POST: Operations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Investorsid,Depositdate,Returndate,Depositid,Depositamount,Refundamount,Returnstamp,Emploeeid")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Depositid"] = new SelectList(_context.Deposits, "Id", "Id", operation.Depositid);
            ViewData["Emploeeid"] = new SelectList(_context.Emploees, "Id", "Name + Surname + Middlename", operation.Emploeeid);
            ViewData["Investorsid"] = new SelectList(_context.Investors, "Id", "Name + Surname + Middlename", operation.Investorsid);
            return View(operation);
        }

        // GET: Operations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Operations == null)
            {
                return NotFound();
            }

            var operation = await _context.Operations.FindAsync(id);
            if (operation == null)
            {
                return NotFound();
            }
            ViewData["Depositid"] = new SelectList(_context.Deposits, "Id", "Id", operation.Depositid);
            ViewData["Emploeeid"] = new SelectList(_context.Emploees, "Id", "Name + Surname + Middlename", operation.Emploeeid);
            ViewData["Investorsid"] = new SelectList(_context.Investors, "Id", "Name + Surname + Middlename", operation.Investorsid);
            return View(operation);
        }

        // POST: Operations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Investorsid,Depositdate,Returndate,Depositid,Depositamount,Refundamount,Returnstamp,Emploeeid")] Operation operation)
        {
            if (id != operation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationExists(operation.Id))
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
            ViewData["Depositid"] = new SelectList(_context.Deposits, "Id", "Id", operation.Depositid);
            ViewData["Emploeeid"] = new SelectList(_context.Emploees, "Id", "Name + Surname + Middlename", operation.Emploeeid);
            ViewData["Investorsid"] = new SelectList(_context.Investors, "Id", "Name + Surname + Middlename", operation.Investorsid);
            return View(operation);
        }

        // GET: Operations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Operations == null)
            {
                return NotFound();
            }

            var operation = await _context.Operations
                .Include(o => o.Deposit)
                .Include(o => o.Emploee)
                .Include(o => o.Investors)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        // POST: Operations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Operations == null)
            {
                return Problem("Entity set 'BankDeposits1Context.Operations'  is null.");
            }
            var operation = await _context.Operations.FindAsync(id);
            if (operation != null)
            {
                _context.Operations.Remove(operation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperationExists(int id)
        {
          return (_context.Operations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
