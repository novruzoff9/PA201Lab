namespace RestaurantManagement.Models;

internal class Pizza : Food
{
    public double Radius { get; set; }

    public bool IsSpicy { get; set; }

    public override double CalcPrice()
    {

        double price = 3 * Radius * Radius;
        price *= 0.10;
        if (IsSpicy)
            price += 1.15;
        return price;
    }
    public override string ToString()
    {
        return $"Pizza: {Name}, Radius: {Radius}, Calory: {Calory}, IsSpicy: {IsSpicy}, CreatedAt: {CreatedAt}, Price: {CalcPrice()}";
    }
}
