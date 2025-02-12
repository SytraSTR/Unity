using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Added for scene management

public class Skor : MonoBehaviour
{
    private TextMeshProUGUI skorText;
    private int skor = 0;

    void Start()
    {
        skorText = GetComponent<TextMeshProUGUI>();
        
        // Check if the current scene is OyunKazanma
        if (SceneManager.GetActiveScene().name == "OyunKazanma")
        {
            skor = PlayerPrefs.GetInt("FinalScore", 0); // Retrieve the score if it exists
        }
        SkoruGuncelle();
    }

    public void SkorEkle(int puan)
    {
        skor = puan; // Update the score by adding points
        SkoruGuncelle();
        PlayerPrefs.SetInt("FinalScore", skor); // Store the score for the OyunKazanma scene
    }

    private void SkoruGuncelle()
    {
        skorText.text = $"Skor: {skor}";
    }
}
