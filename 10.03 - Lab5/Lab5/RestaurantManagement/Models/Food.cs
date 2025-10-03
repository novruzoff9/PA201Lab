namespace RestaurantManagement.Models;

internal abstract class Food
{
    public int Id { get; set; }
    public string Name { get; set; }
    private DateTime _createdAt;
    public DateTime CreatedAt
    {
        get
        {
            return _createdAt;
        }
        set
        {
            if (value > DateTime.Now)
            {
                Console.WriteLine("ola bilmez");
            }
            else
            {
                _createdAt = value;
            }
        }
    }
    public TimeSpan PrepareTime { get; set; }
    private double _calory;
    public double Calory
    {
        get => _calory;
        set
        {
            if (value < 0) Console.WriteLine("Calari menfi eded ola bilmez");
            else _calory = value;
        }
    }
    abstract public double CalcPrice();
}