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
    public class CameramanEmailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CameramanEmailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CameramanEmails
        public async Task<IActionResult> Index()
        {
            return View(await _context.CameramanEmail.ToListAsync());
        }

        // GET: CameramanEmails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cameramanEmail = await _context.CameramanEmail
                .FirstOrDefaultAsync(m => m.CameramanId == id);
            if (cameramanEmail == null)
            {
                return NotFound();
            }

            return View(cameramanEmail);
        }

        // GET: CameramanEmails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CameramanEmails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CameramanId,Email")] CameramanEmail cameramanEmail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cameramanEmail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cameramanEmail);
        }

        // GET: CameramanEmails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cameramanEmail = await _context.CameramanEmail.FindAsync(id);
            if (cameramanEmail == null)
            {
                return NotFound();
            }
            return View(cameramanEmail);
        }

        // POST: CameramanEmails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CameramanId,Email")] CameramanEmail cameramanEmail)
        {
            if (id != cameramanEmail.CameramanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cameramanEmail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CameramanEmailExists(cameramanEmail.CameramanId))
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
            return View(cameramanEmail);
        }

        // GET: CameramanEmails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cameramanEmail = await _context.CameramanEmail
                .FirstOrDefaultAsync(m => m.CameramanId == id);
            if (cameramanEmail == null)
            {
                return NotFound();
            }

            return View(cameramanEmail);
        }

        // POST: CameramanEmails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cameramanEmail = await _context.CameramanEmail.FindAsync(id);
            _context.CameramanEmail.Remove(cameramanEmail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CameramanEmailExists(int id)
        {
            return _context.CameramanEmail.Any(e => e.CameramanId == id);
        }
    }
}
