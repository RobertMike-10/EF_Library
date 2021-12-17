using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;
using Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace WebLibrary.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Book> books = _db.Books.ToList();
            return View(books);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            BookVM book = new BookVM();

            book.PublisherList = _db.Publishers.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.PublisherId.ToString()
            });
            if (id == null) return View(book);

            //this for edit
            book.Book = _db.Books.FirstOrDefault(b => b.BookId == id);
            if (book.Book == null) return NotFound();

            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookVM obj)
        {
            if (obj.Book.BookId == 0)
            {
                //this is create
                _db.Books.Add(obj.Book);
            }
            else
            {
                //this is an update
                _db.Books.Update(obj.Book);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
