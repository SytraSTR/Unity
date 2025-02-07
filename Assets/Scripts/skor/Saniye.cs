using UnityEngine;
using TMPro;

public class Saniye : MonoBehaviour
{
    public static Saniye Instance { get; private set; }
    private TextMeshProUGUI zamanText;
    private float gecenSure = 0f;
    private bool oyunTamamlandi = false;
    private SaniyeKontrol saniyeKontrol;

    void Awake()
    {
        Instance = this;
        saniyeKontrol = FindFirstObjectByType<SaniyeKontrol>();
    }

    void OnEnable()
    {
        // Oyun başladığında sıfırla
        oyunTamamlandi = false;
        gecenSure = 0f;
        if (zamanText != null)
        {
            zamanText.text = "0";
        }
        saniyeKontrol.SaniyeSayacıBaslatildi();
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
            if (zamanText != null)
            {
                zamanText.text = $"Saniye: {Mathf.Floor(gecenSure)}";
            }

        }
    }

    public void OyunuBitir()
    {
        saniyeKontrol.OyunBitisMetoduCagrildi();
        oyunTamamlandi = true;
        saniyeKontrol.OyunTamamlanmaSuresi(gecenSure);
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
