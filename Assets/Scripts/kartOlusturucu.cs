using UnityEngine;
using UnityEngine.UI;

public class kartOlusturucu : MonoBehaviour
{
    public int parcaSayisi;

    public GameObject hazirKart;
    public Transform izgaraNesne;
    public void IzgaraAyari()
    {
        GridLayoutGroup izgara = izgaraNesne.GetComponent<GridLayoutGroup>();

        if (izgara == null )
        {
            Debug.LogError("Izgara Bulunamadý.");
            return;
        }

        parcaSayisi = ParcaSayisi.Instance.parcaSayisi;
        izgara.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        izgara.constraintCount = parcaSayisi;

        float panelGenislik = izgaraNesne.GetComponent<RectTransform>().rect.width;
        float hucreBoyutu = panelGenislik / parcaSayisi - izgara.spacing.x;

        izgara.cellSize = new Vector2(hucreBoyutu, hucreBoyutu);

        IzgaraOlustur();
    }
    public void IzgaraOlustur()
    {
        foreach (Transform child in izgaraNesne)
        {
            Destroy(child.gameObject);
        }

        int totalKartlar = parcaSayisi * parcaSayisi;

        for (int i = 0; i < totalKartlar; i++)
        {
            Instantiate(hazirKart, izgaraNesne);
        }
    }

    private void Start()
    { 
        IzgaraAyari();
        
    }
}
