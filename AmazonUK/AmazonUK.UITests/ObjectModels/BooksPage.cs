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
        private const string BookTitle = "Harry Potter and the Cursed Child";
        private const string Paperback = "Paperback";

        public BooksPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public bool FirstResult => Driver.FindElement(By.CssSelector(DataIndexAtrFirstElement))
                    .Text.Contains(BookTitleContainsText);

        public bool CheckIfHavePaperback => Driver.FindElement(By.CssSelector(DataIndexAtrFirstElement))
                        .FindElement(By.LinkText(Paperback)).Displayed;

        public void ClickBooksSectionLink() => Driver.FindElement(By.LinkText(BooksSectionLink)).Click();

        public void FindBookInSearchField() => Driver.FindElement(By.Id(NavSearchFieldId)).SendKeys(BookTitle + "\n");

        public void ClickOnPaperback() => Driver.FindElement(By.CssSelector(DataIndexAtrFirstElement))
            .FindElement(By.LinkText(Paperback)).Click();
    }
}
