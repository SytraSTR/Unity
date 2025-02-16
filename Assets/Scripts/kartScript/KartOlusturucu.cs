using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement; // Added for scene management
using DG.Tweening;
using System.IO; // Make sure to include the DoTween namespace

public class KartOlusturucu : MonoBehaviour
{
    [SerializeField] private Transform izgaraNesne;
    [SerializeField] private GameObject hazirKart;
    [SerializeField] private Sprite[] onYuz;
    [SerializeField] private Sprite arkaPlan;

    public Sprite[] OnYuzSprites => onYuz;
    public Sprite ArkaPlanSprite => arkaPlan;

    private Kart[] tumKartlar;
    private Kart ilkSecilen;
    private Kart ikinciSecilen;
    private bool kontrolEdiliyor = false;
    public bool kartlarKilitli = false;
    private int puan = 0;
    private int _parcaSayisi;
    private bool oyunBitti = false;
    private int aktifKartSayisi;
    private Saniye saniye; 
    private Skor skor;

    void OnEnable()
    {
        saniye = FindFirstObjectByType<Saniye>();
        skor = FindFirstObjectByType<Skor>();
        YeniOyunBaslat();
    }

    void Start()
    {
        aktifKartSayisi = _parcaSayisi * _parcaSayisi;
    }

    public void YeniOyunBaslat()
    {
        StopAllCoroutines();
        oyunBitti = false;
        StartCoroutine(GecikmeliBaslat());
    }

    private IEnumerator GecikmeliBaslat()
    {
        yield return new WaitForEndOfFrame();
        
        _parcaSayisi = PlayerPrefs.GetInt("SelectedGridSize", 4);

        foreach (Transform child in izgaraNesne)
        {
            Destroy(child.gameObject);
        }

        yield return new WaitForEndOfFrame();

        GridLayoutGroup izgara = izgaraNesne.GetComponent<GridLayoutGroup>();
        if (izgara != null)
        {
            izgara.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            izgara.constraintCount = _parcaSayisi;

            RectTransform panelRect = izgaraNesne.GetComponent<RectTransform>();
            float panelGenislik = panelRect.rect.width;
            float panelYukseklik = panelRect.rect.height;
            float hucreBoyutu = Mathf.Min(panelGenislik, panelYukseklik) / _parcaSayisi;

            izgara.cellSize = new Vector2(hucreBoyutu, hucreBoyutu);
            izgara.spacing = Vector2.zero;
            izgara.padding = new RectOffset(0, 0, 0, 0);
        }

        IzgaraOlustur();
        StartCoroutine(BaslangicGosterimi(tumKartlar));
    }

    void OnDisable()
    {
        StopAllCoroutines();
        ilkSecilen = null;
        ikinciSecilen = null;
        kontrolEdiliyor = false;
        kartlarKilitli = false;
        puan = 0;
        oyunBitti = false;
    }

    private void IzgaraOlustur()
    {
        int totalKartlar = _parcaSayisi * _parcaSayisi;
        tumKartlar = new Kart[totalKartlar];
        aktifKartSayisi = totalKartlar;

        List<int> kartIDleri = new List<int>();
        for (int i = 0; i < totalKartlar / 2; i++)
        {
            int spriteIndex = Random.Range(0, onYuz.Length); // Randomly select sprite index
            kartIDleri.Add(spriteIndex);
            kartIDleri.Add(spriteIndex);
        }

        for (int i = 0; i < kartIDleri.Count; i++)
        {
            int temp = kartIDleri[i];
            int randomIndex = Random.Range(i, kartIDleri.Count);
            kartIDleri[i] = kartIDleri[randomIndex];
            kartIDleri[randomIndex] = temp;
        }

        for (int i = 0; i < totalKartlar; i++)
        {
            GameObject yeniKart = Instantiate(hazirKart, izgaraNesne);
            Kart kartScript = yeniKart.GetComponent<Kart>();
            tumKartlar[i] = kartScript;
            kartScript.KartiKur(this, kartIDleri[i]);
        }

        StartCoroutine(BaslangicAnimasyonu());
    }

    private IEnumerator BaslangicAnimasyonu()
    {
        foreach (var kart in tumKartlar)
        {
            kart.KartiCevir(true, true);
        }

        yield return new WaitForSeconds(1f);

        foreach (var kart in tumKartlar)
        {
            kart.KartiCevir(false, true);
        }
    }

