using UnityEngine;
using GoogleMobileAds.Api;

public class Reklam : MonoBehaviour
{
    private BannerView bannerView;

    void Start()
    {
        // AdMob'u başlat
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
    }

    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-5996316752504295/4966292776"; // Buraya AdMob banner reklam birimi ID'nizi ekleyin
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // AdRequest oluşturma
        AdRequest request = new AdRequest(); // Builder kullanarak AdRequest oluşturun

        // Banner reklamı yükle
        bannerView.LoadAd(request);
    }

    void OnDestroy()
    {
        // Banner reklamı yok et
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }
}
