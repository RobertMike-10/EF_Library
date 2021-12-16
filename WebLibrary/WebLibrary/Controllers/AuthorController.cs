using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebLibrary.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AuthorController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Author> authors = _db.Authors.ToList();
            return View(authors);
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Author author = new Author();
            if (id == null) return View(author);
           
            //this for edit
            author = _db.Authors.FirstOrDefault(a => a.AuthorId == id);
            if (author == null) return NotFound();
            
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Author author)
        {
            if (ModelState.IsValid)
            {
                if (author.AuthorId == 0)
                {
                    //this is create
                    _db.Authors.Add(author);
                }
                else
                {
                    //this is an update
                    _db.Authors.Update(author);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(author);

        }

        public IActionResult Delete(int id)
        {
            var author = _db.Authors.FirstOrDefault(a => a.AuthorId == id);
            _db.Authors.Remove(author);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }

}
