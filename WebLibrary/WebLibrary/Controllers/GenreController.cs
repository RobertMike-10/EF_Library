using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebLibrary.Controllers
{
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GenreController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Genre> genres= _db.Genres.ToList();
            return View(genres);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Genre genre = new Genre();
            if (id == null) return View(genre);

            //this for edit
            genre = _db.Genres.FirstOrDefault(g => g.GenreId == id);
            if (genre == null) return NotFound();

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Genre genre)
        {
            if (ModelState.IsValid)
            {
                if (genre.GenreId == 0)
                {
                    //this is create
                    _db.Genres.Add(genre);
                }
                else
                {
                    //this is an update
                    _db.Genres.Update(genre);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);

        }

        public IActionResult Delete(int id)
        {
            var genre = _db.Genres.FirstOrDefault(g => g.GenreId == id);
            _db.Genres.Remove(genre);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
