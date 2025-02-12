using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Added for scene management

public class Saniye : MonoBehaviour
{
    public static Saniye Instance { get; private set; }
    private TextMeshProUGUI saniyeText;
    private float gecenSure = 0f;
    
    private bool oyunTamamlandi = false;
    private float bitisSuresi = 0f; // New variable to hold the end time

    void Start()
    {
        saniyeText = GetComponent<TextMeshProUGUI>();
        if (SceneManager.GetActiveScene().name == "OyunKazanma")
        {
            bitisSuresi = PlayerPrefs.GetFloat("KazanilanSure", 0f); // Retrieve the stored time
            oyunTamamlandi = true; // Ensure the timer does not continue in the OyunKazanma scene
            saniyeText.text = $"Süre: {Mathf.Floor(bitisSuresi)}"; // Show final time immediately
        }
    }

    void Update()
    {
        if (!oyunTamamlandi)
        {
            gecenSure += Time.deltaTime;
            saniyeText.text = $"Saniye: {Mathf.Floor(gecenSure)}";
        }
    }

    public void OyunuBitir()
    {
        oyunTamamlandi = true;
        bitisSuresi = gecenSure;
         // Show final time on game over
        Debug.Log($"OyunuBitir çağrıldı. Geçen Süre: {bitisSuresi}"); // Log to check if method is called
        // Store the elapsed time
        PlayerPrefs.SetFloat("KazanilanSure", bitisSuresi); // Use SetFloat instead of SetInt
    }

    public void OyunuDurdur()
    {
        if (Instance != null)
        {
            Instance.OyunuBitir();
        }
    }
}