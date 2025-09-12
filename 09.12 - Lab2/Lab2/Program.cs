/*
 n sayda daxil edilen ededin ceminin tapilmasi
*/
int n = Convert.ToInt32(Console.ReadLine());
int sum = 0;
for (int i = 0; i < n; i++)
{
    int m = Convert.ToInt32(Console.ReadLine());
    sum += m;
}
Console.WriteLine(sum);

/*
    Istifadeci ‘0’ daxil edene qeder, daxil edilen ededlerin hasilinin tapilmasi
*/
int result = 1;
while (true)
{
    int n = Convert.ToInt32(Console.ReadLine());
    if (n == 0)
    {
        break;
    }
    result = result * n;
}
Console.WriteLine(result);

/*
 Baslangicda login ucun bir parol teyin olunsun. Istifadeci duzgun daxil edene qeder inputa versin. Yanlış olduğu halda (“Tekrar cehd edin”) yazısı ekrana çıxsın. 
 */

string password = "Salam123";
int limit = 3;
int count = 0;
while (true)
{
    string input = Console.ReadLine();
    if (input == password)
    {
        Console.WriteLine("Ugurla daxil olundu");
        break;
    }
    else
    {
        count++;
        if (count == limit)
        {
            Console.WriteLine("Blok olundu");
            break;
        }
        Console.WriteLine("Tekrar cehd edin");
    }
}

/*
 Bir ədəd təyin olunsun. İstifadəçi həmin ədədi tapana qədər input versin. Əgər daxil edilən ədəd axtarılandan kiçikdirsə (“Daha böyük”), böyükdürsə, (“Daha kiçik”) çıxışı verilsin tapildiqda ise (“ugurla tapildi”) çıxışa verilsin.

 */
int number = new Random().Next(1, 100);
while (true)
{
    int n = Convert.ToInt32(Console.ReadLine());

    if (n > number)
    {
        Console.WriteLine("Daha kicikdir");
    }
    else if (n == number)
    {
        Console.WriteLine("Ugurla tapildi");
        break;
    }
    else
    {
        Console.WriteLine("Daha boyukdur");
    }
}


/*
 Massivde olan elementleri ozu ve ozunden sonrakinin ededi ortasi olacaq sekilde deyisdirmek. {4,8,3,5,9} -> {6,5,4,7}
 */
int[] arr1 = { 4, 8, 3, 5, 9 };
int[] arr2 = new int[arr1.Length - 1];
for (int i = 0; i < arr1.Length - 1; i++)
{
    int avg = (arr1[i] + arr1[i + 1]) / 2;
    arr2[i] = avg;
}
foreach (int number in arr2)
{
    Console.WriteLine(number);
}

/*
 n-ci fibonacci ededini cixisa veren proqram
 */
int n = Convert.ToInt32(Console.ReadLine());
int[] arr = new int[n]; // 0 1 1 2 3 5 8 13
arr[1] = 1;
for (int i = 2; i < n; i++)
{
    arr[i] = arr[i - 1] + arr[i - 2];
}
Console.WriteLine(arr[n - 1]);


/*
 Verilmis ededin sade ve ya murekkeb olmasini tapan proqram
 */
int n = Convert.ToInt32(Console.ReadLine());
int last = Convert.ToInt32(Math.Sqrt(n));
for (int i = 2; i <= last; i++)
{
    if (n % i == 0)
    {
        Console.WriteLine("Murekkebdir");
        return;
    }
}
Console.WriteLine("Sadedir");