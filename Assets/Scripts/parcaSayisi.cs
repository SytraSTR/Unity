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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ParcaSayisiAl(int parcaSayi)
    {
        parcaSayisi = parcaSayi;
        Debug.Log("Parça Sayýsý Güncellendi: " + parcaSayisi);
    }
}
