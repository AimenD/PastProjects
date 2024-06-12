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
    public class CameramenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CameramenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cameramen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cameraman.ToListAsync());
        }

        // GET: Cameramen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cameraman = await _context.Cameraman
                .FirstOrDefaultAsync(m => m.CameramanId == id);
            if (cameraman == null)
            {
                return NotFound();
            }

            return View(cameraman);
        }

        // GET: Cameramen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cameramen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CameramanId,FirstName,LastName,DateOfBirth,PhoneNumber,Address,Age")] Cameraman cameraman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cameraman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cameraman);
        }

        // GET: Cameramen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cameraman = await _context.Cameraman.FindAsync(id);
            if (cameraman == null)
            {
                return NotFound();
            }
            return View(cameraman);
        }

        // POST: Cameramen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CameramanId,FirstName,LastName,DateOfBirth,PhoneNumber,Address,Age")] Cameraman cameraman)
        {
            if (id != cameraman.CameramanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cameraman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CameramanExists(cameraman.CameramanId))
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
            return View(cameraman);
        }

        // GET: Cameramen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cameraman = await _context.Cameraman
                .FirstOrDefaultAsync(m => m.CameramanId == id);
            if (cameraman == null)
            {
                return NotFound();
            }

            return View(cameraman);
        }

        // POST: Cameramen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cameraman = await _context.Cameraman.FindAsync(id);
            _context.Cameraman.Remove(cameraman);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CameramanExists(int id)
        {
            return _context.Cameraman.Any(e => e.CameramanId == id);
        }
    }
}
