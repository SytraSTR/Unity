using UnityEngine;

public class ParcaSayisi : MonoBehaviour
{
    public static ParcaSayisi Instance { get; private set; }
    public int parcaSayisi;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Eğer daha önce seçim yapıldıysa, kayıtlı değeri yükle
        if (PlayerPrefs.HasKey("SelectedGridSize"))
        {
            parcaSayisi = PlayerPrefs.GetInt("SelectedGridSize");
        }
    }

    public void ParcaSayisiAl(int parcaSayi)
    {
        parcaSayisi = parcaSayi;
        PlayerPrefs.SetInt("SelectedGridSize", parcaSayisi); // Seçimi kaydet
        PlayerPrefs.Save();
        Debug.Log("Parça Sayısı Güncellendi: " + parcaSayisi);
    }
}
