using Assessment.Data;
using Assessment.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core {
    public class AuthorRepository : IRepository<AuthorRepository> {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public virtual ICollection<BookRepository> Books { get; set; }

        private DataContext _dataContext;

        public AuthorRepository() {
            _dataContext = new DataContext();
        }

        public ICollection<AuthorRepository> GetAll() {
            var authors = from a in _dataContext.Authors
                          select new AuthorRepository() {
                              AuthorId = a.AuthorId,
                              FirstName = a.FirstName,
                              LastName = a.LastName,
                              Email = a.Email,
                              BirthDate = a.BirthDate,
                              Books = a.Books.Select(x => new BookRepository() {
                                  BookId = x.BookId,
                                  Title = x.Title,
                                  ISBN = x.ISBN,
                                  Year = x.Year
                              }).ToList()
                          };
            return authors.ToList();
        }

        public bool Insert(AuthorRepository entity) {
            try {
                _dataContext.Authors.Add(new Author() {
                    AuthorId = entity.AuthorId,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Email = entity.Email,
                    BirthDate = entity.BirthDate
                });
                _dataContext.SaveChanges();
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public bool Update(AuthorRepository entity) {
            try {
                var author = _dataContext.Authors.FirstOrDefault(x => x.AuthorId == entity.AuthorId);
                if (author == null) return false;
                _dataContext.Entry(author).CurrentValues.SetValues(entity);
                _dataContext.SaveChanges();
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public AuthorRepository Get(int id) {
            var author = _dataContext.Authors.FirstOrDefault(x => x.AuthorId == id);
            if (author == null) return null;
            return new AuthorRepository() {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email,
                BirthDate = author.BirthDate,
                Books = author.Books.Select(x => new BookRepository() {
                    BookId = x.BookId,
                    Title = x.Title,
                    ISBN = x.ISBN,
                    Year = x.Year
                }).ToList()
            };
        }

        public bool Delete(int id) {
            try {
                var author = _dataContext.Authors.FirstOrDefault(x => x.AuthorId == id);
                if (author == null) return false;
                _dataContext.Authors.Remove(author);
                _dataContext.SaveChanges();
                return true;
            } catch (Exception ex) {
                return false;
            }
        }
    }
}
