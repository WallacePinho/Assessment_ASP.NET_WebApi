using Assessment.Api.Models;
using Assessment.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assessment.Api.Controllers {
    public class AuthorController : ApiController {

        private AuthorRepository authorRepo;

        public AuthorController() {
            authorRepo = new AuthorRepository();
        }

        // GET: api/Author
        public IEnumerable<Author> Get() {
            var authors = authorRepo.GetAll();
            return authors.Select(x => new Author() {
                AuthorId = x.AuthorId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                BirthDate = x.BirthDate,
                Books = x.Books.Select(b => new Book() {
                    BookId = b.BookId,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    Year = b.Year
                }).ToList()
            });
        }

        // GET: api/Author/5
        public Author Get(int id) {
            var aut = authorRepo.Get(id);
            return new Author() {
                AuthorId = aut.AuthorId,
                FirstName = aut.FirstName,
                LastName = aut.LastName,
                Email = aut.Email,
                BirthDate = aut.BirthDate,
                Books = aut.Books.Select(b => new Book() {
                    BookId = b.BookId,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    Year = b.Year
                }).ToList()
            };
        }

        // POST: api/Author
        [Authorize]
        public void Post(Author author) {
            authorRepo.Insert(new AuthorRepository() {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email,
                BirthDate = author.BirthDate
            });
        }

        // PUT: api/Author/5
        [Authorize]
        public void Put(int id, Author author) {
            author.AuthorId = id;
            authorRepo.Update(new AuthorRepository() {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email,
                BirthDate = author.BirthDate
            });
        }

        // DELETE: api/Author/5
        [Authorize]
        public void Delete(int id) {
            authorRepo.Delete(id);
        }
    }
}
