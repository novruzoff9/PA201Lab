public class Program
{
    public static void Main(string[] args)
    {
        /*
         Metod-a əvvəlcə operatoru yazacam. Daha sonra istədiyim qədər elementləri. Elementlər arasında həmin operatoru işlədən proqram
         */

        int result = Calculate('$', 3, 5, 7, 2, 6);
        Console.WriteLine(result);

        /*
         Loglama metodu yaradilacaq. Metoda mətn və log səviyyəsi (Info, Warn, Error)göndəriləcək. Çıxışda əvvəlcə səviyyə, sonra isə mətn görsənəcək. 
                İstifadəçi log səviyyəsini göndərməsə Info olacaq.
                Tarixi əlavə etmək
         */

        Logging("Istifadeci tapilmadi"); // 9.19.20205 - Info : Istifadeci tapilmadi

        /*
         Bank əməliyyatını simulyasiya edən metod qurulacaq:
         ProcessPayment() metoduna “ref” ilə balans və silinməli olunan məbləğ gönderiləcək. əgər balans kifayət edərsə bir başa özündən dəyər çıxılıb özündə saxlanılacaq. əks halda halda isə “Yetərli balans yoxdur” çıxışa veriləcək.
         */

        int balance = 1000;
        int amount = 500;
        ProcessPayment(ref balance, amount); // "Balans kifayet deyil"

        Console.WriteLine(balance); // 500

        /*
         Verilən e-poçt adresinin doğruluğunu yoxlayan metod:
            Boş dəyər olması halında istifadəçiyə məlumat verməli
            içərisində @ simvolu olmalı
            @ simvolundan sonra . simvolu olmalı
         Əgər bütün bunlar ödənərsə, istifadəçiyə true əks halda false qaytarsın.
         */

        string email = "test.user@codeedu.az";
        CheckMail(email);

        /*
         email dəyişənindən @ işarəsindən sağ tərəfdə olan hissəni return etsin. Dəyişən özündə sadəcə sol tərəfi saxlasın
         */

        string email = "test.user@code.edu.az";
        string domain = FixEmail(ref email);
        Console.WriteLine(email);  //test.user
        Console.WriteLine(domain); //code.edu.az

        /*
         Metoda adlardan ibarət siyahını göndərim. Daha sonra silmək istədiyim adları bir bir qeyd edim. Mənə təzə siyahını qaytarsin.
         */
        string[] names = { "Test", "User", "Admin", "Root", "Manager" };
        string[] newNames = RemoveNames(names, "User", "Manager");
        Console.WriteLine(string.Join(", ", newNames)); // User, Admin

        /*
         Özündə adları saxlayan zədələnmiş string var. Ona aşağıdakı düzəlişləri edən metodu yazmaq lazımdır
         */
        string wreckedNames = "?yX?N;F?tEH;yuSif;k?MR?n";
        Console.WriteLine(RepairNames(wreckedNames));
        //Ayxan, Fateh, Yusif, Kamran
    }

    public static string RepairNames(string value)
    {
        value = value.Replace('?', 'a');
        string[] names = value.Split(';');
        for(int i = 0; i< names.Length; i++)
        {
            names[i] = names[i][0].ToString().ToUpper() + names[i].Substring(1).ToLower();
        }
        string result = string.Join(", ", names);
        return result;
    }

    public static string[] RemoveNames(string[] names, params string[] removeNames)
    {
        string[] newNames = new string[names.Length - removeNames.Length];
        int index = 0;
        foreach (string item in names)
        {
            if (!removeNames.Contains(item))
            {
                newNames[index] = item;
                index++;
            }
        }

        // Alternative using LINQ
        string[] newNames = names.Where(n => !removeNames.Contains(n)).ToArray();
        return newNames;
    }

    public static string FixEmail(ref string email)
    {
        int atIndex = email.IndexOf('@');
        string domain = email.Substring(atIndex + 1);

        email = email.Remove(atIndex);
        return domain;
    }


    public static void CheckMail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            Console.WriteLine("Email bos ola bilmez");
            return;
        }
        if (email.IndexOf('@') == -1)
        {
            Console.WriteLine("@ isaresi yoxdur");
            return;
        }
        int atIndex = email.IndexOf('@');
        int dotIndex = email.LastIndexOf('.');
        if(dotIndex < atIndex)
        {
            Console.WriteLine("@ isaresinden sonra noqte olmalidir");
            return;
        }
        Console.WriteLine("Email ugurludur");
    }

    public static void ProcessPayment(ref int balance, int amount)
    {
        if (balance >= amount) balance -= amount;
        else Console.WriteLine("Kifayet qeder balans yoxdur");
    }

    public static void Logging(string content, string level = "Info")
    {
        string date = DateTime.Now.ToString("dd MMM yyyy");
        Console.WriteLine($"{date} - {level} : {content}");
    }

    public static int Calculate(char op, params int[] numbers)
    {
        if(op == '+')
        {
            int result = 0;
            foreach (int item in numbers)
            {
                result += item;
            }
            return result;
        }
        else if(op == '*')
        {
            int result = 1;
            foreach (int item in numbers)
            {
                result *= item;
            }
            return result;
        }
        else
        {
            Console.WriteLine("Operator yanlisdir");
            return 0;
        }
    }
}