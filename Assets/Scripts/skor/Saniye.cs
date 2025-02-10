using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Added for scene management

public class Saniye : MonoBehaviour
{
    public static Saniye Instance { get; private set; }
    private TextMeshProUGUI zamanText;
    private float gecenSure = 0f;
    private bool oyunTamamlandi = false;
    private SaniyeKontrol saniyeKontrol; // Inspector'da atanacak


    void Awake()
    {
        
        if (saniyeKontrol == null)
        {
            
            if (saniyeKontrol == null)
            {
                Debug.LogError("SaniyeKontrol instance bulunamadı! Lütfen sahnede SaniyeKontrol GameObject'inin mevcut olduğundan emin olun.");
            }
        }
    }

    void OnEnable()
    {
        
        oyunTamamlandi = false;
        gecenSure = 0f;
        zamanText.text = "0"; 
        saniyeKontrol.SaniyeSayaciBaslatildi();
    }

    void Start()
    {
        zamanText = GetComponent<TextMeshProUGUI>();
        if (zamanText == null)
        {
            saniyeKontrol.TextMeshProBulunamadi();
        }
    }

    void Update()
    {
        if (!oyunTamamlandi)
        {
            gecenSure += Time.deltaTime;
            zamanText.text = $"Saniye: {Mathf.Floor(gecenSure)}"; // Removed null check for simplicity
        }
    }

    public void OyunuBitir()
    {
        saniyeKontrol.OyunBitisMetoduCagrildi();
        oyunTamamlandi = true;
        zamanText.text = $"Oyun Bitti! Süre: {Mathf.Floor(gecenSure)}"; // Show final time on game over
        saniyeKontrol.OyunTamamlanmaSuresi(gecenSure);
        
        // Check if the current scene is OyunKazanma
        if (SceneManager.GetActiveScene().name == "OyunKazanma")
        {
            zamanText.text = $"Tamamlanan Süre: {Mathf.Floor(gecenSure)}"; // Display elapsed time if in OyunKazanma scene
        }

        PlayerPrefs.SetFloat("KazanilanSure", gecenSure); // Store the elapsed time
    }

    public void OyunuDurdur()
    {
        if (Instance != null)
        {
            Instance.OyunuBitir();
        }
        else
        {
            saniyeKontrol.SaniyeInstanceBulunamadi();
        }
    }
}


