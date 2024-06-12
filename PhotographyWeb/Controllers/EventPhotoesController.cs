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
    public class EventPhotoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventPhotoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventPhotoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.EventPhoto.ToListAsync());
        }

        // GET: EventPhotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventPhoto = await _context.EventPhoto
                .FirstOrDefaultAsync(m => m.EventPhotoId == id);
            if (eventPhoto == null)
            {
                return NotFound();
            }

            return View(eventPhoto);
        }

        // GET: EventPhotoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventPhotoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventPhotoId,EventName")] EventPhoto eventPhoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventPhoto);
        }

        // GET: EventPhotoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventPhoto = await _context.EventPhoto.FindAsync(id);
            if (eventPhoto == null)
            {
                return NotFound();
            }
            return View(eventPhoto);
        }

        // POST: EventPhotoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventPhotoId,EventName")] EventPhoto eventPhoto)
        {
            if (id != eventPhoto.EventPhotoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventPhotoExists(eventPhoto.EventPhotoId))
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
            return View(eventPhoto);
        }

        // GET: EventPhotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventPhoto = await _context.EventPhoto
                .FirstOrDefaultAsync(m => m.EventPhotoId == id);
            if (eventPhoto == null)
            {
                return NotFound();
            }

            return View(eventPhoto);
        }

        // POST: EventPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventPhoto = await _context.EventPhoto.FindAsync(id);
            _context.EventPhoto.Remove(eventPhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventPhotoExists(int id)
        {
            return _context.EventPhoto.Any(e => e.EventPhotoId == id);
        }
    }
}
