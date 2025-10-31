# Lab 9 - 31 oktyabr
## Bilet sistemi
1. Yaradılmalı olan class-lar:
    - `Event`:
        - Id
        - Ad
        - Tarix
        - Bilet qiyməti
    - `Ticket`
        - Id
        - Qiyməti
    Hər 2 class-da `ToString` metodu override edilməlidir.
    Hər tədbirin özünə aid biletləri olacaq. Aradakı əlaqəni qurmaq lazımdır
2. Ef Core paketləri yüklənməli, DbContext class-ı qurulmalı, connectionString verilməli və migration yaradılmalıdır
3. `Event` üçün asinxron CRUD əməliyyatlarını özündə saxlayan `IEventService` yaradılmalıdır
4. `Program.cs`-de Event-lə bağlı bütün metodları yoxlanılmaılıdr
5. `Ticket` üçün yaradılacaq `ITicketService`:
    - Tədbir üçün biletin alınması. Tədbirin id-si göndəriləcək və həmin tədbir üçün bir bilet yaradılacaq Əgər tədbirin vaxtı artıq keçibsə `EventExpiredException` adlı custom exception göndərilsin
    - Tədbir üçün alınmış olan biletin istifadə edilməsi üçün bir metod yaradılacaq. Bu metod-a biletin id-si göndəriləcək. biletin istifadə olunub olunmamasını bilmək üçün `Ticket` class-na əlavə property artırmaq lazımdır. Əgər bilet 1 dəfə istifadə olunubsa daha sonra təkrar istifadə edilmək istənildikdə `TicketAlreadyUsedException` göndərilsin
6. `Program.cs` Ticket servisin metodlarini exception-lari handle edəcək şəkildə istifadə etmək.
7. Filter metodları:
    - `IEventService`:
        - Tədbirləri ən çox bilet satılandan ən satılana doğru satılmış bilet sayı ilə birlikdə göstərən metod
        - Tədbirləri, biletlərdən ən çox qazanc əldən edəndən ən az əldə edənə doğru sıralamaq
        - Yalnız gələcəkdə baş tutacaq olan tədbirləri göstərən metod
        - Artıq baş tutmuş tədbirlərdən, Hər bir tədbir üçün neçə biletin alınıb, neçəsinin istifadə edildiyini göstərən metod
        (Məs: Event1 - 5/8)
8. Metodların təkmilləşmiş forması:
    - `IEventService`:
        - Tədbir yaradarkən property dəyərlərini istifadəçidən input olaraq alıb, yeni tədbir yaradılsın. (Tarix inputunu araşdırın)
    - `ITicketService`:
        - Tədbirə bilet alınan zaman tədbir id-si bir başa göndərilmək əvəzinə, istifadəçiyə gələcəkdə olan tədbirlərin siyahısı çıxsın (İd-si, adı, qiyməti). İstifadəçi bilet almaq istədiyi tədbirin Id-sini daxil etdikdə əgər həmin İd-li tədbir varsa, o tədbir üçün bilet alınmış olsun, əgər yoxdursa, təkrar input tələb etsin.