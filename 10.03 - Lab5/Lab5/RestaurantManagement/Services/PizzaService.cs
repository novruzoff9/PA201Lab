using RestaurantManagement.Models;
using RestaurantManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Services
{
    internal class PizzaService : IPizzaService
    {
        public Pizza BigRadiusPizza(Pizza[] pizzas)
        {
            Pizza? maxRadiusPizza = pizzas.ToList().MaxBy(p => p.Radius) ;
            if(maxRadiusPizza is null)
                throw new NullReferenceException();
           
            return maxRadiusPizza;
        }
    }
}
