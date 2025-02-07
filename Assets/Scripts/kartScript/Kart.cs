using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Kart : MonoBehaviour, IPointerClickHandler
{
    public int kartID;
    public KartOlusturucu kartOlusturucu;
    public KartKontrol kartKontrol;
    private Image kartImage;
    private bool onYuzeDogruMu = false;

    public bool OnYuzeDogruMu => onYuzeDogruMu;

    private void Awake()
    {
        kartImage = GetComponent<Image>();
        if (kartImage == null)
        {
            kartImage = gameObject.AddComponent<Image>();
        }
    }

    private void Start()
    {
        if (kartKontrol == null)
        {
            kartKontrol = FindFirstObjectByType<KartKontrol>();
            if (kartKontrol == null)
            {
                Debug.LogWarning("Sahnede KartKontrol script'i bulunamadı!");
            }
        }
    }

    public void KartiKur(KartOlusturucu olusturucu, int id)
    {
        if (kartImage == null)
        {
            kartImage = GetComponent<Image>();
        }
        
        kartOlusturucu = olusturucu;
        kartID = id;
        KartiCevir(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (kartOlusturucu == null || onYuzeDogruMu || kartOlusturucu.kartlarKilitli) return;
        KartiCevir(true);
        kartOlusturucu.KartSecildi(this);
        kartKontrol.KartaDokunuldu(kartID);
    }

    public void KartiGizle()
    {
        if (kartImage != null)
        {
            kartImage.enabled = false;
        }
        onYuzeDogruMu = false;
    }

    public void KartiCevir(bool yeniDurum)
    {
        if (kartImage == null)
        {
            kartImage = GetComponent<Image>();
            if (kartImage == null)
            {
                Debug.LogError("Image component bulunamadı!");
                return;
            }
        }

        onYuzeDogruMu = yeniDurum;
        kartImage.enabled = true;
        kartImage.sprite = yeniDurum ? kartOlusturucu.OnYuzSprites[kartID] : kartOlusturucu.ArkaPlanSprite;
    }
}