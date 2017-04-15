using Assessment.Core;
using Assessment.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment.Web.Controllers {
    public class BookController : Controller {
        private BookRepository bookRepo;
        private AuthorRepository authorRepo;

        public BookController() {
            bookRepo = new BookRepository();
            authorRepo = new AuthorRepository();
        }

        // GET: Book
        [Authorize]
        public ActionResult Index() {
            var books = bookRepo.GetAll();

            var modelList = books.Select(b => new BookViewModel() {
                BookId = b.BookId,
                ISBN = b.ISBN,
                Title = b.Title,
                Year = b.Year,
                Authors = b.Authors.Select(x => new SelectListItem() {
                    Value = x.AuthorId.ToString(),
                    Text = x.FirstName + " " + x.LastName
                }).ToList()
            });
            return View(modelList);
        }

        // GET: Book/Details/5
        [Authorize]
        public ActionResult Details(int id) {
            var b = bookRepo.Get(id);

            return View(new BookViewModel() {
                BookId = b.BookId,
                ISBN = b.ISBN,
                Title = b.Title,
                Year = b.Year,
                Authors = b.Authors.Select(x => new SelectListItem() {
                    Value = x.AuthorId.ToString(),
                    Text = x.FirstName + " " + x.LastName
                }).ToList()
            });
        }

        // GET: Book/Create
        [Authorize]
        public ActionResult Create() {
            var authors = authorRepo.GetAll();
            return View(new BookViewModel() {
                Authors = authors.Select(x => new SelectListItem() {
                    Value = x.AuthorId.ToString(),
                    Text = x.FirstName + " " + x.LastName
                }).ToList()
            });
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(BookViewModel book) {
            try {
                bookRepo.Insert(new BookRepository() {
                    BookId = book.BookId,
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Year = book.Year,
                    Authors = book.AuthorsIds.Select(x => new AuthorRepository() {
                        AuthorId = x
                    }).ToList()
                });
                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        // GET: Book/Edit/5
        [Authorize]
        public ActionResult Edit(int id) {
            var b = bookRepo.Get(id);

            return View(new BookViewModel() {
                BookId = b.BookId,
                ISBN = b.ISBN,
                Title = b.Title,
                Year = b.Year,
                Authors = b.Authors.Select(x => new SelectListItem() {
                    Value = x.AuthorId.ToString(),
                    Text = x.FirstName + " " + x.LastName
                }).ToList()
            });
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(int id, BookViewModel book) {
            try {
                book.BookId = id;
                bookRepo.Update(new BookRepository() {
                    BookId = book.BookId,
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Year = book.Year
                });

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        // GET: Book/Delete/5
        [Authorize]
        public ActionResult Delete(int id) {
            var b = bookRepo.Get(id);

            return View(new BookViewModel() {
                BookId = b.BookId,
                ISBN = b.ISBN,
                Title = b.Title,
                Year = b.Year,
                Authors = b.Authors.Select(x => new SelectListItem() {
                    Value = x.AuthorId.ToString(),
                    Text = x.FirstName + " " + x.LastName
                }).ToList()
            });
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(int id, BookViewModel book) {
            try {
                bookRepo.Delete(id);

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }
    }
}
