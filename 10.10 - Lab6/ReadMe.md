# Lab 6 - 10 oktyabr
## Kart Sistemi:
1. `Card` adında class yaradırıq:
    - Id
    - Balance
    - Bonus
    - CardNumber (mütləq 16 rəqəmdən ibarət olmalıdır (Bunun üçün Regex araşdırması etsən yaxşıdır))
    - Bank (enum) (dəyərlər 3-cü sualda var)
    - Kartdan pul çıxılması üçün bir `WithDraw` adında metod olmalıdır. Bu metod parametr olaraq çıxılacaq məbləği qəbul edəcək. Balans yoxlanıldıqdan sonra əgər problem yoxdursa balansdan pul çıxılacaq. Və əgər proeses uğurla tamamlanarsa, `true` əks halda `false` dəyəri qaytaracaq.
    - `ToString` metodunu override edin
2. `Bank` enum:
    - ABB, Kapital, Leo
3. `ICardService` interfeysi:
    - Bütün Kartları qaytaran metod 
    - Siyahıya kart əlavə etmək üçün metod
    - Card nömrəsi qəbul edib, ona əsasən Card-ı qaytaran Indexer
4. `CardService`
    Kartlar, proyektdə olan `Data` folderi içində, `cards.json` faylında saxlanmalıdır. 
    - GetAll metodu json faylındakı bütün kartları qaytarmalıdır.
    - Indexer istifadə edərək, göndərilən `CardNumber`-ə uyğun onun məlumatlarını qaytaran metod
        ```cs
        Card searchCard = cardService["1234567812345678"];
        Console.WriteLine(searchCard); // kartın məlumatları
        ```
    - Json faylına yeni kart əlavə edən metod. (Əgər eyni `CardNumber` əlavə olunmağa çalışılarsa `ConflictException` exception versin) (Əgər kart nömrəsi 16 rəqəmdən ibarət deyilsə `InvalidCardNumberException` xətası verin)
5. Extension metodlar
    - `MaskCardNumber()` bu metod kart-ın nömrəsini gizləyərək `1234 **** **** 5678` formatında qaytaracaq
    - `ExpenseAndGetBonus()` metodu, özlüyündə xərclənən məbləği qəbul edəcək. Kartın bu ödənişi edib, edə bilməyəcəyini yoxlandıqdan sonra kartın hansı bank-a aid olmasına görə onun `Bonus` property-sinin dəyərini artıracaq. Bonuslar belədir:
        - `Abb` kartı üçün 2%
        - `Leo` kartı üçün 4%
        - `Kapital` kartı üçün 5%
6. Transactions:
    1. `Transaction` adında class yaradılmalıdır:
        - Id
        - CardNumber
        - Amount
        - Date
        - `ToString` metodunun override olunması
    2. `ITransactionService`
        - `GetAll` metodu
        - Tranzaksiya artırma metodu
        - Kartın tranzaksiyalarını gətirən metod
        - Verilən tarix aralığındakı tranzaksiyaları göstərən metod
    3. `TransactionService`
        Data folderi içində `transactions.json` faylı olacaq ki, tranzaksiyalar orada saxlansın
        - Standart GetAll
        - Tranzaksiya artırma metodu, bir tranzkaisyan; həmin json faylına əlavə edəcək. Bu metod `WithDraw` və `ExpenseAndGetBonus` metodları zamanı işə düşəcək
        - Kartın nömrəsinə görə onun tranzaksiyalarını gətirsin
        - 2 fərqli tarix verilməlidi. həmin tarix aralığına uyğun gələn tranzaksiyanı göstərsin
    