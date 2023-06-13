using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KonfiguratorPojazdow.Data;
using KonfiguratorPojazdow.Models;
using Microsoft.AspNetCore.Authorization;

namespace KonfiguratorPojazdow.Controllers
{
    [Authorize]
    public class UsersConfigurationsController : Controller
    {
        private readonly ConfigurationContext _context;

        public UsersConfigurationsController(ConfigurationContext context)
        {
            _context = context;
        }

        // GET: UsersConfigurations
        public async Task<IActionResult> Index()
        {
            var configurationContext = _context.Configurations
                .Where(w=>w.UserId == User.GetId())
                .Include(c => c.Car)
                .Include(c => c.Engine)
                .Include(c => c.Paint);
            return View(await configurationContext.ToListAsync());
        }

        // GET: UsersConfigurations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Configurations == null)
            {
                return NotFound();
            }

            var configuration = await _context.Configurations
                .Where(w => w.UserId == User.GetId())
                .Include(c => c.Car)
                .Include(c => c.Engine)
                .Include(c => c.Paint)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuration == null)
            {
                return NotFound();
            }

            return View(configuration);
        }

        // GET: UsersConfigurations/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Display");
            ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "Display");
            ViewData["PaintId"] = new SelectList(_context.Paints, "Id", "Display");
            return View();
        }

        // POST: UsersConfigurations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CarId,EngineId,PaintId,Interior,Rims")] Configuration configuration)
        {
            if (ModelState.IsValid)
            {
                configuration.UserId = User.GetId();
                _context.Add(configuration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Display", configuration.CarId);
            ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "Display", configuration.EngineId);
            ViewData["PaintId"] = new SelectList(_context.Paints, "Id", "Display", configuration.PaintId);
            return View(configuration);
        }

        // GET: UsersConfigurations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Configurations == null)
            {
                return NotFound();
            }

            var configuration = await _context.Configurations
                .Where(w => w.UserId == User.GetId())
                .FirstOrDefaultAsync(m => m.Id == id);

            if (configuration == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Display", configuration.CarId);
            ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "Display", configuration.EngineId);
            ViewData["PaintId"] = new SelectList(_context.Paints, "Id", "Display", configuration.PaintId);
            return View(configuration);
        }

        // POST: UsersConfigurations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CarId,EngineId,PaintId,Interior,Rims")] Configuration configuration)
        {
            if (id != configuration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    configuration.UserId = User.GetId();
                    _context.Update(configuration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigurationExists(configuration.Id))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Display", configuration.CarId);
            ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "Display", configuration.EngineId);
            ViewData["PaintId"] = new SelectList(_context.Paints, "Id", "Display", configuration.PaintId);
            return View(configuration);
        }

        // GET: UsersConfigurations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Configurations == null)
            {
                return NotFound();
            }

            var configuration = await _context.Configurations
                .Include(c => c.Car)
                .Include(c => c.Engine)
                .Include(c => c.Paint)
                .Where(w => w.UserId == User.GetId())
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuration == null)
            {
                return NotFound();
            }

            return View(configuration);
        }

        // POST: UsersConfigurations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Configurations == null)
            {
                return Problem("Entity set 'ConfigurationContext.Configurations'  is null.");
            }
            var configuration = await _context.Configurations
                .Where(w => w.UserId == User.GetId())
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuration != null)
            {
                _context.Configurations.Remove(configuration);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfigurationExists(int id)
        {
          return (_context.Configurations?.Any(e => e.Id == id && e.UserId == User.GetId())).GetValueOrDefault();
        }
    }
}
