using UnityEngine;

public class SaniyeKontrol : MonoBehaviour
{
    public void SaniyeSayacıBaslatildi()
    {
        Debug.Log("Saniye sayacı başlatıldı.");
    }

    public void TextMeshProBulunamadi()
    {
        Debug.LogError("TextMeshProUGUI component bulunamadı!");
    }

    public void OyunBitisMetoduCagrildi()
    {
        Debug.Log("Oyun bitiş metodu çağrıldı.");
    }

    public void OyunTamamlanmaSuresi(float sure)
    {
        Debug.Log($"Oyun {sure} saniyede tamamlandı.");
    }

    public void SaniyeInstanceBulunamadi()
    {
        Debug.LogError("Saniye instance'ı bulunamadı!");
    }
}
