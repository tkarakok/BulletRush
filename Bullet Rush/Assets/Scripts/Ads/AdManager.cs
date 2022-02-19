using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdManager : Singleton<AdManager>
{
    public string bannerId;
    public string interstitialId;
    public string rewardedAdId;

    private InterstitialAd _interstitialAd;
    private BannerView _bannerView;
    private RewardedAd _rewardedAd;


    AdRequest AdRequest()
    {
        return new AdRequest.Builder().Build();
    }
    private void Start()
    {

        RequestBanner();
        RequestInterstitialAd();
        RequestRewardedAd();
    }


    #region Rewarded Ad
    void RequestRewardedAd()
    {
        _rewardedAd = new RewardedAd(rewardedAdId);
        _rewardedAd.LoadAd(AdRequest());

        _rewardedAd.OnUserEarnedReward += RewardedGift;
    }

    public void RewardAdShow()
    {
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
        }
        else
        {
            RequestRewardedAd();
            _rewardedAd.Show();
        }
    }
    #endregion

    #region Interstitial Ad 
    void RequestInterstitialAd()
    {
        _interstitialAd = new InterstitialAd(interstitialId);
        _interstitialAd.LoadAd(AdRequest());

        _interstitialAd.OnAdClosed += AgainRequest;
    }

    public void InterstitialAdShow()
    {
        if (_interstitialAd.IsLoaded())
        {
            _interstitialAd.Show();
        }
        else
        {
            RequestInterstitialAd();
            _interstitialAd.Show();
        }
    }
    #endregion

    #region Banner Ad
    void RequestBanner()
    {
        _bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        _bannerView.LoadAd(request);
    }



    void DestroyAd()
    {
        _bannerView.Destroy();
    }
    #endregion

    #region Events For Ad
    public void AgainRequest(object sender, EventArgs args)
    {
        RequestInterstitialAd();
    }

    public void RewardedGift(object sender, Reward args)
    {
        string type = args.Type;
        double amount = 250;

        
    }
    #endregion





}