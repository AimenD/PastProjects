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
    public class StockPhotoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockPhotoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StockPhotoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.StockPhoto.ToListAsync());
        }

        // GET: StockPhotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockPhoto = await _context.StockPhoto
                .FirstOrDefaultAsync(m => m.StockPhotoId == id);
            if (stockPhoto == null)
            {
                return NotFound();
            }

            return View(stockPhoto);
        }

        // GET: StockPhotoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockPhotoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockPhotoId,Price")] StockPhoto stockPhoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockPhoto);
        }

        // GET: StockPhotoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockPhoto = await _context.StockPhoto.FindAsync(id);
            if (stockPhoto == null)
            {
                return NotFound();
            }
            return View(stockPhoto);
        }

        // POST: StockPhotoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StockPhotoId,Price")] StockPhoto stockPhoto)
        {
            if (id != stockPhoto.StockPhotoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockPhotoExists(stockPhoto.StockPhotoId))
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
            return View(stockPhoto);
        }

        // GET: StockPhotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockPhoto = await _context.StockPhoto
                .FirstOrDefaultAsync(m => m.StockPhotoId == id);
            if (stockPhoto == null)
            {
                return NotFound();
            }

            return View(stockPhoto);
        }

        // POST: StockPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockPhoto = await _context.StockPhoto.FindAsync(id);
            _context.StockPhoto.Remove(stockPhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockPhotoExists(int id)
        {
            return _context.StockPhoto.Any(e => e.StockPhotoId == id);
        }
    }
}
