using System.Text;

namespace Lab4.Models;

public class Order
{
    public int id;
    public string name;
    public int count;
    public decimal price;

    public Order(int id, string name, int count, decimal price)
    {
        this.id = id;
        this.name = name;
        this.count = count;
        this.price = price;
    }

    public Order(int id, string name, decimal price) : this(id, name, 1, price) { }

    public decimal TotalPrice()
    {
        return count * price;
    }
    public void IncreaseCount(int n)
    {
        count += n;
    }

    public string FullInfo()
    {
        StringBuilder sb= new StringBuilder();
        sb.AppendLine($"Id : {id}");
        sb.AppendLine($"Name : {name}");
        sb.AppendLine($"Count : {count}");
        sb.AppendLine($"Price : {price}");
        sb.AppendLine($"Total Price : {TotalPrice()}");
        return sb.ToString();
    }
    public void ChangePrice(decimal newPrice, out decimal diff)
    {
        diff = newPrice - price ;
        price = newPrice;
    }
}
