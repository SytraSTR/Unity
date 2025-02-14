using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening; // Make sure to include the DoTween namespace

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
            gameObject.SetActive(false);
        }
        onYuzeDogruMu = false;
    }

    public void KartiCevir(bool yeniDurum)
    {
        if (kartImage != null)
        {
            onYuzeDogruMu = yeniDurum;
            kartImage.enabled = true;

            float targetRotation = yeniDurum ? 180f : 0f; // Rotate to 90 degrees for face-up, 0 for face-down
            kartImage.DOFade(0, 1f).OnComplete(() => // Fade out the image
            {
                transform.DORotate(new Vector3(0, targetRotation, 0), 0.5f).OnComplete(() =>
                {
                    kartImage.sprite = yeniDurum ? kartOlusturucu.OnYuzSprites[kartID] : kartOlusturucu.ArkaPlanSprite;
                    kartImage.DOFade(1, 0.25f); // Fade in the image
                });
            });
        }
    }
}