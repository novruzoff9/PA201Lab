namespace WebApi.Dto.Categories;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<CategoryProductDto> Products { get; set; } = new();
}

public class CategoryProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
