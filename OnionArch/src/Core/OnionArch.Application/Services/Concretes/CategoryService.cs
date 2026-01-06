using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OnionArch.Application.Dtos.Category;
using OnionArch.Application.Interfaces;
using OnionArch.Application.Models;
using OnionArch.Application.Services.Interfaces;
using OnionArch.Domain.Entities;

namespace OnionArch.Application.Services.Concretes;

public class CategoryService(
    IValidator<CategoryCreateDto> createValidator,
    IApplicationDbContext dbContext,
    IMapper mapper
    ) : ICategoryService
{
    public async Task<ResponseModel<CategoryReturnDto>> CreateCategoryAsync(CategoryCreateDto categoryDto)
    {
        if (await dbContext.Categories.AnyAsync(c => c.Name == categoryDto.Name))
            throw new Exception("Category with the same name already exists.");

        var validationResult = await createValidator.ValidateAsync(categoryDto);
        if (!validationResult.IsValid)
            return ResponseModel<CategoryReturnDto>.Fail(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                );

        var category = mapper.Map<Category>(categoryDto);
        await dbContext.Categories.AddAsync(category);
        await dbContext.SaveChangesAsync();

        var categoryReturnDto = mapper.Map<CategoryReturnDto>(category);
        return ResponseModel<CategoryReturnDto>.Success(categoryReturnDto);
    }

    public async Task<ResponseModel<List<CategoryReturnDto>>> GetAllCategoriesAsync()
    {
        var categories = await dbContext.Categories
            .ToListAsync();

        var categoryDtos = mapper.Map<List<CategoryReturnDto>>(categories);
        return ResponseModel<List<CategoryReturnDto>>.Success(categoryDtos);
    }
}
