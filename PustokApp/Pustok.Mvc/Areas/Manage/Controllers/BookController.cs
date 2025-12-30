using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Mvc.Data;
using Pustok.Mvc.Extensions;
using Pustok.Mvc.Models;

namespace Pustok.Mvc.Areas.Manage.Controllers;
[Area("Manage")]
public class BookController(AppDbContext appDbContext) : Controller
{
    public IActionResult Index()
    {
        var books = appDbContext.Books
            .Include(b => b.Author)
            .ToList();
        return View(books);
    }
    public IActionResult Details(int id)
    {
        var book = appDbContext.Books
            .Include(b => b.Author)
            .Include(b => b.BookImages)
            .Include(b => b.BookTags)
            .ThenInclude(bt => bt.Tag)
            .FirstOrDefault(b => b.Id == id);

        if (book is null)
            return NotFound();

        return PartialView("_BookDetailsPartial", book);
    }
    public IActionResult Create()
    {
        ViewBag.Authors = appDbContext.Authors.ToList();
        ViewBag.Tags = appDbContext.Tags.ToList();
        return View();
    }
    [HttpPost]
    public IActionResult Create(Book book)
    {
        ViewBag.Authors = appDbContext.Authors.ToList();
        ViewBag.Tags = appDbContext.Tags.ToList();
        if (!ModelState.IsValid)
            return View(book);
        var mainFile = book.MainPhoto;
        var hoverFile = book.HoverPhoto;
        var photos = book.Photos;
        if (appDbContext.Books.Any(b => b.Name == book.Name))
        {
            ModelState.AddModelError("Name", "Book name already exists");
            return View(book);
        }
        if (!appDbContext.Authors.Any(a => a.Id == book.AuthorId))
        {
            ModelState.AddModelError("AuthorId", "Author not found");
            return View(book);
        }
        foreach (var tagId in book.TagIds)
        {
            if (!appDbContext.Tags.Any(t => t.Id == tagId))
            {
                ModelState.AddModelError("TagIds", "One or more tags not found");
                return View(book);
            }

        }
        if (mainFile is null)
        {
            ModelState.AddModelError("MainPhoto", "Main photo is required");
            return View(book);

        }
        if (hoverFile is null)
        {
            ModelState.AddModelError("HoverPhoto", "Hover photo is required");
            return View(book);

        }
        book.MainImageUrl = mainFile.SaveFile("wwwroot/assets/image/products");
        book.HoverImageUrl = hoverFile.SaveFile("wwwroot/assets/image/products");
        foreach (var photo in photos)
        {
            var bookImage = new BookImage
            {
                ImageUrl = photo.SaveFile("wwwroot/assets/image/products"),
                BookId = book.Id
            };
            book.BookImages.Add(bookImage);

        }
        foreach (var tagId in book.TagIds)
        {
            var bookTag = new BookTag
            {
                Book = book,
                TagId = tagId
            };
            book.BookTags.Add(bookTag);
        }
        appDbContext.Books.Add(book);
        appDbContext.SaveChanges();
        return RedirectToAction("index");
    }
    public IActionResult Update(int id)
    {
        var book = appDbContext.Books
            .Include(b => b.BookImages)
            .Include(b => b.BookTags)
            .FirstOrDefault(b => b.Id == id);
        if (book is null)
            return NotFound();
        ViewBag.Authors = appDbContext.Authors.ToList();
        ViewBag.Tags = appDbContext.Tags.ToList();
        book.TagIds = book.BookTags.Select(bt => bt.TagId).ToList();
        return View(book);
    }
    public IActionResult DeleteImage(int imageid)
    {
        var bookImage = appDbContext.BookImage.FirstOrDefault(bi => bi.Id == imageid);
        if (bookImage is null)
            return NotFound();
        appDbContext.BookImage.Remove(bookImage);
        appDbContext.SaveChanges();
        return RedirectToAction("update", new { id = bookImage.BookId });
    }
    [HttpPost]
    public IActionResult Update(Book book)
    {
        ViewBag.Authors = appDbContext.Authors.ToList();
        ViewBag.Tags = appDbContext.Tags.ToList();
        if (!ModelState.IsValid)
            return View(book);
        var existingBook = appDbContext.Books
            .Include(b => b.BookTags)
            .FirstOrDefault(b => b.Id == book.Id);
        if (existingBook is null)
            return NotFound();

        if (appDbContext.Books.Any(b => b.Name == book.Name && b.Id != book.Id))
        {
            ModelState.AddModelError("Name", "Book name already exists");
            return View(book);
        }
        var mainFile = book.MainPhoto;
        var hoverFile = book.HoverPhoto;
        var photos = book.Photos;
        if (!appDbContext.Authors.Any(a => a.Id == book.AuthorId))
        {
            ModelState.AddModelError("AuthorId", "Author not found");
            return View(book);
        }
        foreach (var tagId in book.TagIds)
        {
            if (!appDbContext.Tags.Any(t => t.Id == tagId))
            {
                ModelState.AddModelError("TagIds", "One or more tags not found");
                return View(book);
            }
        }
        var oldMainImageUrl = existingBook.MainImageUrl;
        string folderPath = "wwwroot/assets/image/products";
        string mainImagePath = Path.Combine(Directory.GetCurrentDirectory(), folderPath, oldMainImageUrl);
        if (mainFile is not null)
        {
            existingBook.MainImageUrl = mainFile.SaveFile(folderPath);
            FileManager.DeleteFile(mainImagePath);
        }
        var oldHoverImageUrl = existingBook.HoverImageUrl;
        string hoverImagePath = Path.Combine(Directory.GetCurrentDirectory(), folderPath, oldHoverImageUrl);
        if (hoverFile is not null)
        {
            existingBook.HoverImageUrl = hoverFile.SaveFile(folderPath);
            FileManager.DeleteFile(hoverImagePath);
        }
        if (photos is not null)
        {
            foreach (var photo in photos)
            {
                var bookImage = new BookImage
                {
                    ImageUrl = photo.SaveFile(folderPath),
                    BookId = book.Id
                };
                existingBook.BookImages.Add(bookImage);
            }
        }
        // Update BookTags 1.2.3 =>1,3,4
        var existingTagIds = existingBook.BookTags.Select(bt => bt.TagId).ToList();
        var newTagIds = book.TagIds;
        var tagsToAdd = newTagIds.Except(existingTagIds).ToList();
        var tagsToRemove = existingTagIds.Except(newTagIds).ToList();
        foreach (var tagId in tagsToAdd)
        {
            var bookTag = new BookTag
            {
                BookId = book.Id,
                TagId = tagId
            };
            existingBook.BookTags.Add(bookTag);
        }
        foreach (var tagId in tagsToRemove)
        {
            var bookTag = existingBook.BookTags.FirstOrDefault(bt => bt.TagId == tagId);
            if (bookTag is not null)
            {
                existingBook.BookTags.Remove(bookTag);
            }
        }
        existingBook.Name = book.Name;
        existingBook.Description = book.Description;
        existingBook.Price = book.Price;
        existingBook.DiscountPercent = book.DiscountPercent;
        existingBook.Code = book.Code;
        existingBook.InStock = book.InStock;
        existingBook.IsFeatured = book.IsFeatured;
        existingBook.IsNew = book.IsNew;
        existingBook.AuthorId = book.AuthorId;
        appDbContext.SaveChanges();
        return RedirectToAction("index");
    }
    public IActionResult Delete(int id)
    {
        var book = appDbContext.Books.FirstOrDefault(b => b.Id == id);
        if (book is null)
            return NotFound();
        appDbContext.Books.Remove(book);
        appDbContext.SaveChanges();
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/image/products", book.MainImageUrl);
        FileManager.DeleteFile(path);
        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/image/products", book.HoverImageUrl);
        FileManager.DeleteFile(path);
        foreach (var image in book.BookImages)
        {
            path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/image/products", image.ImageUrl);
            FileManager.DeleteFile(path);

        }
        return Ok();
    }
}