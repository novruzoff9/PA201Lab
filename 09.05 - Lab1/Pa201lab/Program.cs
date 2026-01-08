///* Dəyişən olaraq ad verəcəksiniz. “Salam ad” cixis verilsin. */
//string name = "John";
//Console.WriteLine("Salam " + name);


///*
//    Əgər ədəd müsbətdirsə → “Positive”, mənfidirsə → “Negative”, sıfırdırsa → “Zero”
//*/


    string input = Console.ReadLine();
    int num = Convert.ToInt32(input);
    if (num > 0)
    {
        Console.WriteLine("Positive");
    }
    else if (num < 0)
    {
        Console.WriteLine("Negative");
    }
    else
    {
        Console.WriteLine("Zero");
    }


///*
//    Yaş dəyişənini təyin et:
//    0 - 18: usaq
//    18 - 65 yetiskin
//    65> yasli
//*/
//int age = 25;
//if (age > 0 && age < 18)
//{
//    Console.WriteLine("Usaq");
//}
//else if (age >= 18 && age < 65)
//{
//    Console.WriteLine("yetiskin");
//}
//else if (age >= 65)
//{
//    Console.WriteLine("yasli");
//}

///*
//İstifadəçi balı verilir (0-100). Əgər 90+ → A, 70-89 → B, 50-69 → C, 50-dən aşağı → F
//*/

//int point = -14;
//if (point > 100 || point < 0)
//{
//    Console.WriteLine("Yanlis daxil edilib");
//    return;
//}
//if (point > 90) Console.WriteLine("A");
//else if (point >= 70) Console.WriteLine("B");
//else if (point >= 50) Console.WriteLine("C");
//else Console.WriteLine("F");


///* Dəyişən olaraq ad daxil edeceksiniz. “Salam ad” cixis verilsin. */
//string name = Console.ReadLine();
//Console.WriteLine("Salam " + name);