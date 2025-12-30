using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Mvc.Data;
using Pustok.Mvc.Models;

namespace Pustok.Mvc.Areas.Manage.Controllers;

[Area("Manage")]
public class AuthorController(AppDbContext appDbContext) : Controller
{
    public IActionResult Index()
    {
        var authors = appDbContext
            .Authors
            .Include(a=> a.Books)
            .ToList();
        return View(authors);
    }
    public IActionResult Details(int id)
    {
        var author = appDbContext.Authors
            .Include(a => a.Books)
            .FirstOrDefault(a => a.Id == id);
        
        if (author == null) return NotFound();
        
        return PartialView("_DetailsPartial", author);
    }
    public IActionResult Delete(int id)
    {
        var author = appDbContext.Authors.Find(id);
        if (author == null) return NotFound();
        appDbContext.Authors.Remove(author);
        appDbContext.SaveChanges();
        return Ok();
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Author author)
    {
        if (!ModelState.IsValid)
            return View();
        if (appDbContext.Authors.Any(a=>a.FullName==author.FullName))
        {
            ModelState.AddModelError("FullName", "already exist..");
            return View();
        }
        appDbContext.Authors.Add(author);
        appDbContext.SaveChanges();
        return RedirectToAction("Index");
    }
    public IActionResult Update(int id)
    {
        var author = appDbContext.Authors.Find(id);
        if (author == null) return NotFound();
        return View(author);
    }
    [HttpPost]
    public IActionResult Update(Author author)
    {
        if (!ModelState.IsValid)
            return View();
        var existAuthor = appDbContext.Authors.Find(author.Id);

        if (existAuthor == null) return NotFound();
        if (appDbContext.Authors.Any(a => a.FullName == author.FullName && a.Id != author.Id))
        {
            ModelState.AddModelError("FullName", "already exist..");
            return View();
        }
        existAuthor.FullName = author.FullName;
        appDbContext.SaveChanges();
        return RedirectToAction("Index");
    }
}
