namespace App.Scripts.Infrastructure.Services.EventBus.Ads
{
    public struct NoAds : IEvent
    {
        public bool IsNoAds;

        public NoAds(bool isNoAds) => IsNoAds = isNoAds;
    }
}