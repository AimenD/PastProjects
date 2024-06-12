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
    public class WeddingVideosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeddingVideosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WeddingVideos
        public async Task<IActionResult> Index()
        {
            return View(await _context.WeddingVideo.ToListAsync());
        }

        // GET: WeddingVideos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weddingVideo = await _context.WeddingVideo
                .FirstOrDefaultAsync(m => m.WeddingVideoId == id);
            if (weddingVideo == null)
            {
                return NotFound();
            }

            return View(weddingVideo);
        }

        // GET: WeddingVideos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeddingVideos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeddingVideoId,Groom,Bride")] WeddingVideo weddingVideo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weddingVideo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weddingVideo);
        }

        // GET: WeddingVideos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weddingVideo = await _context.WeddingVideo.FindAsync(id);
            if (weddingVideo == null)
            {
                return NotFound();
            }
            return View(weddingVideo);
        }

        // POST: WeddingVideos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WeddingVideoId,Groom,Bride")] WeddingVideo weddingVideo)
        {
            if (id != weddingVideo.WeddingVideoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weddingVideo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeddingVideoExists(weddingVideo.WeddingVideoId))
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
            return View(weddingVideo);
        }

        // GET: WeddingVideos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weddingVideo = await _context.WeddingVideo
                .FirstOrDefaultAsync(m => m.WeddingVideoId == id);
            if (weddingVideo == null)
            {
                return NotFound();
            }

            return View(weddingVideo);
        }

        // POST: WeddingVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weddingVideo = await _context.WeddingVideo.FindAsync(id);
            _context.WeddingVideo.Remove(weddingVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeddingVideoExists(int id)
        {
            return _context.WeddingVideo.Any(e => e.WeddingVideoId == id);
        }
    }
}
