namespace AmazonUK.UITests.ObjectModels
{
    using System;
    using System.Net;
    using OpenQA.Selenium;

    public class HomePage
    {
        private readonly IWebDriver Driver;
        private const string PageUrl = "https://www.amazon.co.uk/";
        private const string PageTitle = "Amazon.co.uk: Low Prices in Electronics, Books, Sports Equipment & more";
        private const string CookieId = "sp-cc-accept";

        public HomePage(IWebDriver driver)
        {
            Driver = driver;           
        }

        public void NavigateTo(IWebDriver driver)
        {
            Driver.Navigate().GoToUrl(PageUrl);
            EnsurePageLoaded();
            driver.FindElement(By.Id(CookieId)).Click();
        }

        public void EnsurePageLoaded()
        {
            bool pageHasLoaded = (Driver.Url == PageUrl) && (Driver.Title == PageTitle);

            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Page URL = '{Driver.Url}' Page Source: \r\n {Driver.PageSource}");
            }
        }
    }
}
