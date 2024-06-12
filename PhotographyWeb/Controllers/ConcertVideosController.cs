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
    public class ConcertVideosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConcertVideosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConcertVideos
        public async Task<IActionResult> Index()
        {
            return View(await _context.ConcertVideo.ToListAsync());
        }

        // GET: ConcertVideos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertVideo = await _context.ConcertVideo
                .FirstOrDefaultAsync(m => m.ConcertVideoId == id);
            if (concertVideo == null)
            {
                return NotFound();
            }

            return View(concertVideo);
        }

        // GET: ConcertVideos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConcertVideos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConcertVideoId,Artist,ConcertArea")] ConcertVideo concertVideo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(concertVideo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(concertVideo);
        }

        // GET: ConcertVideos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertVideo = await _context.ConcertVideo.FindAsync(id);
            if (concertVideo == null)
            {
                return NotFound();
            }
            return View(concertVideo);
        }

        // POST: ConcertVideos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConcertVideoId,Artist,ConcertArea")] ConcertVideo concertVideo)
        {
            if (id != concertVideo.ConcertVideoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concertVideo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertVideoExists(concertVideo.ConcertVideoId))
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
            return View(concertVideo);
        }

        // GET: ConcertVideos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concertVideo = await _context.ConcertVideo
                .FirstOrDefaultAsync(m => m.ConcertVideoId == id);
            if (concertVideo == null)
            {
                return NotFound();
            }

            return View(concertVideo);
        }

        // POST: ConcertVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concertVideo = await _context.ConcertVideo.FindAsync(id);
            _context.ConcertVideo.Remove(concertVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcertVideoExists(int id)
        {
            return _context.ConcertVideo.Any(e => e.ConcertVideoId == id);
        }
    }
}
