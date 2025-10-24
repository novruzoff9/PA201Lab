using Lab7.Practise.Interface;
using Lab7.Practise.Models;

namespace Lab7.Practise.Services;

public class ProductService : IProductService
{
    public List<Product> Products { get; } = [];
    public void AddProduct(Product product)
    {
        Products.Add(product);
    }
    public void EditProduct(int id, string name, double price, int count)
    {
        var product = GetById(id);
        product.Name = name;
        product.Price = price;
        product.Count = count;
    }
    public Product GetById(int id)
    {
        var product = Products.FirstOrDefault(x => x.Id == id);
        if(product is null)
        {
            Console.WriteLine("Bu Id-de deyer yoxdur");
        }
        return product;
    }
    public void RemoveProduct(int id)
    {
        var product = GetById(id);
        Products.Remove(product);
    }
}