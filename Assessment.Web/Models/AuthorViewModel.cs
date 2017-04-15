using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assessment.Web.Models {
    public class AuthorViewModel {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public ICollection<BookViewModel> Books { get; set; }
    }
}