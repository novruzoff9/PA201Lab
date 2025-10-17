namespace Lab6.Models;

public class Student
{
    public int Age { get; set; }
    public string Name { get; set; } 

    public int GetClass()
    {
        return Age - 6; 
    }
}
