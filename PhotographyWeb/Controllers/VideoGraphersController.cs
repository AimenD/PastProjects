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
    public class VideoGraphersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VideoGraphersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VideoGraphers
        public async Task<IActionResult> Index()
        {
            return View(await _context.VideoGrapher.ToListAsync());
        }

        // GET: VideoGraphers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoGrapher = await _context.VideoGrapher
                .FirstOrDefaultAsync(m => m.VideographerId == id);
            if (videoGrapher == null)
            {
                return NotFound();
            }

            return View(videoGrapher);
        }

        // GET: VideoGraphers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VideoGraphers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VideographerId,VideoCameraOwned")] VideoGrapher videoGrapher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videoGrapher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(videoGrapher);
        }

        // GET: VideoGraphers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoGrapher = await _context.VideoGrapher.FindAsync(id);
            if (videoGrapher == null)
            {
                return NotFound();
            }
            return View(videoGrapher);
        }

        // POST: VideoGraphers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VideographerId,VideoCameraOwned")] VideoGrapher videoGrapher)
        {
            if (id != videoGrapher.VideographerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videoGrapher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoGrapherExists(videoGrapher.VideographerId))
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
            return View(videoGrapher);
        }

        // GET: VideoGraphers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoGrapher = await _context.VideoGrapher
                .FirstOrDefaultAsync(m => m.VideographerId == id);
            if (videoGrapher == null)
            {
                return NotFound();
            }

            return View(videoGrapher);
        }

        // POST: VideoGraphers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoGrapher = await _context.VideoGrapher.FindAsync(id);
            _context.VideoGrapher.Remove(videoGrapher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoGrapherExists(int id)
        {
            return _context.VideoGrapher.Any(e => e.VideographerId == id);
        }
    }
}
