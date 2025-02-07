using UnityEngine;
using UnityEngine.EventSystems;

public class KartKontrol : MonoBehaviour
{
    private Skor skor;
    private SkorKontrol skorKontrol;

    void Start()
    {
        skor = FindFirstObjectByType<Skor>();
        skorKontrol = FindFirstObjectByType<SkorKontrol>();
    }

    public void KartCevrildi(int kartID, bool onYuze)
    {
        Debug.Log($"Kart {kartID} {(onYuze ? "ön" : "arka")} yüze çevrildi");
    }

    public void KartEslesmesi(int kartID1, int kartID2, bool eslestimi, int puan)
    {
        if (eslestimi)
        {
            Debug.Log($"Eşleşme başarılı! Puan: {puan}");
            if (skor != null)
            {
                skor.SkorEkle(puan);
                if (skorKontrol != null)
                {
                    skorKontrol.EslesmedenPuanKazanildi(puan);
                    skorKontrol.SkorEklendi(puan);
                    skorKontrol.SkorGuncellendi(puan);
                }
            }
        }
        else
        {
            Debug.Log("Kartlar eşleşmedi!");
        }
    }

    public void KartaDokunuldu(int kartID)
    {
        Debug.Log($"Kart ID: {kartID} - Karta dokunuldu!");
    }

    public void KalanKartBilgisi(int aktifKartSayisi)
    {
        Debug.Log($"Kalan aktif kart sayısı: {aktifKartSayisi}");
    }

    public void ImageBulunamadi()
    {
        Debug.LogError("Image component bulunamadı!");
    }

    public void ImageOtomatikEklendi()
    {
        Debug.LogWarning($"GameObject üzerinde Image bileşeni bulunamadı. Otomatik olarak eklendi.");
    }

    public void KartKontrolBulunamadi()
    {
        Debug.LogWarning("Sahnede KartKontrol script'i bulunamadı!");
    }
}
