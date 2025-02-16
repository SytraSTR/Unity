using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Kart : MonoBehaviour, IPointerClickHandler
{
    public int kartID;
    public KartOlusturucu kartOlusturucu;
    private Image kartImage;
    private bool onYuzeDogruMu = false;
    private bool animasyonDevamEdiyor = false;

    public bool OnYuzeDogruMu => onYuzeDogruMu;

    private void Awake()
    {
        kartImage = GetComponent<Image>();
    }

    public void KartiKur(KartOlusturucu olusturucu, int id)
    {
        kartOlusturucu = olusturucu;
        kartID = id;
        KartiCevir(false, true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (kartOlusturucu == null || onYuzeDogruMu || kartOlusturucu.kartlarKilitli || animasyonDevamEdiyor) 
            return;

        KartiCevir(true, false);
        kartOlusturucu.KartSecildi(this);
    }

    public void KartiGizle()
    {
        gameObject.SetActive(false);
        onYuzeDogruMu = false;
    }

    public void KartiCevir(bool yeniDurum, bool anindaCevir)
    {
        if (kartImage == null || animasyonDevamEdiyor) return;
        onYuzeDogruMu = yeniDurum;

        if (anindaCevir) 
        {
            kartImage.sprite = yeniDurum ? kartOlusturucu.OnYuzSprites[kartID] : kartOlusturucu.ArkaPlanSprite;
            return;
        }

        animasyonDevamEdiyor = true;
        transform.DORotate(new Vector3(0, 90, 0), 0.25f).OnComplete(() =>
        {
            kartImage.sprite = yeniDurum ? kartOlusturucu.OnYuzSprites[kartID] : kartOlusturucu.ArkaPlanSprite;
            transform.DORotate(new Vector3(0, 0, 0), 0.25f).OnComplete(() =>
            {
                animasyonDevamEdiyor = false;
            });
        });
    }
}
