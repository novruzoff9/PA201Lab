using Lab4.Models;
using System.Text;

Order order1 = new(2, "Armud", 2, 1.2m);

Console.WriteLine(order1.TotalPrice());

order1.IncreaseCount(4);
Console.WriteLine(order1.TotalPrice());

Console.WriteLine(order1.FullInfo());


decimal diff;

order1.ChangePrice(1.50m, out diff);
Console.WriteLine(order1.FullInfo());
Console.WriteLine(diff);


Order order3 = new();

OnlineOrder onlineOrder = new();