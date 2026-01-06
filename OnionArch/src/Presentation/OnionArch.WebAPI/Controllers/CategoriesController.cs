using Microsoft.AspNetCore.Mvc;
using OnionArch.Application.Dtos.Category;
using OnionArch.Application.Services.Interfaces;

namespace OnionArch.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var response = await categoryService.GetAllCategoriesAsync();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Crete(CategoryCreateDto createDto)
    {
        var response = await categoryService.CreateCategoryAsync(createDto);
        return Ok(response);
    }
}
