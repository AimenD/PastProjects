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
    public class VideoCamerasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VideoCamerasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VideoCameras
        public async Task<IActionResult> Index()
        {
            return View(await _context.VideoCamera.ToListAsync());
        }

        // GET: VideoCameras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoCamera = await _context.VideoCamera
                .FirstOrDefaultAsync(m => m.VideoCameraId == id);
            if (videoCamera == null)
            {
                return NotFound();
            }

            return View(videoCamera);
        }

        // GET: VideoCameras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VideoCameras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VideoCameraId,MovieSize")] VideoCamera videoCamera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videoCamera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(videoCamera);
        }

        // GET: VideoCameras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoCamera = await _context.VideoCamera.FindAsync(id);
            if (videoCamera == null)
            {
                return NotFound();
            }
            return View(videoCamera);
        }

        // POST: VideoCameras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VideoCameraId,MovieSize")] VideoCamera videoCamera)
        {
            if (id != videoCamera.VideoCameraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videoCamera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoCameraExists(videoCamera.VideoCameraId))
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
            return View(videoCamera);
        }

        // GET: VideoCameras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoCamera = await _context.VideoCamera
                .FirstOrDefaultAsync(m => m.VideoCameraId == id);
            if (videoCamera == null)
            {
                return NotFound();
            }

            return View(videoCamera);
        }

        // POST: VideoCameras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoCamera = await _context.VideoCamera.FindAsync(id);
            _context.VideoCamera.Remove(videoCamera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoCameraExists(int id)
        {
            return _context.VideoCamera.Any(e => e.VideoCameraId == id);
        }
    }
}
