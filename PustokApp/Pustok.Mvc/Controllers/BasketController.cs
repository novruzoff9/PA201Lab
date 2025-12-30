using Microsoft.AspNetCore.Mvc;
using Pustok.Mvc.Models;
using System.Text.Json;

namespace Pustok.Mvc.Controllers;
public class BasketController : Controller
{
    public IActionResult Index()
    {
        SetCookie();
        var book = GetCookie();
        return Json(book);
    }
    public IActionResult SetSession()
    {
        Book book = new Book
        {
            Id = 1,
            Name = "Book 1",
            Price = 20
        };
        HttpContext.Session.SetString("book", JsonSerializer.Serialize(book));
        return Content("Set Session");
    }
    public IActionResult GetSession()
    {
        var book = HttpContext.Session.GetString("book");
        return Json(JsonSerializer.Deserialize<Book>(book));
    }
    public IActionResult RemoveSession()
    {
        HttpContext.Session.Remove("book");
        return Content("Removed");
    }
    public IActionResult SetCookie()
    {
        Book book = new Book
        {
            Id = 1,
            Name = "Book 1",
            Price = 20
        };
        Response.Cookies.Append("book", JsonSerializer.Serialize(book), new() { MaxAge=TimeSpan.FromMinutes(2)});
        return Content("Set Cookie");
    }
    public IActionResult GetCookie()
    {
        var book = Request.Cookies["book"];
        return Json(JsonSerializer.Deserialize<Book>(book));
    }
}
