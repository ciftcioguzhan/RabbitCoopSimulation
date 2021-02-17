# Tavşan Similasyonu
- Başlangıçta kümeste birer yetişkin  erkek ve dişi vardır.
- Konfigürasyon dosyasından similasyonun kaç dönem çalışacağı bilgisi alınır.
- Kümes içerisindeki hayvanlar için üç tip olay(event) vardır , Bunlar ; çiftleşmek, üremek ve ölmektir.
- İlk olarak çiftleşme daha sonra üreme ve son olarak ölme olayı gerçekleşiyor. her döngü sonucunda zaman bir gün ilerletilmektedir.
- İleride kümese farklı hayvanların da alınabilmesi için kümes jenerik olarak tasarlandı.

## *Çiftleşme*
Çiftleşek olan hayvanlar bir olay(event) fırlatarak haberleşir.
Dişiler için Çiftleşme; Kümes hayvanı ergenliğe erişmişse ve gebe değilse çiftleşebilir.
Erkekler için Çiftleşme; Kümes hayvanı ergenliğe ulaşmışsa çiftleşebilir.
Ergenliği erişim süresi 400 Gün  olarak  olarak belirlenmiştir.
Kümes hayvanının çiftleşme olayına katılıp katılmayacağı **Çiftleşme Katılım Oranı(0.25)** üzerinden belirlenmektedir.

## *Gebe Kalma*
Kümes hayvanının gebe kalıp kalmayacağı  **Gebe Kalma Oranı(0.15)**  üzerinden belirlenmektedir.
Gebelik sürece 40 gündür, bu süreç içerisinde çiftleşmeye giremez.
Eğer gebe ise tekrar çiftleşmeye giremez.
## *Doğum*
Doğum yapacak olan hayvanlar bir olay(event) fırlatarak haberleşir.
Gebelik süreci tamamlandıktan sonra doğum işlemi gerçekleşir.
Kümes hayvanı doğum yaptığında çocukların kız olma olasılığı  **Dişi Doğum Oranı(0.50)**  üzerinden belirlenmektedir.

```csharp
  public static Dictionary<int, decimal> NumberOfChildren = new Dictionary<int, decimal>
        {
            { 4, 0.35M },
            { 5, 0.24M },
            { 6, 0.20M },
            { 7, 0.06M },
            { 8, 0.05M },
            { 9, 0.04M },
            { 10, 0.03M },
            { 11, 0.02M },
            { 12, 0.01M },
        }; !
```
4 ile 12 yaş arası farklı dağılımlara bağlı olarak çocuk sayısı değişir.Olasılıklar şu şekildedir ; <br>

## *Ölüm*
Ölümü gerçekleşecek olan hayvanlar bir olay(event) fırlatarak haberleşir.
Tavşan dokuz yaşını geçtiğinde ölüm gerçekleşmiş oluyor.
Tavşan dokuz yaşının altında ise  farklı dağılımlara bağlı olarak ölüm işlemi gerçekleşir. Olasılıklar şu şekildedir ; <br>

```csharp
public static Dictionary<int, decimal> DeathRate = new Dictionary<int, decimal>
        {
            { 0, 0.03M / 365},
            { 1, 0.02M / 365},
            { 2, 0.03M / 365},
            { 3, 0.05M / 365},
            { 4, 0.10M / 365},
            { 5, 0.12M / 365},
            { 6, 0.13M / 365},
            { 7, 0.16M / 365},
            { 8, 0.20M / 365},
            { 9, 0.26M / 365},
        }; 
```