    public void OnYuz()
    {
        List<int> kartIDleri = new List<int>();
        int ciftSayisi = (_parcaSayisi * _parcaSayisi) / 2;
        
        for (int i = 0; i < ciftSayisi; i++)
        {
            int spriteIndex = Random.Range(0, onYuz.Length); // Randomly select sprite index
            kartIDleri.Add(spriteIndex);
            kartIDleri.Add(spriteIndex);
        }

        for (int i = kartIDleri.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = kartIDleri[i];
            kartIDleri[i] = kartIDleri[randomIndex];
            kartIDleri[randomIndex] = temp;
        }

        for (int i = 0; i < tumKartlar.Length; i++)
        {
            if (tumKartlar[i] != null)
            {
                tumKartlar[i].kartID = kartIDleri[i];
                tumKartlar[i].kartOlusturucu = this;
            }
        }

        StartCoroutine(BaslangicGosterimi(tumKartlar));
    }

    private IEnumerator BaslangicGosterimi(Kart[] kartlar)
    {
        if (kartlar == null || kartlar.Length == 0)
        {
            yield break;
        }

        foreach (var kart in kartlar)
        {
            if (kart != null)
            {
                kart.KartiCevir(true, true);
            }
        }

        yield return new WaitForSeconds(1f);

        foreach (var kart in kartlar)
        {
            if (kart != null)
            {
                kart.KartiCevir(false, true);
            }
        }
    }

    public void KartSecildi(Kart secilenKart)
    {
        if (oyunBitti || kontrolEdiliyor || kartlarKilitli) return;

        if (ilkSecilen == null)
        {
            ilkSecilen = secilenKart;
        }
        else if (ikinciSecilen == null && ilkSecilen != secilenKart)
        {
            ikinciSecilen = secilenKart;
            kontrolEdiliyor = true;
            kartlarKilitli = true;
            StartCoroutine(EslesmeKontrol());
        }
    }

    private IEnumerator EslesmeKontrol()
    {
        if (ilkSecilen == null || ikinciSecilen == null)
        {
            kontrolEdiliyor = false;
            kartlarKilitli = false;
            yield break;
        }

        yield return new WaitForSeconds(0.5f);

        bool eslesme = ilkSecilen.kartID == ikinciSecilen.kartID;

        if (eslesme)
        {
            puan += 10;
            skor.SkorEkle(puan); // Send score to Skor script
            
            // Add fade out animation using DoTween
            var kart1 = ilkSecilen.GetComponent<Image>();
            var kart2 = ikinciSecilen.GetComponent<Image>();

            // Check if kart1 and kart2 are not null before accessing them
            if (kart1 != null && kart2 != null)
            {
                kart1.DOFade(0, 1f).OnComplete(() => 
                {
                    if (ilkSecilen != null) // Check if ilkSecilen is still valid
                    {
                        ilkSecilen.KartiGizle();
                    }
                    else
                    {
                        Debug.Log("ilkSecilen null oldu.");
                    }
                });
                kart2.DOFade(0, 1f).OnComplete(() => 
                {
                    if (ikinciSecilen != null) // Check if ikinciSecilen is still valid
                    {
                        ikinciSecilen.KartiGizle();
                    }
                    else
                    {
                        Debug.Log("ikinciSecilen null oldu.");
                    }
                });
            }
            else
            {
                Debug.Log("Kartlar null: kart1 veya kart2.");
            }
            
            aktifKartSayisi -= 2;

            // Check if all cards are matched
            if (aktifKartSayisi == 0)
            {
                DOTween.KillAll();

                oyunBitti = true;
                saniye.OyunuBitir();
                SceneManager.LoadScene("OyunKazanma"); // Load the OyunKazanma scene
            }
        }
        else
        {
            if (ilkSecilen != null) ilkSecilen.KartiCevir(false, true);
            if (ikinciSecilen != null) ikinciSecilen.KartiCevir(false, true);
        }

        // Reset selections
        ilkSecilen = null;
        ikinciSecilen = null;
        kontrolEdiliyor = false;
        kartlarKilitli = false;
    }
}

