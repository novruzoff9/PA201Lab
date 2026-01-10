## 🎯 MARAQLI & CƏLBEDİCİ ƏLAVƏLƏR (Console level)

---

## 🔥 1. “Şanslı Bilet” (Random Feature)

Bilet alınanda:

* **10% ehtimalla** bilet **pulsuz** olsun
* Console-da:

```txt
🎉 TƏBRİKLƏR! Şanslı bilet qazandınız!
```

👉 Texniki olaraq:

* `Random`
* Ticket qiyməti `0`
* Event gəliri hesablananda nəzərə alınsın

---

## ⏳ 2. “Son 1 saat” Panic Mode

Əgər tədbirin başlamasına **1 saatdan az qalırsa**:

* Bilet qiyməti avtomatik **20% ucuzlaşsın**
* Console xəbərdarlıq:

```txt
⚠ Son 1 saat! Endirim tətbiq olundu
```

👉 İstifadəçi bilet almaq istədikdə, tədbirlər ekranında qiyməti endirimli halda görsün

---

## 🏆 3. TOP 3 Event Scoreboard

Console-da:

```txt
🏆 TOP 3 EVENTS
1. Konsert A – 15 bilet
2. Film Gecəsi – 12 bilet
3. Standup – 9 bilet
```

---

## 🎟️ 4. “Combo” alışı

İstifadəçi:

* eyni tədbir üçün **birdən çox bilet** ala bilər

Əgər:

* 3 və daha çox bilet alırsa → **10% endirim**

sifarişi təsdiq edəndə console-da:

```txt
🎫 Combo alışı üçün 10% endirim qazandınız!
1 bilet x 50 AZN = 150 AZN
Endirim: -15 AZN
Cəmi: 135 AZN
```

---
## 🎁 5. Promo Kodlar
Console-da:

```txt
Promo kodunuz varsa, daxil edin (yoxdursa, keçin):
```
* İstifadəçi promo kod daxil edə bilər

Promo kodlar üçün database-də ayrıca cədvəl yaradılır:
- `PromoCode`:
    - Id
    - Kod (məs: "PROMO10")
    - Endirim faizi (məs: 10 üçün 10%)
    - İstifadə edilib/edilməyib (bool)

Bilet alınarkən istifadəçiyə promo kod daxil etmək imkanı verilir.

```txt
Promo kodunuz varsa, daxil edin (yoxdursa, keçin):
```

Əgər istifadəçi düzgün promo kod daxil edərsə:
* ümumi məbləğdən müvafiq endirim çıxılır
* console-da:

```txt
🎁 Promo kod uğurla tətbiq olundu! Siz {endirim_faizi}% endirim qazandınız.
```
Əgər promo kod yalnışdırsa:
* console-da:
```txt
❌ Promo kod yalnışdır. Endirim tətbiq olunmadı.
```

---

## 🧠 6. “Bilet ovçusu” challenge

Tapşırıq:

> Elə LINQ yaz ki,
> **istifadə olunmuş biletlərin sayı**,
> **istifadə olunmamışlardan çox olan event-ləri tap**

---

## 🎬 10. Gündəlik satış


Console-da İstifadəçidən gün input olaraq alınır (məs: 2023-10-31)
və həmin gün üçün satış statistikası göstərilsin:

```txt
Bu gün 23 bilet satıldı
3 tədbir uğurla baş tutdu
```

👉 Statistik summary

---
