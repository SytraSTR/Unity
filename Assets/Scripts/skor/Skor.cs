using UnityEngine;
using TMPro;

public class Skor : MonoBehaviour
{
    private TextMeshProUGUI skorText;
    private int skor = 0;

    void Start()
    {
        skorText = GetComponent<TextMeshProUGUI>();
        SkoruGuncelle();
    }

    public void SkorEkle(int puan)
    {
        skor = puan; // Değişiklik: puanı ekle
        SkoruGuncelle();
    }

    private void SkoruGuncelle()
    {
        skorText.text = $"Skor: {skor}";
    }
}
