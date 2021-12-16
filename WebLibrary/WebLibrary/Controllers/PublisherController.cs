using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebLibrary.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PublisherController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Publisher> objList = _db.Publishers.ToList();
            return View(objList);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Publisher publisher = new Publisher();
            if (id == null) return View(publisher);
            
            //this for edit
            publisher = _db.Publishers.FirstOrDefault(p => p.PublisherId == id);
            if (publisher == null) return NotFound();          
            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                if (publisher.PublisherId == 0)
                {
                    //this is create
                    _db.Publishers.Add(publisher);
                }
                else
                {
                    //this is an update
                    _db.Publishers.Update(publisher);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);

        }

        public IActionResult Delete(int id)
        {
            var publisher = _db.Publishers.FirstOrDefault(p => p.PublisherId == id);
            _db.Publishers.Remove(publisher);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
