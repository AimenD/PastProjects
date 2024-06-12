#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotographyWeb.Data;
using PhotographyWeb.Models;

namespace PhotographyWeb.Controllers
{
    public class PhotographersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhotographersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Photographers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Photographer.ToListAsync());
        }

        // GET: Photographers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photographer = await _context.Photographer
                .FirstOrDefaultAsync(m => m.PhotographerId == id);
            if (photographer == null)
            {
                return NotFound();
            }

            return View(photographer);
        }

        // GET: Photographers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Photographers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhotographerId,PhotoCameraOwned")] Photographer photographer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(photographer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(photographer);
        }

        // GET: Photographers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photographer = await _context.Photographer.FindAsync(id);
            if (photographer == null)
            {
                return NotFound();
            }
            return View(photographer);
        }

        // POST: Photographers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhotographerId,PhotoCameraOwned")] Photographer photographer)
        {
            if (id != photographer.PhotographerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photographer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotographerExists(photographer.PhotographerId))
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
            return View(photographer);
        }

        // GET: Photographers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photographer = await _context.Photographer
                .FirstOrDefaultAsync(m => m.PhotographerId == id);
            if (photographer == null)
            {
                return NotFound();
            }

            return View(photographer);
        }

        // POST: Photographers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photographer = await _context.Photographer.FindAsync(id);
            _context.Photographer.Remove(photographer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhotographerExists(int id)
        {
            return _context.Photographer.Any(e => e.PhotographerId == id);
        }
    }
}
