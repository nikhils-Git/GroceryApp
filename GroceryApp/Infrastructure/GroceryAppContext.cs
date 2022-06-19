using GroceryApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryApp.Infrastructure
{
    public class GroceryAppContext : DbContext
    {
        public  GroceryAppContext(DbContextOptions<GroceryAppContext> options)
            :base(options)
        {

        }

        //add page, category, product to the context
        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
