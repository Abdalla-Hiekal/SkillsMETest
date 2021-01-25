using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkillsTest.Data;
using SkillsTest.Models;
using SkillsTest.Models.ViewModels;

namespace SkillsTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["Max"] = _context.Cattle.Where(m => m.PastureId == null).Count();
            ViewData["PastureId"] = new SelectList(_context.Pasture, "Id", "Name");
            return View();
        }

        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(QuickAssignment quickassignment)
        {
            var AvailableCattles = _context.Cattle.Where(m => m.PastureId == null);
            var pasture = _context.Pasture.Any(m=> m.Id == quickassignment.PastureId);
            if (!pasture) return BadRequest();
            ViewData["PastureId"] = new SelectList(_context.Pasture, "Id", "Name", quickassignment.PastureId);
            int count = AvailableCattles.Count();
            if (quickassignment.Number > count)
            {
                ViewData["Max"] = count;
                ModelState.AddModelError("Number", "There are not enough unassigned cattles");  
                return View(quickassignment);
            }
            var cattles = AvailableCattles.Take(quickassignment.Number);
            foreach(var cat in cattles)
            {
                cat.PastureId = quickassignment.PastureId;
                _context.Update(cat);
            }
            await _context.SaveChangesAsync();
            ViewData["Max"] = count- quickassignment.Number;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
