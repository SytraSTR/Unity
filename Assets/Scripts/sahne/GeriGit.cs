using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GeriGit : MonoBehaviour
{
    private float sonBasmaZamani;
    private float ciftBasmaAraligi = 1f; // İki basma arasında izin verilen maksimum süre
    [SerializeField] private GameObject gizliNesne; // Yeni nesne, SerializeField ile eklendi

    void Start()
    {
        // Gizli nesneyi başlangıçta görünmez yap
        gizliNesne?.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            float simdikiZaman = Time.time;
            switch (SceneManager.GetActiveScene().name)
            {
                case "OyunArayuzu":
                    // OyunArayuzu sahnesindeysen direkt çık
                    GeriButonu();
                    break;
                case "AnaMenu":
                    // Ana Menüdeki çift basma kontrolü
                    if (simdikiZaman - sonBasmaZamani <= ciftBasmaAraligi)
                    {
                        // Ana menüde çıkış yap
                        Application.Quit();
                    }
                    else
                    {
                        UyariGoster();
                        sonBasmaZamani = simdikiZaman; // Zamanı güncelle
                    }
                    break;
                default:
                    if (simdikiZaman - sonBasmaZamani <= ciftBasmaAraligi)
                    {
                        GeriButonu();
                    }
                    else
                    {   
                        UyariGoster();
                    }
                    sonBasmaZamani = simdikiZaman;
                    break;
            }
        }
    }

    private void UyariGoster()
    {
        // Uyarı nesnesini göster
        if (gizliNesne != null)
        {
            gizliNesne.SetActive(true);
            StartCoroutine(GizliNesneGorunur());
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

    private IEnumerator GizliNesneGorunur()
    {
        yield return new WaitForSeconds(1f);
        gizliNesne?.SetActive(false);
    }
}
