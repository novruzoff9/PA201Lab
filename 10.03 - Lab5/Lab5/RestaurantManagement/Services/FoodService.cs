using RestaurantManagement.Models;
using RestaurantManagement.Services.Interfaces;

namespace RestaurantManagement.Services;

internal class FoodService : IFoodService
{
    public int MostCaloryCount(Food[] foods, int n)
    {
        int count = foods.Count(x => x.Calory > n);
        return count;
    }

    public DateTime WhenWillPrepared(Food food)
    {
        DateTime dateTime = food.CreatedAt + food.PrepareTime;
        return dateTime;
    }
}
