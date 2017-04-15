using Assessment.Data;
using Assessment.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core {
    public class BookRepository : IRepository<BookRepository> {
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public virtual ICollection<AuthorRepository> Authors { get; set; }

        private DataContext _dataContext;

        public BookRepository() {
            _dataContext = new DataContext();
        }

        public ICollection<BookRepository> GetAll() {
            var books = from b in _dataContext.Books
                        select new BookRepository() {
                            BookId = b.BookId,
                            ISBN = b.ISBN,
                            Title = b.Title,
                            Year = b.Year,
                            Authors = b.Authors.Select(x=> new AuthorRepository() {
                                AuthorId = x.AuthorId,
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                Email = x.Email,
                                BirthDate = x.BirthDate
                            }).ToList()
                        };

            return books.ToList();
        }

        public bool Insert(BookRepository entity) {
            try {
                var book = new Book() {
                    BookId = entity.BookId,
                    ISBN = entity.ISBN,
                    Title = entity.Title,
                    Year = entity.Year
                };

                _dataContext.Books.Add(book);
                foreach(var author in entity.Authors) {
                    var aut = (from a in _dataContext.Authors where author.AuthorId == a.AuthorId select a).First();
                    aut.Books.Add(book);
                }
                _dataContext.SaveChanges();
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public bool Update(BookRepository entity) {
            try {
                var book = _dataContext.Books.FirstOrDefault(x => x.BookId == entity.BookId);
                if (book == null) return false;
                _dataContext.Entry(book).CurrentValues.SetValues(entity);
                _dataContext.SaveChanges();
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public BookRepository Get(int id) {
            var book = _dataContext.Books.FirstOrDefault(x => x.BookId == id);
            if (book == null) return null;
            return new BookRepository() {
                BookId = book.BookId,
                ISBN = book.ISBN,
                Title = book.Title,
                Year = book.Year,
                Authors = book.Authors.Select(x => new AuthorRepository() {
                    AuthorId = x.AuthorId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    BirthDate = x.BirthDate
                }).ToList()
            };
        }

        public bool Delete(int id) {
            try {
                var book = _dataContext.Books.FirstOrDefault(x => x.BookId == id);
                if (book == null) return false;
                _dataContext.Books.Remove(book);
                _dataContext.SaveChanges();
                return true;
            } catch (Exception ex) {
                return false;
            }
        }
    }
}
