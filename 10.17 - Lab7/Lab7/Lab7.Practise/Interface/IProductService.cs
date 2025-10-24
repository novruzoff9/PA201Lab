using Lab7.Practise.Models;

namespace Lab7.Practise.Interface;

public interface IProductService
{
    public List<Product> Products { get; }
    void AddProduct(Product product);
    Product GetById(int id);
    void RemoveProduct(int id);
    void EditProduct(int id, string name, double price, int count);
}
