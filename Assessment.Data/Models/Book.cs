using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Data.Models {
    public class Book {
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
    }
}
