using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebLibrary.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Category> categories = _db.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            //
            if (id == null) return View(category);           
            category = _db.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.CategoryId == 0)
                {
                    //this is create
                    _db.Categories.Add(obj);
                }
                else
                {
                    //this is an update
                    _db.Categories.Update(obj);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);

        }
    }
}
