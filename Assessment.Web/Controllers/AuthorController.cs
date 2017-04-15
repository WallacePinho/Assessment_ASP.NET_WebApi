using Assessment.Core;
using Assessment.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment.Web.Controllers {
    public class AuthorController : Controller {

        private AuthorRepository authorRepo;

        public AuthorController() {
            authorRepo = new AuthorRepository();
        }

        // GET: Author
        [Authorize]
        public ActionResult Index() {
            var authors = authorRepo.GetAll();
            return View(authors.Select(x => new AuthorViewModel() {
                AuthorId = x.AuthorId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                BirthDate = x.BirthDate,
                Books = x.Books.Select(b => new BookViewModel() {

                }).ToList()
            }));
        }

        // GET: Author/Details/5
        [Authorize]
        public ActionResult Details(int id) {
            var aut = authorRepo.Get(id);
            return View(new AuthorViewModel() {
                AuthorId = aut.AuthorId,
                FirstName = aut.FirstName,
                LastName = aut.LastName,
                Email = aut.Email,
                BirthDate = aut.BirthDate,
                Books = aut.Books.Select(b => new BookViewModel() {
                }).ToList()
            });
        }

        // GET: Author/Create
        [Authorize]
        public ActionResult Create() {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorViewModel author) {
            try {
                authorRepo.Insert(new AuthorRepository() {
                    AuthorId = author.AuthorId,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Email = author.Email,
                    BirthDate = author.BirthDate
                });
                return RedirectToAction("Index");
            } catch (Exception ex) {
                return View();
            }
        }

        // GET: Author/Edit/5
        [Authorize]
        public ActionResult Edit(int id) {
            var aut = authorRepo.Get(id);
            return View(new AuthorViewModel() {
                AuthorId = aut.AuthorId,
                FirstName = aut.FirstName,
                LastName = aut.LastName,
                Email = aut.Email,
                BirthDate = aut.BirthDate
            });
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(int id, AuthorViewModel author) {
            try {
                author.AuthorId = id;
                authorRepo.Update(new AuthorRepository() {
                    AuthorId = author.AuthorId,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Email = author.Email,
                    BirthDate = author.BirthDate
                });

                return RedirectToAction("Index");
            } catch (Exception ex) {
                return View();
            }
        }

        // GET: Author/Delete/5
        [Authorize]
        public ActionResult Delete(int id) {
            var aut = authorRepo.Get(id);
            return View(new AuthorViewModel() {
                AuthorId = aut.AuthorId,
                FirstName = aut.FirstName,
                LastName = aut.LastName,
                Email = aut.Email,
                BirthDate = aut.BirthDate
            });
        }

        // POST: Author/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(int id, AuthorViewModel author) {
            try {
                authorRepo.Delete(id);

                return RedirectToAction("Index");
            } catch (Exception ex){
                return View();
            }
        }
    }
}
