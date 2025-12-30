using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Mvc.Data;
using Pustok.Mvc.Models;
using Pustok.Mvc.ViewModels;

namespace Pustok.Mvc.Controllers
{
    public class HomeController(AppDbContext dbContext) : Controller
    {

        public IActionResult Index()
        {
            HomeVm homeVm = new HomeVm
            {
                Sliders = dbContext.Sliders.ToList(),

                FeaturedBooks = dbContext.Books
                .Include(x=>x.Author)
                .Include(x=>x.BookImages)
                .Where(x => x.IsFeatured).ToList(),

                NewBooks = dbContext.Books
                .Include(x => x.Author)
                .Include(x => x.BookImages)
                .Where(x => x.IsNew).ToList(),

                DiscountedBooks = dbContext.Books
                .Include(x => x.Author)
                .Include(x => x.BookImages)
                .Where(x => x.DiscountPercent > 0).ToList()
            };
            return View(homeVm);
        }
    
        public IActionResult Test()
        {
            var books = dbContext.Books
                .Include(x => x.Author)
                .Include(x => x.BookImages)
                .Where(x => x.IsFeatured).ToList();
            return PartialView("_BooksPartial", books);
        }
    }
}
