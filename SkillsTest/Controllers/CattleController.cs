using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkillsTest.Data;
using SkillsTest.Models;
using SkillsTest.Models.ViewModels;

namespace SkillsTest.Controllers
{
    public class CattleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CattleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cattle
        //[Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cattle.Include(c => c.Pasture).OrderBy(m=> m.Id);
            ViewData["PastureId"] = new SelectList(_context.Pasture, "Id", "Name");
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cattle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cattle = await _context.Cattle
                .Include(c => c.Pasture)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cattle == null)
            {
                return NotFound();
            }

            return View(cattle);
        }

        // GET: Cattle/Create
        //[Authorize]
        public IActionResult Create()
        {
            ViewData["PastureId"] = new SelectList(_context.Pasture, "Id", "Name");
            ViewData["Type"] = new SelectList( new List<TypeViewModel>(){new TypeViewModel{ Type="Cow" }, new TypeViewModel { Type = "Bull" } }, "Type", "Type");
            return View();
        }

        // POST: Cattle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> Create([Bind("Id,Age,Price,Weight,Type,HealthStatus,Color,PastureId")] Cattle cattle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cattle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PastureId"] = new SelectList(_context.Pasture, "Id", "Name", cattle.PastureId);
            ViewData["Type"] = new SelectList(new List<TypeViewModel>() { new TypeViewModel { Type = "Cow" }, new TypeViewModel { Type = "Bull" } }, "Type", "Type");
            return View(cattle);
        }

        // GET: Cattle/Edit/5
        //[Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cattle = await _context.Cattle.FindAsync(id);
            if (cattle == null)
            {
                return NotFound();
            }
            ViewData["PastureId"] = new SelectList(_context.Pasture, "Id", "Name", cattle.PastureId);
            ViewData["Type"] = new SelectList(new List<TypeViewModel>() { new TypeViewModel { Type = "Cow" }, new TypeViewModel { Type = "Bull" } }, "Type", "Type");
            return View(cattle);
        }

        // POST: Cattle/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Age,Price,Weight,Type,HealthStatus,Color,PastureId")] Cattle cattle)
        {
            if (id != cattle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cattle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CattleExists(cattle.Id))
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
            ViewData["PastureId"] = new SelectList(_context.Pasture, "Id", "Name", cattle.PastureId);
            ViewData["Type"] = new SelectList(new List<TypeViewModel>() { new TypeViewModel { Type = "Cow" }, new TypeViewModel { Type = "Bull" } }, "Type", "Type");
            return View(cattle);
        }

        // GET: Cattle/Sell/5 (for Delete)
        //[Authorize]
        public async Task<IActionResult> Sell(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cattle = await _context.Cattle
                .Include(c => c.Pasture)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cattle == null)
            {
                return NotFound();
            }

            return View(cattle);
        }

        // POST: Cattle/Delete/5
        [HttpPost, ActionName("Sell")]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cattle = await _context.Cattle.FindAsync(id);
            _context.Cattle.Remove(cattle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> ChangePasture(int Id, int? PastureId)
        {
            var cattle = await _context.Cattle.FindAsync(Id);
            var pasture = _context.Pasture.Any(m=> m.Id == PastureId);
            if (cattle == null || (PastureId != null && !pasture))
            {
                return BadRequest();
            }
            cattle.PastureId = PastureId;
            _context.Update(cattle);
            await _context.SaveChangesAsync();
            return Json(new{ status="ok" });
        }

        private bool CattleExists(int id)
        {
            return _context.Cattle.Any(e => e.Id == id);
        }
    }
}
