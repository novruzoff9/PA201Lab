# Lab 5 - 3 oktyabr
## Restoran Simulyaiyası:
1. `Food` adında `abstract` class yaratmalı:
2. Propertylər:
    - Id
    - Məhsulun adı
    - Məhsulun yaranma vaxtı (gelecek tarix ola bilməz)
    - Hazırlanma müddəti
    - Kalori dəyəri (kəsr ədəd ola bilər, mənfi ola bilməz)
3. Metodlar:
    - Qiyməti hesablamaq üçün `abstrakt` metod (class-lar üzrə istifadəsi aşağıda verilib)
        ```cs
        //İstifadə nümunəsi
        Console.WriteLine(food.CalcPrice()); // 5.36
        ```
4. `Food` clasından miras alan 2 class yaratmalı `Pizza` və `Fries`
    - `Pizza` propertyləri:
        - Radius 
        - Acılı olub olmaması
    - `Pizza` metodları:
        - Qiymətin hesablanması:
        Sahesine görə 10 qəpikdən, əgər acılıdırsa əlavə 1.15 azn
    - `Fries` propertyləri:
        - Duzlu olub olmaması
        - Çəki
    - `Fries` metodları:
        - Çəkisinə görə 1 manatdan, əgər duzludursa əlavə 0.8 azn
5. `Pizza` və ya `Fries` instance-sini bir başa Console-a yazdıra bilmək:
    ```cs
    Pizza pizza1 = new Pizza();
    Console.WriteLine(pizza1); 
    //Name: Margherita, Price: 8.99 Azn, Radius: 12 ...

    Fries fri1 = new Fries();
    Console.WriteLine(fri1); 
    //Name: French Fries, Price: 2.99 Azn, Weight: 150g
    ```
6. `Food` obyektlərini idarə etmək üçün servislər qurulacaq
    - `IFoodService` adında interfeys yaradırıq aşağıdakı metodlar olmalıdır:
        - Qidanın nə vaxt hazır olacağını bildirən metod:
        Bir qida obyekti qəbul edəcək və onun hazır olacağı vaxtı geri qaytacaq
        ```cs
        Console.WriteLine(foodService.WhenWillPrepared(pizza));
        // Çıxış nüumnəsi: 16/05/2025 18:45:20
        ```
        - Qidalar siyahısında `kalori` dəyəri n-dən böyük olan qidaların sayını qaytaran metod 
    - daha sonra bu interfeysi implement edən `FoodService` class-ında metodların içini doldururuq.

7. `Pizza` obyektlərini idarə etmək üçün servislər qurulacaq
    - `IPizzaService` adında interfeys yaradırıq aşağıdakı metodlar olmalıdır:
        - Yeni bir pizza yaradılması üçün metod dəyərləri həmin metoda göndəririk
        - Pizza siyahısında radiusu ən böyük olan pizzanı qaytaran metod
    - daha sonra bu interfeysi implement edən `PizzaService` class-ında metodların içini doldururuq.

Pizza siyahısı:
```cs
Pizza[] pizzas = new Pizza[5]
{
    new Pizza { Name = "Margherita", Radius = 12, Calories = 250, IsHot = false, PrepareTime = TimeSpan.FromMinutes(15), Created = DateTime.Now },
    new Pizza { Name = "Pepperoni", Radius = 14, Calories = 320, IsHot = true, PrepareTime = TimeSpan.FromMinutes(18), Created = DateTime.Now },
    new Pizza { Name = "BBQ Chicken", Radius = 16, Calories = 400, IsHot = true, PrepareTime = TimeSpan.FromMinutes(20), Created = DateTime.Now },
    new Pizza { Name = "Veggie", Radius = 10, Calories = 220, IsHot = false, PrepareTime = TimeSpan.FromMinutes(12), Created = DateTime.Now },
    new Pizza { Name = "Hawaiian", Radius = 13, Calories = 280, IsHot = false, PrepareTime = TimeSpan.FromMinutes(17), Created = DateTime.Now }
};

```