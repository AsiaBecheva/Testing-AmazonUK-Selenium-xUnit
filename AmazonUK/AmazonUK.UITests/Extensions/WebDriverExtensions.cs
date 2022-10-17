namespace AmazonUK.UITests.Extensions
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;

    public static class WebDriverExtensions
    {
        public static IWebElement FindElementWithWait(this IWebDriver driver, By by, int timeoutInSeconds = 5)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
    }
}
