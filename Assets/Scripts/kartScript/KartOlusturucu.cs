using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

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
    private int aktifKartSayisi;  // Sınıf değişkeni olarak tanımlıyoruz
    private KartKontrol kartKontrol;
    private OyunKontrol oyunKontrol;
    private SaniyeKontrol saniyeKontrol;  // Yeni referans ekle
    private Saniye saniye;

    void OnEnable()
    {
        kartKontrol = FindFirstObjectByType<KartKontrol>();
        oyunKontrol = FindFirstObjectByType<OyunKontrol>();
        saniyeKontrol = FindFirstObjectByType<SaniyeKontrol>();
        saniye = FindFirstObjectByType<Saniye>();

        YeniOyunBaslat();
    }

    void Start()
    {
        // ... diğer start kodları ...
        aktifKartSayisi = _parcaSayisi * _parcaSayisi;  // Başlangıçta toplam kart sayısını ayarlıyoruz
    }

    public void YeniOyunBaslat()
    {
        if (oyunKontrol == null || kartKontrol == null)
        {
            kartKontrol?.KartKontrolBulunamadi();
            return;
        }

        StopAllCoroutines();
        oyunBitti = false;
        oyunKontrol.YeniOyunBasladi();  // Zaten var olan metodu kullanıyoruz
        StartCoroutine(GecikmeliBaslat());
    }

    private IEnumerator GecikmeliBaslat()
    {
        yield return new WaitForEndOfFrame();
        
        _parcaSayisi = PlayerPrefs.GetInt("SelectedGridSize", 4);
        
        oyunKontrol.IzgaraBoyutuAyarlandi(_parcaSayisi);

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

        // Başlangıç gösterimini başlat
        StartCoroutine(BaslangicGosterimi(tumKartlar));
    }

    void OnDisable()
    {
        // Coroutine'leri ve değişkenleri temizle
        StopAllCoroutines();
        ilkSecilen = null;
        ikinciSecilen = null;
        kontrolEdiliyor = false;
        kartlarKilitli = false;
        puan = 0;
        oyunBitti = false;
        oyunKontrol.OyunDurduruldu();
    }

    private void IzgaraOlustur()
    {
        int totalKartlar = _parcaSayisi * _parcaSayisi;
        tumKartlar = new Kart[totalKartlar];
        aktifKartSayisi = totalKartlar;  // Burada ayarlıyoruz
        kartKontrol.KalanKartBilgisi(aktifKartSayisi);
        oyunKontrol.ToplamParcaSayisi(totalKartlar);

        List<int> kartIDleri = new List<int>();
        for (int i = 0; i < totalKartlar / 2; i++)
        {
            int spriteIndex = i % onYuz.Length;
            kartIDleri.Add(spriteIndex);
            kartIDleri.Add(spriteIndex);
        }

        // ID'leri karıştır
        for (int i = 0; i < kartIDleri.Count; i++)
        {
            int temp = kartIDleri[i];
            int randomIndex = Random.Range(i, kartIDleri.Count);
            kartIDleri[i] = kartIDleri[randomIndex];
            kartIDleri[randomIndex] = temp;
        }

        // Kartları oluştur
        for (int i = 0; i < totalKartlar; i++)
        {
            GameObject yeniKart = Instantiate(hazirKart, izgaraNesne);
            Kart kartScript = yeniKart.GetComponent<Kart>();
            tumKartlar[i] = kartScript;
            kartScript.KartiKur(this, kartIDleri[i]);
        }

        // Başlangıç animasyonunu başlat
        StartCoroutine(BaslangicAnimasyonu());
    }

    private IEnumerator BaslangicAnimasyonu()
    {
        // Tüm kartları ön yüze çevir
        foreach (var kart in tumKartlar)
        {
            kart.KartiCevir(true);
        }

        // 1 saniye bekle
        yield return new WaitForSeconds(1f);

        // Tüm kartları arka yüze çevir
        foreach (var kart in tumKartlar)
        {
            kart.KartiCevir(false);
        }
        
        oyunKontrol.OyunDevamEdiyor();
    }

    public void OnYuz()
    {
        if (onYuz == null || onYuz.Length == 0)
        {
            kartKontrol.ImageBulunamadi();
            return;
        }

        // Kart ID'lerini oluştur ve karıştır
        List<int> kartIDleri = new List<int>();
        int ciftSayisi = (_parcaSayisi * _parcaSayisi) / 2;
        
        for (int i = 0; i < ciftSayisi; i++)
        {
            if (i >= onYuz.Length)
            {
                kartKontrol.ImageOtomatikEklendi();
                i = 0;
            }
            kartIDleri.Add(i);
            kartIDleri.Add(i);
        }

        // Fisher-Yates karıştırma algoritması
        for (int i = kartIDleri.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = kartIDleri[i];
            kartIDleri[i] = kartIDleri[randomIndex];
            kartIDleri[randomIndex] = temp;
        }

        if (tumKartlar.Length != kartIDleri.Count)
        {
            kartKontrol.ImageBulunamadi();
            return;
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
            kartKontrol.ImageBulunamadi();
            yield break;
        }

        // Kartların ön yüzünü göster
        foreach (var kart in kartlar)
        {
            if (kart != null)
            {
                kart.KartiCevir(true);
            }
        }

        // 1 saniye bekle
        yield return new WaitForSeconds(1f);

        // Kartların arka yüzünü göster
        foreach (var kart in kartlar)
        {
            if (kart != null)
            {
                kart.KartiCevir(false);
            }
        }
    }

    public void KartSecildi(Kart secilenKart)
    {
        // Eğer oyun bittiyse, kontrol devam ediyorsa veya kartlar kilitliyse işlem yapma
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
            kartKontrol.KartEslesmesi(ilkSecilen.kartID, ikinciSecilen.kartID, true, puan);
            
            ilkSecilen.KartiGizle();
            ikinciSecilen.KartiGizle();
            
            aktifKartSayisi -= 2;
            kartKontrol.KalanKartBilgisi(aktifKartSayisi);

            if (aktifKartSayisi <= 0)
            {
                if (oyunKontrol != null)
                {
                    oyunKontrol.OyunBitti();
                    if (saniye != null)
                    {
                        saniye.OyunuBitir();
                    }
                    else
                    {
                        saniyeKontrol.SaniyeInstanceBulunamadi();
                    }
                }
            }
        }
        else
        {
            kartKontrol.KartEslesmesi(ilkSecilen.kartID, ikinciSecilen.kartID, false, puan);
            ilkSecilen.KartiCevir(false);
            ikinciSecilen.KartiCevir(false);
        }

        ilkSecilen = null;
        ikinciSecilen = null;
        kontrolEdiliyor = false;
        kartlarKilitli = false;
    }
}
