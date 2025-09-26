namespace Lab4.Models;

public class OnlineOrder : Order
{
    public int DeliveryDistance { get; set; }
    public OnlineOrder(int id, string name, decimal price, int deliveryDistance, int count = 1) : base(id, name, count, price)
    {
        DeliveryDistance = deliveryDistance;
    }
}
