using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RBApplicationCore80.Data;
using RBApplicationCore80.Models;

namespace RBApplicationCore80.Controllers
{
    [Authorize]
    public class OccupationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OccupationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Occupation
        [Authorize(Policy = "readpolicy")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Occupation.ToListAsync());
        }

        // GET: Occupation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = await _context.Occupation
                .FirstOrDefaultAsync(m => m.OccupationId == id);
            if (occupation == null)
            {
                return NotFound();
            }

            return View(occupation);
        }

        [Authorize(Policy = "writepolicy")]
        // GET: Occupation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Occupation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OccupationId,OccupationName")] Occupation occupation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(occupation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(occupation);
        }
        [Authorize(Policy = "writepolicy")]
        // GET: Occupation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = await _context.Occupation.FindAsync(id);
            if (occupation == null)
            {
                return NotFound();
            }
            return View(occupation);
        }

        // POST: Occupation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OccupationId,OccupationName")] Occupation occupation)
        {
            if (id != occupation.OccupationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(occupation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OccupationExists(occupation.OccupationId))
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
            return View(occupation);
        }

        // GET: Occupation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = await _context.Occupation
                .FirstOrDefaultAsync(m => m.OccupationId == id);
            if (occupation == null)
            {
                return NotFound();
            }

            return View(occupation);
        }

        // POST: Occupation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var occupation = await _context.Occupation.FindAsync(id);
            if (occupation != null)
            {
                _context.Occupation.Remove(occupation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OccupationExists(int id)
        {
            return _context.Occupation.Any(e => e.OccupationId == id);
        }
    }
}
