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
    public class EnginesController : Controller
    {
        private readonly ConfigurationContext _context;

        public EnginesController(ConfigurationContext context)
        {
            _context = context;
        }

        // GET: Engines
        public async Task<IActionResult> Index()
        {
              return _context.Engines != null ? 
                          View(await _context.Engines.ToListAsync()) :
                          Problem("Entity set 'ConfigurationContext.Engines'  is null.");
        }

        // GET: Engines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Engines == null)
            {
                return NotFound();
            }

            var engine = await _context.Engines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (engine == null)
            {
                return NotFound();
            }

            return View(engine);
        }

        // GET: Engines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Engines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Capacity,Fuel,Price,Hybrid")] Engine engine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(engine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(engine);
        }

        // GET: Engines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Engines == null)
            {
                return NotFound();
            }

            var engine = await _context.Engines.FindAsync(id);
            if (engine == null)
            {
                return NotFound();
            }
            return View(engine);
        }

        // POST: Engines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Capacity,Fuel,Price,Hybrid")] Engine engine)
        {
            if (id != engine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(engine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EngineExists(engine.Id))
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
            return View(engine);
        }

        // GET: Engines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Engines == null)
            {
                return NotFound();
            }

            var engine = await _context.Engines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (engine == null)
            {
                return NotFound();
            }

            return View(engine);
        }

        // POST: Engines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Engines == null)
            {
                return Problem("Entity set 'ConfigurationContext.Engines'  is null.");
            }
            var engine = await _context.Engines.FindAsync(id);
            if (engine != null)
            {
                _context.Engines.Remove(engine);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EngineExists(int id)
        {
          return (_context.Engines?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
