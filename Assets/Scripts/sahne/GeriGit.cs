using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GeriGit : MonoBehaviour
{
    private float sonBasmaZamani;
    private float ciftBasmaAraligi = 0.5f; // İki basma arasında izin verilen maksimum süre
    private TextMeshProUGUI uyariText;
    private float uyariSuresi = 1f;
    private float uyariZamani;
    private bool uyariGosteriliyor;

    void Start()
    {
        uyariText = GameObject.Find("UyariText")?.GetComponent<TextMeshProUGUI>();
        if (uyariText != null)
        {
            uyariText.text = "";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            float simdikiZaman = Time.time;
            if (simdikiZaman - sonBasmaZamani <= ciftBasmaAraligi)
            {
                GeriButonu();
            }
            else
            {
                UyariGoster("Çıkmak için ESC tuşuna tekrar basın");
            }
            sonBasmaZamani = simdikiZaman;
        }

        // Uyarı metnini belirli süre sonra kaldır
        if (uyariGosteriliyor && Time.time - uyariZamani >= uyariSuresi)
        {
            if (uyariText != null)
            {
                uyariText.text = "";
                uyariGosteriliyor = false;
            }
        }
    }

    private void UyariGoster(string mesaj)
    {
        if (uyariText != null)
        {
            uyariText.text = mesaj;
            uyariGosteriliyor = true;
            uyariZamani = Time.time;
        }
    }

    public void GeriButonu()
    {
        int geriSahneIndex = SceneManager.GetActiveScene().buildIndex - 1;

        // Geçerli sahneden önceki sahne olup olmadığını kontrol et
        if (geriSahneIndex >= 0)
        {
            SceneManager.LoadScene(geriSahneIndex);
        }
    }
} 
