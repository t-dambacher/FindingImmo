using FindingImmo.Core.Infrastructure.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.ObjectModel;
using System.Net.Sockets;

namespace FindingImmo.Core.Scraping.Sites
{
    sealed internal class WebDriver : IWebDriver
    {
        private const int NumberOfRequetPerNavigatorInstance = 20;
        private int _navigationCalls;
        private readonly ILogger _logger;

        public WebDriver(ILogger logger)
        {
            this._logger = logger;
            this._decorated = BuildNewDriver();
            this._navigationCalls = 0;
        }

        #region IWebDriver implementation

        private IWebDriver _decorated;

        public string Url
        {
            get { return _decorated.Url; }
            set { _decorated.Url = value; }
        }

        public string Title => _decorated.Title;

        public string PageSource => _decorated.PageSource;

        public string CurrentWindowHandle => _decorated.CurrentWindowHandle;

        public ReadOnlyCollection<string> WindowHandles => _decorated.WindowHandles;

        public void Close()
        {
            _decorated.Close();
        }

        public void Dispose()
        {
            _decorated.Dispose();
        }

        public IWebElement FindElement(By by)
        {
            return _decorated.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _decorated.FindElements(by);
        }

        public IOptions Manage()
        {
            return _decorated.Manage();
        }

        public INavigation Navigate()
        {
            return new Navigation(_decorated.Navigate(), this);
        }

        public void Quit()
        {
            _decorated.Quit();
        }

        public ITargetLocator SwitchTo()
        {
            return _decorated.SwitchTo();
        }

        #endregion

        public void NavigationCalled()
        {
            ++this._navigationCalls;
            string suffix = this._navigationCalls == 1 ? "st" : (this._navigationCalls == 2 ? "nd" : (this._navigationCalls == 3 ? "rd" : "th"));
            this._logger.Info($"Changing url for the {this._navigationCalls}{suffix} time.");

            if (this._navigationCalls % NumberOfRequetPerNavigatorInstance == 0)
            {
                this._logger.Info("Reseting the browser...");
                string currentUrl = this._decorated.Url;
                this._decorated.Dispose();
                this._decorated = BuildNewDriver();
                this._decorated.Navigate().GoToUrl(currentUrl);
            }
        }

        private IWebDriver BuildNewDriver()
        {
            IWebDriver res = new ChromeDriver(
                new ChromeOptions()
                {
                    PageLoadStrategy = PageLoadStrategy.Normal
                }
            );

            var timeout = TimeSpan.FromSeconds(60);
            res.Manage().Timeouts().AsynchronousJavaScript = timeout;
            res.Manage().Timeouts().ImplicitWait = timeout;
            res.Manage().Timeouts().PageLoad = timeout;

            return res;
        }

        #region Nested classes

        private class Navigation : INavigation
        {
            private readonly INavigation _decorated;
            private readonly WebDriver _driver;

            public Navigation(INavigation decorated, WebDriver driver)
            {
                this._decorated = decorated;
                this._driver = driver;
            }

            private void AfterNavigation()
            {
                this._driver.NavigationCalled();
            }

            #region INavigation implementation

            public void Back()
            {
                TryOrRetry(() => _decorated.Back());
            }

            public void Forward()
            {
                TryOrRetry(() => _decorated.Forward());
            }

            public void GoToUrl(string url)
            {
                TryOrRetry(() => _decorated.GoToUrl(url));
                AfterNavigation();
            }

            public void GoToUrl(Uri url)
            {
                TryOrRetry(() => _decorated.GoToUrl(url));
                AfterNavigation();
            }

            public void Refresh()
            {
                TryOrRetry(() => _decorated.Refresh());
            }

            private void TryOrRetry(Action action)
            {
                try
                {
                    action();
                }
                catch (Exception ex) when (ex.GetBaseException() is SocketException)
                {
                    action();
                }
            }

            #endregion
        }

        #endregion
    }
}
