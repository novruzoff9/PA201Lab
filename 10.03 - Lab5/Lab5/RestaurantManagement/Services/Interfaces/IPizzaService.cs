using RestaurantManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Services.Interfaces
{
    internal interface IPizzaService
    {
         Pizza BigRadiusPizza(Pizza[] pizzas);
    }
}
