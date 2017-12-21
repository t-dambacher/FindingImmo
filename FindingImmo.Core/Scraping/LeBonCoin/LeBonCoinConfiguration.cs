namespace FindingImmo.Core.Scraping.LeBonCoin
{
    sealed internal class LeBonCoinConfiguration
    {
        public int MinSurface => 100;
        public int MaxSurface => 200;
        public string MinPrice => "150 000";
        public string MaxPrice => "350 000";
        public int MinRooms => 4;
        public int MaxRooms => 8;
        public int LastPageToCheck => 20;
        public string RootUrl => "https://www.leboncoin.fr/ventes_immobilieres/offres/alsace/";
    }
}
