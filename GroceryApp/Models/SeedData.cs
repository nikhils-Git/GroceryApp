using GroceryApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new GroceryAppContext(serviceProvider.GetRequiredService<DbContextOptions< GroceryAppContext>>()))
            {
                // now we have access to db
                // now we check if there are any rows available in "pages" table, if there are...then we dont have to do anything

                if(context.Pages.Any())
                {
                    return;
                }

                //else we gonna add pages

                context.Pages.AddRange(
                    new Page
                    {
                        Title = "Home",
                        //Slug is url friendly representation of a title.
                        Slug = "home",
                        Content = "home page",
                        Sorting = 0
                    },
                    new Page
                    {
                        Title = "About Us",
                        Slug = "about-us",
                        Content = "about us page",
                        Sorting = 100
                    },
                     new Page
                     {
                         Title = "Services",
                         Slug = "services",
                         Content = "services page",
                         Sorting = 100
                     },
                      new Page
                      {
                          Title = "Contact",
                          Slug = "contact",
                          Content = "contact page",
                          Sorting = 100
                      }

                    );

                context.SaveChanges(); 

                
            }
        }
    }
}
