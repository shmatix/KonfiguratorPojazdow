using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KonfiguratorPojazdow.Data;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace KonfiguratorPojazdow.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PaintsController : Controller
    {
        private readonly ConfigurationContext _context;

        public PaintsController(ConfigurationContext context)
        {
            _context = context;
        }

        // GET: Paints
        public async Task<IActionResult> Index()
        {
              return _context.Paints != null ? 
                          View(await _context.Paints.ToListAsync()) :
                          Problem("Entity set 'ConfigurationContext.Paints'  is null.");
        }

        // GET: Paints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Paints == null)
            {
                return NotFound();
            }

            var paint = await _context.Paints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paint == null)
            {
                return NotFound();
            }

            return View(paint);
        }

        // GET: Paints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Color,Type,Price")] Paint paint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paint);
        }

        // GET: Paints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Paints == null)
            {
                return NotFound();
            }

            var paint = await _context.Paints.FindAsync(id);
            if (paint == null)
            {
                return NotFound();
            }
            return View(paint);
        }

        // POST: Paints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Color,Type,Price")] Paint paint)
        {
            if (id != paint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaintExists(paint.Id))
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
            return View(paint);
        }

        // GET: Paints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Paints == null)
            {
                return NotFound();
            }

            var paint = await _context.Paints
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paint == null)
            {
                return NotFound();
            }

            return View(paint);
        }

        // POST: Paints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Paints == null)
            {
                return Problem("Entity set 'ConfigurationContext.Paints'  is null.");
            }
            var paint = await _context.Paints.FindAsync(id);
            if (paint != null)
            {
                _context.Paints.Remove(paint);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaintExists(int id)
        {
          return (_context.Paints?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
