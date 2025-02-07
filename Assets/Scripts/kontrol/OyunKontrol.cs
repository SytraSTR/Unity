using UnityEngine;

public class OyunKontrol : MonoBehaviour
{
    public void YeniOyunBasladi()
    {
        Debug.Log("Yeni oyun başlatıldı.");
    }

    public void OyunBitti()
    {
        Debug.Log("Oyun başarıyla tamamlandı!");
    }

    public void OyunDurduruldu()
    {
        Debug.Log("Oyun durduruldu.");
    }

    public void OyunDevamEdiyor()
    {
        Debug.Log("Oyun devam ediyor...");
    }

    public void OyunYukleniyor()
    {
        Debug.Log("Oyun yükleniyor...");
    }

    public void IzgaraBoyutuAyarlandi(int parcaSayisi)
    {
        Debug.Log($"Izgara boyutu {parcaSayisi}x{parcaSayisi} olarak ayarlandı.");
    }

    public void ToplamParcaSayisi(int toplamParca)
    {
        Debug.Log($"Toplam {toplamParca} adet parça oluşturuldu.");
    }
}
