using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Kart : MonoBehaviour, IPointerClickHandler
{
    public int kartID;
    public KartOlusturucu kartOlusturucu;
    private Image kartImage;
    private bool onYuzeDogruMu = false;

    public bool OnYuzeDogruMu => onYuzeDogruMu;

    private void Awake()
    {
        kartImage = GetComponent<Image>();
    }

    public void KartiKur(KartOlusturucu olusturucu, int id)
    {
        Image kartImage = GetComponent<Image>() ?? throw new System.Exception("Image component bulunamadı!");
        
        kartOlusturucu = olusturucu;
        kartID = id;
        KartiCevir(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (kartOlusturucu == null || onYuzeDogruMu || kartOlusturucu.kartlarKilitli) return;
        KartiCevir(true);
        kartOlusturucu.KartSecildi(this);
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
            kartImage = GetComponent<Image>() ?? throw new System.Exception("Image component bulunamadı!");
        }

        onYuzeDogruMu = yeniDurum;
        kartImage.enabled = true;
        kartImage.sprite = yeniDurum ? kartOlusturucu.OnYuzSprites[kartID] : kartOlusturucu.ArkaPlanSprite;
    }
}