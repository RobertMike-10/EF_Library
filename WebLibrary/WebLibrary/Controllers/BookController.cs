using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            List<Book> books = _db.Books.Include(b => b.Publisher).
                                         Include(b => b.Category).
                                         Include(b => b.BookAuthors).ThenInclude(a => a.Author).
                                         Include(b=> b.BookGenres).ThenInclude(g => g.Genre).ToList();
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
            book.CategoriesList = _db.Categories.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.CategoryId.ToString()
            });
            if (id == null) return View(book);

            //this for edit
            book.Book = _db.Books.FirstOrDefault(b => b.BookId == id);
            if (book.Book == null) return NotFound();

            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookVM bookVM)
        {
            if (bookVM.Book.BookId == 0)
            {
                //this is create
                _db.Books.Add(bookVM.Book);
            }
            else
            {
                //this is an update
                _db.Books.Update(bookVM.Book);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var book = _db.Books.FirstOrDefault(b => b.BookId == id);
            _db.Books.Remove(book);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            BookVM bookVM = new BookVM();

            if (id == null) return View(bookVM);
         
            //this for edit
            bookVM.Book = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.BookId == id);

            if (bookVM == null) return NotFound();
           
            return View(bookVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookVM bookVM)
        {
            if (bookVM.Book.BookDetail.BookDetailId == 0)
            {
                //this is create
                _db.BookDetails.Add(bookVM.Book.BookDetail);
                _db.SaveChanges();

                //adding key
                Book book = _db.Books.FirstOrDefault(b => b.BookId == bookVM.Book.BookId);
                book.BookDetailId = bookVM.Book.BookDetail.BookDetailId;
                _db.SaveChanges();
            }
            else
            {
                //this is an update
                _db.BookDetails.Update(bookVM.Book.BookDetail);
                _db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult PlayGround()
        {
            IEnumerable<Book> BookList1 = _db.Books;
            var FilteredBook1 = BookList1.Where(b => b.Price > 500).ToList();

            IQueryable<Book> BookList2 = _db.Books;
            var fileredBook2 = BookList2.Where(b => b.Price > 500).ToList();

            return RedirectToAction(nameof(Index));
        }
}
