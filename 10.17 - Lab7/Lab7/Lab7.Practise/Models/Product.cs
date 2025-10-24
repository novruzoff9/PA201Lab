namespace Lab7.Practise.Models;

public class Product
{
    private static int _counter = 100;
    public Product(string name, int count, double price)
    {
        Id = _counter;
        Name = name;
        Count = count;
        Price = price;
        _counter++;
    }

    public int Id { get; set; }
    private string name;
    public string Name { 
        get
        {
            return name;
        }
        set
        {
            if(value.Length > 2)
                name = value;
            else
                Console.WriteLine("2 simvoldan az qebul olunmur");
        }
    }
    public int Count { get; set; }
    public double Price { get; set; }
    public double TotalPrice()
    {
        Console.WriteLine("Mebleg hesablandi");
        return Count * Price;
    }
    public override string ToString()
    {
        return $"Id {Id} Name {Name}";
    }
}