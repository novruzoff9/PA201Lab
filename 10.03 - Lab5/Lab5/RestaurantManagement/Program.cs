using RestaurantManagement.Models;
using RestaurantManagement.Services;
using RestaurantManagement.Services.Interfaces;

var pizza = new Pizza() {
    Name = "Margherita",
    Radius = 12,
    Calory = 250,
    IsSpicy = false,
    PrepareTime = new(0, 4, 26),
    CreatedAt = DateTime.Now };

//Console.WriteLine(pizza);

IFoodService foodService = new FoodService();
var prepareTime = foodService.WhenWillPrepared(pizza);
Console.WriteLine(prepareTime.ToString("yyyy.MM.dd HH:mm:ss"));


Pizza[] pizzas = new Pizza[5]
{
    new() { Name = "Margherita", Radius = 12, Calory = 250, IsSpicy = false, PrepareTime = TimeSpan.FromMinutes(15), CreatedAt = DateTime.Now },
    new() { Name = "Pepperoni", Radius = 14, Calory = 320, IsSpicy = true, PrepareTime = TimeSpan.FromMinutes(18), CreatedAt = DateTime.Now },
    new() { Name = "BBQ Chicken", Radius = 16, Calory = 400, IsSpicy = true, PrepareTime = TimeSpan.FromMinutes(20), CreatedAt = DateTime.Now },
    new() { Name = "Veggie", Radius = 10, Calory = 220, IsSpicy = false, PrepareTime = TimeSpan.FromMinutes(12), CreatedAt = DateTime.Now },
    new Pizza { Name = "Hawaiian", Radius = 13, Calory = 280, IsSpicy = false, PrepareTime = TimeSpan.FromMinutes(17), CreatedAt = DateTime.Now }
};

int count = foodService.MostCaloryCount(pizzas, 300);
Console.WriteLine(count);

IPizzaService pizzaService = new PizzaService();
Pizza pizza1 = pizzaService.BigRadiusPizza(pizzas);
Console.WriteLine(pizza1);