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
    public class ConferenceVideoSpeakersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConferenceVideoSpeakersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConferenceVideoSpeakers
        public async Task<IActionResult> Index()
        {
            return View(await _context.ConferenceVideoSpeaker.ToListAsync());
        }

        // GET: ConferenceVideoSpeakers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conferenceVideoSpeaker = await _context.ConferenceVideoSpeaker
                .FirstOrDefaultAsync(m => m.ConferenceVideoId == id);
            if (conferenceVideoSpeaker == null)
            {
                return NotFound();
            }

            return View(conferenceVideoSpeaker);
        }

        // GET: ConferenceVideoSpeakers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConferenceVideoSpeakers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConferenceVideoId,Speaker")] ConferenceVideoSpeaker conferenceVideoSpeaker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conferenceVideoSpeaker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conferenceVideoSpeaker);
        }

        // GET: ConferenceVideoSpeakers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conferenceVideoSpeaker = await _context.ConferenceVideoSpeaker.FindAsync(id);
            if (conferenceVideoSpeaker == null)
            {
                return NotFound();
            }
            return View(conferenceVideoSpeaker);
        }

        // POST: ConferenceVideoSpeakers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConferenceVideoId,Speaker")] ConferenceVideoSpeaker conferenceVideoSpeaker)
        {
            if (id != conferenceVideoSpeaker.ConferenceVideoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conferenceVideoSpeaker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConferenceVideoSpeakerExists(conferenceVideoSpeaker.ConferenceVideoId))
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
            return View(conferenceVideoSpeaker);
        }

        // GET: ConferenceVideoSpeakers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conferenceVideoSpeaker = await _context.ConferenceVideoSpeaker
                .FirstOrDefaultAsync(m => m.ConferenceVideoId == id);
            if (conferenceVideoSpeaker == null)
            {
                return NotFound();
            }

            return View(conferenceVideoSpeaker);
        }

        // POST: ConferenceVideoSpeakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conferenceVideoSpeaker = await _context.ConferenceVideoSpeaker.FindAsync(id);
            _context.ConferenceVideoSpeaker.Remove(conferenceVideoSpeaker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConferenceVideoSpeakerExists(int id)
        {
            return _context.ConferenceVideoSpeaker.Any(e => e.ConferenceVideoId == id);
        }
    }
}
