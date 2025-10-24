using Lab7.Practise.Models;
using Lab7.Practise.Services;

Product product = new("apple", 3, 1.2);
Product product1 = new("apple2", 2, 3.5);

ProductService service = new();
service.AddProduct(product);
service.AddProduct(product1);

foreach (var item in service.Products)
{
    Console.WriteLine(item);
}
service.EditProduct(100, "Armud", 3.1, 5);
Console.WriteLine(new string('-', 50));

service.RemoveProduct(301);

foreach (var item in service.Products)
{
    Console.WriteLine(item);
}

//Console.WriteLine(service.GetById(205));