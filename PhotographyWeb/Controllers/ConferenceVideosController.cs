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
    public class ConferenceVideosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConferenceVideosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConferenceVideos
        public async Task<IActionResult> Index()
        {
            return View(await _context.ConferenceVideo.ToListAsync());
        }

        // GET: ConferenceVideos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conferenceVideo = await _context.ConferenceVideo
                .FirstOrDefaultAsync(m => m.ConferenceVideoId == id);
            if (conferenceVideo == null)
            {
                return NotFound();
            }

            return View(conferenceVideo);
        }

        // GET: ConferenceVideos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConferenceVideos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConferenceVideoId")] ConferenceVideo conferenceVideo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conferenceVideo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conferenceVideo);
        }

        // GET: ConferenceVideos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conferenceVideo = await _context.ConferenceVideo.FindAsync(id);
            if (conferenceVideo == null)
            {
                return NotFound();
            }
            return View(conferenceVideo);
        }

        // POST: ConferenceVideos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConferenceVideoId")] ConferenceVideo conferenceVideo)
        {
            if (id != conferenceVideo.ConferenceVideoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conferenceVideo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConferenceVideoExists(conferenceVideo.ConferenceVideoId))
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
            return View(conferenceVideo);
        }

        // GET: ConferenceVideos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conferenceVideo = await _context.ConferenceVideo
                .FirstOrDefaultAsync(m => m.ConferenceVideoId == id);
            if (conferenceVideo == null)
            {
                return NotFound();
            }

            return View(conferenceVideo);
        }

        // POST: ConferenceVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conferenceVideo = await _context.ConferenceVideo.FindAsync(id);
            _context.ConferenceVideo.Remove(conferenceVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConferenceVideoExists(int id)
        {
            return _context.ConferenceVideo.Any(e => e.ConferenceVideoId == id);
        }
    }
}
