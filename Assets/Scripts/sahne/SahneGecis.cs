using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneGecis : MonoBehaviour
{
    public void SahneGec(string SahneAdi)
    {
        if (SahneAdi == "Cikis")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(SahneAdi);
        }
    }
}