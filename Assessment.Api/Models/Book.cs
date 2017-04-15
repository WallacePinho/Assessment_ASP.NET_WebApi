using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment.Api.Models {
    public class Book {
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}