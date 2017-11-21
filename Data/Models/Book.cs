using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Book
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public DateTime? PublishDate { get; set; }

        public string PublishingCompany { get; set; }

        public int? Pages { get; set; }
    }
}
