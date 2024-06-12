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
    public class PhotoCamerasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhotoCamerasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PhotoCameras
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhotoCamera.ToListAsync());
        }

        // GET: PhotoCameras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoCamera = await _context.PhotoCamera
                .FirstOrDefaultAsync(m => m.PhotoCameraId == id);
            if (photoCamera == null)
            {
                return NotFound();
            }

            return View(photoCamera);
        }

        // GET: PhotoCameras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhotoCameras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhotoCameraId,MaxResolution")] PhotoCamera photoCamera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(photoCamera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(photoCamera);
        }

        // GET: PhotoCameras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoCamera = await _context.PhotoCamera.FindAsync(id);
            if (photoCamera == null)
            {
                return NotFound();
            }
            return View(photoCamera);
        }

        // POST: PhotoCameras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhotoCameraId,MaxResolution")] PhotoCamera photoCamera)
        {
            if (id != photoCamera.PhotoCameraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photoCamera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoCameraExists(photoCamera.PhotoCameraId))
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
            return View(photoCamera);
        }

        // GET: PhotoCameras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoCamera = await _context.PhotoCamera
                .FirstOrDefaultAsync(m => m.PhotoCameraId == id);
            if (photoCamera == null)
            {
                return NotFound();
            }

            return View(photoCamera);
        }

        // POST: PhotoCameras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photoCamera = await _context.PhotoCamera.FindAsync(id);
            _context.PhotoCamera.Remove(photoCamera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhotoCameraExists(int id)
        {
            return _context.PhotoCamera.Any(e => e.PhotoCameraId == id);
        }
    }
}
