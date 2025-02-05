using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneGecis : MonoBehaviour
{
    public void SahneGec(string SahneAdi)
    {
        SceneManager.LoadScene(SahneAdi);
    }
}