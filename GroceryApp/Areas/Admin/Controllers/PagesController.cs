using GroceryApp.Infrastructure;
using GroceryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryApp.Areas.Admin.Controllers
{
    // set area attribute in the class
    [Area("Admin")]
    public class PagesController : Controller
    {
        //inject dependencies using constructors
        private readonly GroceryAppContext context;

        public PagesController(GroceryAppContext context)
        {
            this.context = context;
        }

        //Index method to return all the pages.
        // GET /admin/pages
        public async Task<IActionResult> Index()
        {
            IQueryable<Page> pages = from p in context.Pages orderby p.Sorting select p;

            List<Page> pagesList = await pages.ToListAsync();
            return View(pagesList);
        }

        // GET /admin/pages/details/5     here 5 is some id
        public async Task<IActionResult> Details(int id)
        {
            Page page = await context.Pages.FirstOrDefaultAsync(x => x.ID == id);
            //check page is null or not
           if(page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // GET /admin/pages/create    
        public IActionResult Create() => View();

        // Post /admin/pages/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            // check validation has no error
            if (ModelState.IsValid)
            {
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                page.Sorting = 100;

                var slug = await context.Pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if(slug != null)
                {
                    //validation : means title is already exist
                    ModelState.AddModelError("", "The Title already exists.");
                    return View(page);
                }

                context.Add(page);
                await context.SaveChangesAsync();

                TempData["Success"] = "The Page has been added!";
                return RedirectToAction("Index");
            }
            return View(page);
        }

        // GET /admin/pages/edit/5     here 5 is some id
        public async Task<IActionResult> Edit(int id)
        {
            Page page = await context.Pages.FindAsync(id);
            //check page is null or not
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }


        // Post /admin/pages/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Page page)
        {

            // check validation has no error
            if (ModelState.IsValid)
            {

                //check if home page is being edited because if it is, then we dont't add slug
                page.Slug = page.ID == 1 ? "home" : page.Title.ToLower().Replace(" ", "-");
                

                var slug = await context.Pages.Where(x => x.ID != page.ID).FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {
                    //validation : means title is already exist
                    ModelState.AddModelError("", "The Title already exists.");
                    return View(page);
                }

                context.Update(page);
                await context.SaveChangesAsync();

                TempData["Success"] = "The Page has been Edited!";
                return RedirectToAction("Edit", new { id = page.ID });

            }
            return View(page);
        }

        // GET /admin/pages/delete/5     
        public async Task<IActionResult> Delete(int id)
        {
            Page page = await context.Pages.FindAsync(id);
            //check page is null or not
            if (page == null)
            {
                TempData["Error"] = "The Page does not exist!";
            }
            else
            {
                context.Pages.Remove(page);
                await context.SaveChangesAsync();

                TempData["Success"] = "The Page has been deleted!";
            }
            return RedirectToAction("Index");
        }


        // Post /admin/pages/reorder
        [HttpPost]
        
        public async Task<IActionResult> Reorder(int [] Id)
        {
            int count = 1;

            foreach (var pageId in Id)
            {
                Page page = await context.Pages.FindAsync(pageId);
                page.Sorting = count;
                context.Update(page);
                await context.SaveChangesAsync();
                count++;
            }
            return Ok();

        }
          
        }
    }

