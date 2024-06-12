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
    public class WeddingPhotoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeddingPhotoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WeddingPhotoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.WeddingPhoto.ToListAsync());
        }

        // GET: WeddingPhotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weddingPhoto = await _context.WeddingPhoto
                .FirstOrDefaultAsync(m => m.WeddingPhotoId == id);
            if (weddingPhoto == null)
            {
                return NotFound();
            }

            return View(weddingPhoto);
        }

        // GET: WeddingPhotoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeddingPhotoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeddingPhotoId,Groom,Bride")] WeddingPhoto weddingPhoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weddingPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weddingPhoto);
        }

        // GET: WeddingPhotoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weddingPhoto = await _context.WeddingPhoto.FindAsync(id);
            if (weddingPhoto == null)
            {
                return NotFound();
            }
            return View(weddingPhoto);
        }

        // POST: WeddingPhotoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WeddingPhotoId,Groom,Bride")] WeddingPhoto weddingPhoto)
        {
            if (id != weddingPhoto.WeddingPhotoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weddingPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeddingPhotoExists(weddingPhoto.WeddingPhotoId))
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
            return View(weddingPhoto);
        }

        // GET: WeddingPhotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weddingPhoto = await _context.WeddingPhoto
                .FirstOrDefaultAsync(m => m.WeddingPhotoId == id);
            if (weddingPhoto == null)
            {
                return NotFound();
            }

            return View(weddingPhoto);
        }

        // POST: WeddingPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weddingPhoto = await _context.WeddingPhoto.FindAsync(id);
            _context.WeddingPhoto.Remove(weddingPhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeddingPhotoExists(int id)
        {
            return _context.WeddingPhoto.Any(e => e.WeddingPhotoId == id);
        }
    }
}
