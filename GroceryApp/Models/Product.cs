using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryApp.Models
{
    public class Product
    {
        
        public int ID { get; set; }

        //validation
        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
       // [RegularExpression(@"^[a-zA-Z-]+$", ErrorMessage = "Only letters are allowed")]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }

        //making category id foreign key
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set;}
    }
}

