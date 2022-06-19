using GroceryApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        //inject dependencies using constructors
        private readonly GroceryAppContext context;

        public ProductsController(GroceryAppContext context)
        {
            this.context = context;
        }

        //Get/admin/products

        public async Task<IActionResult> Index()
        {
            //Include(x => x.Category) means category to which product is connected to.
            return View(await context.Products.OrderByDescending(x => x.ID).Include(x => x.Category).ToListAsync());
        }
    }
}
