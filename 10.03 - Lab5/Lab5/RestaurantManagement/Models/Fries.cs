namespace RestaurantManagement.Models;

internal class Fries : Food
{
    public double Weight { get; set; }

    public bool IsSalty { get; set; }

    public override double CalcPrice()
    {
        double price = Math.Ceiling(Weight);
        if (IsSalty)
        price += 0.80;
        return price;
    }
}
