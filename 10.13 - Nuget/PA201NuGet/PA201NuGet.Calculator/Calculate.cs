namespace PA201NuGet;

public class Calculate
{
    public int Add(int a, int b) => a + b;
    public int Subtract(int a, int b) => a - b;
    public int Multiply(int a, int b) => a * b;
    public double Divide(int a, int b)
    {
        if (b == 0)
            Console.WriteLine("Bolen 0 ola bilmez");
        return (double)a / b;
    }
}
