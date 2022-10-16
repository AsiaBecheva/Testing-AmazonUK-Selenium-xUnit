using OpenQA.Selenium;

namespace AmazonUK.UITests.ObjectModels
{
    public class BooksPage
    {
        private readonly IWebDriver Driver;
        private const string BooksSectionLink = "Books";
        private const string NavSearchFieldId = "twotabsearchtextbox";
        private const string DataIndexAtrFirstElement = "div[data-index='1']";
        private const string BookTitleContainsText = "Harry Potter and The Cursed Child - Parts One and Two";
        public const string BookTitle = "Harry Potter and the Cursed Child";
        private const string Paperback = "Paperback";
        private const string BookPriceMainSearchPath = "span[class='a-offscreen']";
        private const string PaperbackPriceSelector = "span[class='a-size-base a-color-price a-color-price']";
        public const string Price = "£11.12";

        public BooksPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public bool FirstResult => Driver.FindElement(By.CssSelector(DataIndexAtrFirstElement))
                    .Text.Contains(BookTitleContainsText);

        public bool CheckIfHavePaperback => Driver.FindElement(By.CssSelector(DataIndexAtrFirstElement))
                        .FindElement(By.LinkText(Paperback)).Displayed;

        public IWebElement PriceOnMainSearch => Driver.FindElement(By.CssSelector(BookPriceMainSearchPath));

        public void ClickBooksSectionLink() => Driver.FindElement(By.LinkText(BooksSectionLink)).Click();

        public void FindBookInSearchField() => Driver.FindElement(By.Id(NavSearchFieldId)).SendKeys(BookTitle + "\n");

        public void ClickOnPaperback() => Driver.FindElement(By.CssSelector(DataIndexAtrFirstElement))
            .FindElement(By.LinkText(Paperback)).Click();

        public IWebElement FindPaperbackElementPrice => Driver.FindElement(By.CssSelector(PaperbackPriceSelector));
    }
}
