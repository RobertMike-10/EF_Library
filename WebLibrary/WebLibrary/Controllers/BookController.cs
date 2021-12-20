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
                                         Include(b => b.BookGenres).ThenInclude(g => g.Genre).ToList();
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

        [HttpGet]
        public IActionResult ManageGenres(int id)
        {
            BookGenreVM model = new BookGenreVM
            {
                BookGenreList = _db.BookGenres.Include(b => b.Genre).Include(b => b.Book)
                                    .Where(b => b.BookId == id).ToList(),
                BookGenre = new BookGenre()
                {
                    BookId = id
                },
                Book = _db.Books.FirstOrDefault(b => b.BookId == id)
            };
            List<int> tempListOfGenres = model.BookGenreList.Select(g => g.GenreId).ToList();
            //NOT IN Clause in LINQ
            //get all the authors whos id is not in tempListOfAssignedAuthors
            var tempList = _db.Genres.Where(g => !tempListOfGenres.Contains(g.GenreId)).ToList();

            model.GenreList = tempList.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.GenreId.ToString()
            });

            return View(model);
        }

        [HttpPost]
        public IActionResult ManageGenres(BookGenreVM bookGenreVM)
        {
            if (bookGenreVM.BookGenre.BookId != 0 && bookGenreVM.BookGenre.GenreId != 0)
            {
                _db.BookGenres.Add(bookGenreVM.BookGenre);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(ManageGenres), new { @id = bookGenreVM.BookGenre.BookId });
        }

        [HttpPost]
        public IActionResult RemoveGenres(int genreId, BookGenreVM bookGenreVM)
        {
            int bookId = bookGenreVM.Book.BookId;
            BookGenre bookGenre = _db.BookGenres.FirstOrDefault(
                b => b.GenreId == genreId && b.BookId == bookId);

            _db.BookGenres.Remove(bookGenre);
            _db.SaveChanges();
            return RedirectToAction(nameof(ManageGenres), new { @id = bookId });
        }

        [HttpGet]
        public IActionResult ManageAuthors(int id)
        {
            BookAuthorVM model = new BookAuthorVM
            {
                BookAuthorList = _db.BookAuthors.Include(b => b.Author).Include(b => b.Book)
                                    .Where(b => b.BookId == id).ToList(),
                BookAuthor = new BookAuthor()
                {
                    BookId = id
                },
                Book = _db.Books.FirstOrDefault(b => b.BookId == id)
            };
            List<int> tempListOfAuthors = model.BookAuthorList.Select(a => a.AuthorId).ToList();
            //NOT IN Clause in LINQ
            //get all the authors whos id is not in tempListOfAssignedAuthors
            var tempList = _db.Authors.Where(a => !tempListOfAuthors.Contains(a.AuthorId)).ToList();

            model.AuthorList = tempList.Select(i => new SelectListItem
            {
                Text = i.FullName,
                Value = i.AuthorId.ToString()
            });

            return View(model);
        }

        [HttpPost]
        public IActionResult ManageAuthors(BookAuthorVM bookAuthorVM)
        {
            if (bookAuthorVM.BookAuthor.BookId != 0 && bookAuthorVM.BookAuthor.AuthorId != 0)
            {
                _db.BookAuthors.Add(bookAuthorVM.BookAuthor);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.BookAuthor.BookId });
        }

        [HttpPost]
        public IActionResult RemoveAuthors(int authorId, BookAuthorVM bookAuthorVM)
        {
            int bookId = bookAuthorVM.Book.BookId;
            BookAuthor bookAuthor = _db.BookAuthors.FirstOrDefault(
                b => b.AuthorId == authorId && b.BookId == bookId);

            _db.BookAuthors.Remove(bookAuthor);
            _db.SaveChanges();
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookId });
        }

        public IActionResult PlayGround()
        {
            IEnumerable<Book> BookList1 = _db.Books;
            var FilteredBook1 = BookList1.Where(b => b.Price > 500).ToList();

            IQueryable<Book> BookList2 = _db.Books;
            var fileredBook2 = BookList2.Where(b => b.Price > 500).ToList();
            /*
            var category = _db.Categories.FirstOrDefault();
            _db.Entry(category).State = EntityState.Modified;

            _db.SaveChanges();

            var bookTemp1 = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.BookId == 4);
            bookTemp1.BookDetail.NumberOfChapters = 300;
            bookTemp1.Price = 5678;
            _db.Books.Update(bookTemp1);
            _db.SaveChanges();

            var bookTemp2 = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.BookId == 4);
            bookTemp2.BookDetail.Weight = 34.8M;
            bookTemp2.Price = 5678;
            _db.Books.Attach(bookTemp2);
            _db.SaveChanges();*/

            //VIEWS
            var viewList = _db.BookDetailsView.ToList();
            var viewList1 = _db.BookDetailsView.FirstOrDefault();
            var viewList2 = _db.BookDetailsView.Where(u => u.Price > 500);

            //RAW SQL

            var bookRaw = _db.Books.FromSqlRaw("Select * from dbo.books").ToList();
           

            return RedirectToAction(nameof(Index));
        }
    }
}
