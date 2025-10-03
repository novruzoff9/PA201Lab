using RestaurantManagement.Models;

namespace RestaurantManagement.Services.Interfaces;

internal interface IFoodService
{
    DateTime WhenWillPrepared(Food food);
    int MostCaloryCount(Food[] foods, int n);
}
