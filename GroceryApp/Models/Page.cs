using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryApp.Models
{
    public class Page
    {

        // write "prop" and double tab
        //automatically represented as primary key
        public int ID { get; set; }

        //validation
        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string Title { get; set; }
        
        public string Slug { get; set; }

        [Required, MinLength(5, ErrorMessage = "Minimum length is 5")]
        public string Content { get; set; }
        public int Sorting { get; set; }
    }
}
