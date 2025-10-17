namespace Lab6.Models;

public class Product
{
    private double price;
    public double Price
    {
        get
        {
            return price;
        }
        set
        {
            if(price < 0)
                Console.WriteLine("Menfi ola bilmez");
            else
                price = value;
        }
    }
    private string name;
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            if (value.Length < 3)
                Console.WriteLine("3 simvoldan kicik ola bilmez");
            else
                name = value;
        }
    }
    public int Count { get; set; }
    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }
    public Product(string name, double price, double double2)
    {
        Name = name;
    }

    public double TotalPrice()
    {
        Console.WriteLine("Mebleg hesablandi");
        return Price * Count;
    }
}
