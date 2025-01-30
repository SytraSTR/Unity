using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private int _settings;
    private const int SettingNumber = 2;

    public enum eParcaSayisi
    {
        NotSet = 0,
        Parca10 = 10,
        Parca15 = 15,
        Parca20 = 20,
    }

    public struct Settings
    {
        public eParcaSayisi ParcaSayisi;
    };

    private Settings _gameSettings;

    public static GameSettings Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameSettings = new Settings();
        ResetGameSettings();
    }

    public void SetPairNumber(eParcaSayisi parcaSayisi)
    {
        if (_gameSettings.ParcaSayisi == eParcaSayisi.NotSet)
            _settings++;
        _gameSettings .ParcaSayisi = parcaSayisi;
    }

    public eParcaSayisi GetirParcaSayisi()
    {
        return _gameSettings .ParcaSayisi;
    }

    public void ResetGameSettings()
    {
        _settings = 0;
        _gameSettings.ParcaSayisi = eParcaSayisi.NotSet;
        
    }
}
