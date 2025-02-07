using UnityEngine;

public class SkorKontrol : MonoBehaviour
{
    public void SkorEklendi(int puan)
    {
        Debug.Log($"Skor güncellendi! Eklenen puan: {puan}");
    }

    public void SkorGuncellendi(int yeniSkor) 
    {
        Debug.Log($"Yeni skor: {yeniSkor}");
    }

    public void EslesmedenPuanKazanildi(int puan)
    {
        Debug.Log($"Eşleşme başarılı! Kazanılan puan: {puan}");
    }
}
