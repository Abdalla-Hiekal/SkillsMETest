using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkillsTest.Data;
using SkillsTest.Models;

namespace SkillsTest.Controllers
{
    public class PasturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PasturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pastures
        //[Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pasture.OrderBy(m => m.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pastures/Details/5
        //[Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasture = await _context.Pasture
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pasture == null)
            {
                return NotFound();
            }

            return View(pasture);
        }


        // GET: Pastures/Edit/5
        //[Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasture = await _context.Pasture.FindAsync(id);
            if (pasture == null)
            {
                return NotFound();
            }
            return View(pasture);
        }

        // POST: Pastures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Temperature,GrassCondition")] Pasture pasture)
        {
            if (id != pasture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pasture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PastureExists(pasture.Id))
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
            return View(pasture);
        }

        private bool PastureExists(int id)
        {
            return _context.Pasture.Any(e => e.Id == id);
        }
    }
}
