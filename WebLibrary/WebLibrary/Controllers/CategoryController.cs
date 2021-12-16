using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System;
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
            //Create
            if (id == null) return View(category);           
            category = _db.Categories.FirstOrDefault(c => c.CategoryId == id);
            //Category Not Found
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

        public IActionResult Delete(int id)
        {
            var category= _db.Categories.FirstOrDefault(c => c.CategoryId == id);
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple(int id)
        {
            List<Category> catList = new List<Category>();
            for (int i = 1; i <= id; i++)
            {
                catList.Add(new Category { Name = Guid.NewGuid().ToString() });
                //_db.Categories.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _db.Categories.AddRange(catList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
