# Lab 8 - 24 oktyabr
## SQL Hospital Management System
1. Aşağıdakı cədvəllər yaradılacaq:
    - Patients 
        - FirstName - maksimum 20 simvol, 
        - LastName - maksimum 25 simvol, 
        - BirthDate - gələcək zaman ola bilməz,
        - Email - təkrarlanmamalıdır
    - Departments 
        - Name - maksimum 40 simvol, təkrarlanmamalıdır,
    - Doctors 
        - FirstName - maksimum 20 simvol, 
        - LastName - maksimum 25 simvol, 
        - Experience (il) - mənfi ola bilməz 
2. Qurulmalı olan relationlar:
    - Hər bir həkim hansısa departamentdə çalışır
    - Bir xəstə bir çox həkimlə görüşmüş ola bilər. Görüşün tarixi və Görüş nəticəsi qeyd olunmalıdır.
3. Hər cədvəl üçün bir neçə insert sorğusu
4. Join sorğuları:
    - Həkimlərin şöbələri ilə birlikdə siyahısı
    - Xəstələrin tibbi qeydləri və müalicə edən həkimlər
5. View-lar:
    
6. Store Procedures:
    - Pasient-in Id-si göndəriləcək və ona hansı tarixdə hansı həkim tərəfindən nə diaqnoz qoyulduğu göstəriləcək
    - Həkim-in Id-si göndəriləcək və bu gün kimlərlə görüşməli olduği siyahı olaraq çıxacaq
7. Əlavə cədvəllər:
    - Dərman:
        - Adı
8. Əlavə Relationlar:
    Hər bir görüşün nəticəsi olaraq pasiyentə bir neçə dərman yazıla bilər.
9. Join sorğuları:
    - Hər bir pasiyentə ona yazılmış dərmanların siyahısı
    - Həkimlərin hansı xəstələrə hansı dərmanları yazdığnı göstər


Northwind Db üzrə tapşırıqlar:
- Çalışanların yapmış olduğu sipariş adedi (orders)  
- Employees tablosundan; FirstName alanına göre, isimleri ASC olarak sıraladıktan sonra, sonuç kümesinin ilk beş kaydını ülkelerine göre gruplayınız.
- Ürün bedeli 35$'dan az olan ürünlerin kategorilerine göre gruplanması
- Baş harfi A-K aralığında olan ve stok miktarı 5 ile 50 arasında olan ürünleri kategorilerine göre gruplayınız.
- Her bir siparişteki toplam ürün sayısını bulunuz.
- Her bir siparişin tutarına göre listelenmesi
- Toplam tutarı 2500 ile 3500 arasında olan siparişlerin gruplanması
- Her bir siparişteki toplam ürün sayısı 200'den az olanlar