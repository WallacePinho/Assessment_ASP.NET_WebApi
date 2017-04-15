using Assessment.Api.Models;
using Assessment.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assessment.Api.Controllers {
    public class BookController : ApiController {
        private BookRepository bookRepo;

        public BookController() {
            bookRepo = new BookRepository();
        }

        // GET: api/Book
        public IEnumerable<Book> Get() {
            var books = bookRepo.GetAll();
            
            return books.Select(b => new Book() {
                BookId = b.BookId,
                ISBN = b.ISBN,
                Title = b.Title,
                Year = b.Year,
                Authors = b.Authors.Select(x => new Author() {
                    AuthorId = x.AuthorId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    BirthDate = x.BirthDate
                }).ToList()
            });
        }

        // GET: api/Book/5
        public Book Get(int id) {
            var b = bookRepo.Get(id);

            return new Book() {
                BookId = b.BookId,
                ISBN = b.ISBN,
                Title = b.Title,
                Year = b.Year,
                Authors = b.Authors.Select(x => new Author() {
                    AuthorId = x.AuthorId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    BirthDate = x.BirthDate
                }).ToList()
            };
        }

        // POST: api/Book
        [Authorize]
        public void Post(Book book) {
            bookRepo.Insert(new BookRepository() {
                BookId = book.BookId,
                ISBN = book.ISBN,
                Title = book.Title,
                Year = book.Year,
                Authors = book.Authors.Select(x => new AuthorRepository() {
                    AuthorId = x.AuthorId
                }).ToList()
            });
        }

        // PUT: api/Book/5
        [Authorize]
        public void Put(int id, Book book) {
            book.BookId = id;
            bookRepo.Update(new BookRepository() {
                BookId = book.BookId,
                ISBN = book.ISBN,
                Title = book.Title,
                Year = book.Year
            });
        }

        // DELETE: api/Book/5
        [Authorize]
        public void Delete(int id) {
            bookRepo.Delete(id);
        }
    }
}
