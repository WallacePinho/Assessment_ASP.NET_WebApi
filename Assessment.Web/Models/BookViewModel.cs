using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assessment.Web.Models {
    public class BookViewModel {
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public ICollection<SelectListItem> Authors { get; set; }
        public List<int> AuthorsIds { get; set; }
    }
}