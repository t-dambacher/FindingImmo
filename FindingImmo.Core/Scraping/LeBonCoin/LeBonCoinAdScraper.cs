using System;
using System.Linq;
using System.Collections.Generic;
using FindingImmo.Core.Domain.Models;
using FindingImmo.Core.Infrastructure;
using FindingImmo.Core.Scraping.DataTransfer;
using OpenQA.Selenium;

namespace FindingImmo.Core.Scraping.LeBonCoin
{
    sealed internal class LeBonCoinAdScraper : AdScraper
    {
        private readonly ILogger _logger;

        public LeBonCoinAdScraper(ILogger logger)
        {
            this._logger = logger;
        }

        public override Ad Scrap(IWebDriver driver, AdReference reference)
        {
            if (reference == null)
                throw new ArgumentNullException(nameof(reference));

            driver.Navigate().GoToUrl(reference.Url);

            Ad res = new Ad(Website.LeBonCoin, reference.Reference)
            {
                Price = GetPrice(driver),
                PostalCode = GetPostalCode(driver),
                IsPro = IsPro(driver),
                RoomsCount = GetRoomsCount(driver),
                Surface = GetSurface(driver),
                GES = GetGES(driver),
                EnergyClass = GetEnergyClass(driver),
                Description = GetDescription(driver),
                Title = GetTitle(driver)
            };

            res.Pictures = GetPictures(driver, res);

            return res;
        }

        private string GetTitle(IWebDriver driver)
        {
            return driver.FindElements(By.TagName("h1")).SingleOrDefault(i => i.GetAttribute("itemprop") == "name")?.Text ?? "";
        }

        private IList<Picture> GetPictures(IWebDriver driver, Ad ad)
        {
            List<Picture> res = new List<Picture>();

            IWebElement mainImage = driver.FindElements(By.TagName("img")).SingleOrDefault(i => i.GetAttribute("itemprop") == "image");
            if (mainImage != null)
            {
                res.Add(GetPicture(ad, mainImage.GetAttribute("src")));
                for (int i = 1, thumbsCount = GetThumbsCount(driver); i < thumbsCount; ++i)
                {
                    res.Add(
                        GetPicture(
                            ad,
                            driver.FindElement(By.Id("thumb_" + i)).FindElement(By.TagName("img")).GetAttribute("src")
                        )
                    );
                }
            }

            return res;
        }

        private int GetThumbsCount(IWebDriver driver)
        {
            string stringThumbsCount = driver.FindElements(By.ClassName("item_photo")).SingleOrDefault()?.Text.Replace("photos disponibles", "")?.Trim();
            int thumbsCount = 0;
            int.TryParse(stringThumbsCount, out thumbsCount);
            return thumbsCount;
        }

        private Picture GetPicture(Ad ad, string url)
        {
            url = url.Replace("ad-thumb", "ad-large").Replace("ad-image", "ad-large");
            // var size = ImageTools.GetWebDimensions(url);
            return new Picture(ad, url, 0, 0); //size.Width, size.Height);
            // to much problems occures for now when using this. the whole process is slown, and the retrieved informations are not even ok (because LBC & grey borders on the frames)
        }

        private string GetDescription(IWebDriver driver)
        {
            return driver.FindElements(By.TagName("p")).Single(p => p.GetAttribute("itemprop") == "description")?.Text;
        }

        private GES GetGES(IWebDriver driver)
        {
            string ges = GetElementByPropertyType(driver, "GES")?.Text ?? "";
            int index = ges.IndexOf(' ');
            if (index >= 0)
                ges = ges.Substring(0, index);

            var dummyValues = new HashSet<string>(new[] { "Non", "Vierge" }, StringComparer.OrdinalIgnoreCase);
            GES res = GES.Unknown;
            if (!Enum.TryParse<GES>(ges, out res) && !dummyValues.Contains(ges))
                this._logger.Error($"Unknown GES value : {ges}");

            return res;
        }

        private EnergyClass GetEnergyClass(IWebDriver driver)
        {
            string energyClass = GetElementByPropertyType(driver, "Classe énergie")?.Text ?? "";
            int index = energyClass.IndexOf(' ');
            if (index >= 0)
                energyClass = energyClass.Substring(0, index);

            var dummyValues = new HashSet<string>(new[] { "Non", "Vierge" }, StringComparer.OrdinalIgnoreCase);
            EnergyClass res = EnergyClass.Unknown;
            if (!Enum.TryParse<EnergyClass>(energyClass, out res) && !dummyValues.Contains(energyClass))
                this._logger.Error($"Unknown energy class value : {energyClass}");

            return res;
        }

        private int GetSurface(IWebDriver driver)
        {
            string surface = GetElementByPropertyType(driver, "Surface")?.Text ?? "";
            int index = surface.IndexOf(' ');
            if (index >= 0)
                surface = surface.Substring(0, index);

            int res = 0;
            if (!int.TryParse(surface, out res))
                this._logger.Error($"Unknown surface value : {surface}");

            return res;
        }

        private int GetRoomsCount(IWebDriver driver)
        {
            string numberOfRooms = GetElementByPropertyType(driver, "Pièces")?.Text;
            int res = 0;
            if (!int.TryParse(numberOfRooms, out res))
                this._logger.Error($"Unknown rooms count : {numberOfRooms}");

            return res;
        }

        private string GetPostalCode(IWebDriver driver)
        {
            return new string((GetElementByPropertyType(driver, "Ville")?.Text ?? "").Where(Char.IsNumber).ToArray());
        }

        private int GetPrice(IWebDriver driver)
        {
            IWebElement element = driver.FindElements(By.TagName("h2")).SingleOrDefault(h => h.GetAttribute("itemprop") == "price");
            string price = element?.GetAttribute("content");
            int res = 0;
            if (!int.TryParse(price, out res))
                this._logger.Error($"Unknown price value : {price}");

            return res;
        }

        private bool IsPro(IWebDriver driver)
        {
            return string.Equals(GetElementByPropertyType(driver, "Honoraires")?.Text ?? "", "Oui", StringComparison.InvariantCultureIgnoreCase);
        }

        private IWebElement GetElementByPropertyType(IWebDriver driver, string propertyType)
        {
            IWebElement res = driver.FindElements(By.TagName("h2")).FirstOrDefault(t => t.FindElement(By.ClassName("property"))?.Text == propertyType);
            return res?.FindElement(By.ClassName("value"));
        }
    }
}
