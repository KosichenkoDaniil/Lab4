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
    public class EmploeesController : Controller
    {
        private readonly BankDeposits1Context _context;

        public EmploeesController(BankDeposits1Context context)
        {
            _context = context;
        }

        // GET: Emploees
        public async Task<IActionResult> Index()
        {
              return _context.Emploees != null ? 
                          View(await _context.Emploees.ToListAsync()) :
                          Problem("Entity set 'BankDeposits1Context.Emploees'  is null.");
        }

        // GET: Emploees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Emploees == null)
            {
                return NotFound();
            }

            var emploee = await _context.Emploees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emploee == null)
            {
                return NotFound();
            }

            return View(emploee);
        }

        // GET: Emploees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Emploees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Middlename,Post,Dob")] Emploee emploee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emploee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emploee);
        }

        // GET: Emploees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Emploees == null)
            {
                return NotFound();
            }

            var emploee = await _context.Emploees.FindAsync(id);
            if (emploee == null)
            {
                return NotFound();
            }
            return View(emploee);
        }

        // POST: Emploees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Middlename,Post,Dob")] Emploee emploee)
        {
            if (id != emploee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emploee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmploeeExists(emploee.Id))
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
            return View(emploee);
        }

        // GET: Emploees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Emploees == null)
            {
                return NotFound();
            }

            var emploee = await _context.Emploees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emploee == null)
            {
                return NotFound();
            }

            return View(emploee);
        }

        // POST: Emploees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Emploees == null)
            {
                return Problem("Entity set 'BankDeposits1Context.Emploees'  is null.");
            }
            var emploee = await _context.Emploees.FindAsync(id);
            if (emploee != null)
            {
                _context.Emploees.Remove(emploee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmploeeExists(int id)
        {
          return (_context.Emploees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
