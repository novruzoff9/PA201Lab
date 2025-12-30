using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Dto.Categories;
using WebApi.Dto.Products;
using WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController
    (AppDbContext dbContext,
    ILogger<CategoriesController> logger,
    IConfiguration configuration)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await dbContext.Categories
            .Include(c => c.Products)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products.Select(p => new CategoryProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock
                }).ToList()
            })
            .ToListAsync();

        return Ok(categories);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto categoryDto)
    {
        var category = new Category
        {
            Name = categoryDto.Name
        };
        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync();
        var user = HttpContext.User.FindFirst(ClaimTypes.Name);
        logger.LogInformation("Category created by {User}: {CategoryName}", user?.Value, category.Name);


        var message = $"New category created: {category.Name} by {user?.Value}";

        string baseUrl = "https://api.telegram.org/";
        string botToken = configuration["Telegram:BotToken"];
        string chatId = configuration["Telegram:ChatId"];

        string requestUrl = $"{baseUrl}bot{botToken}/sendMessage?chat_id={chatId}&text={message}";
        HttpClient client = new HttpClient();
        await client.GetAsync(requestUrl);
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await dbContext.Categories
            .Include(c => c.Products)
            .Where(c => c.Id == id)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products.Select(p => new CategoryProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto categoryDto)
    {
        var category = await dbContext.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        category.Name = categoryDto.Name;
        await dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await dbContext.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        dbContext.Categories.Remove(category);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}/products")]
    public async Task<IActionResult> GetCategoryProducts(int id)
    {
        var category = await dbContext.Categories
            .Include(x=>x.Products).FirstOrDefaultAsync(c => c.Id == id);
        var products = category.Products.Select(x => new ProductDto
        {
            Name = x.Name,
            Id = x.Id,
            Price = x.Price,
            Stock = x.Stock,
            CategoryName = x.Category.Name
        });
        return Ok(products);
    }
}