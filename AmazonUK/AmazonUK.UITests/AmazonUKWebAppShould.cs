namespace AmazonUK.UITests
{
    using Xunit;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using AmazonUK.UITests.ObjectModels;

    public class AmazonUKWebAppShould
    {
        [Fact]
        [Trait("Category","Smoke")]
        //UA1
        public void LoadApplicationPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo(driver);
            }
        }

        [Fact]
        [Trait("Category", "Book")]
        //UA2
        public void SearchInBooksFieldVerifyEdition()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo(driver);
                var bookPage = new BooksPage(driver);

                bookPage.ClickBooksSectionLink();
                Helper.Pause();
                bookPage.FindBookInSearchField();
                Helper.Pause();

                if (!bookPage.FirstResult)
                {
                    Assert.Fail("The book does not contain such Title");
                }
            }
        }

        [Fact]
        [Trait("Category", "Book")]
        //UA2
        public void SearchInBooksFieldCheckVersionPaperback()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo(driver);
                var bookPage = new BooksPage(driver);

                bookPage.ClickBooksSectionLink();
                Helper.Pause();
                bookPage.FindBookInSearchField();
                Helper.Pause();

                if (bookPage.FirstResult)
                {
                    Helper.Pause();

                    if (!bookPage.CheckIfHavePaperback)
                    {
                        Helper.Pause();
                        Assert.Fail("This book does not have paperback!");
                    }
                }
                else
                {
                    Assert.Fail("The book does not contain such Title.");
                }
            }
        }

        [Fact]
        [Trait("Category", "Book")]
        //UA2
        public void SearchInBooksFieldCheckPaperbackPrice()
        {
            string paperbackPriceSelector = "span[class='a-size-base a-color-price a-color-price']";
            string price = "£11.12";

            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo(driver);
                var bookPage = new BooksPage(driver);

                bookPage.ClickBooksSectionLink();
                Helper.Pause();
                bookPage.FindBookInSearchField();
                Helper.Pause();
                bookPage.ClickOnPaperback();
                Helper.Pause();
                var paperbackElement = driver.FindElement(By.CssSelector(paperbackPriceSelector));

                Assert.Equal(price, paperbackElement.Text);
            }
        }

        [Fact]
        [Trait("Category", "Book")]
        //UA3
        public void CheckPaperbackPriceIsSameAsPageOnBack()
        {
            string paperbackPriceSelector = "span[class='a-size-base a-color-price a-color-price']";

            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo(driver);
                var bookPage = new BooksPage(driver);

                bookPage.ClickBooksSectionLink();
                Helper.Pause();
                bookPage.FindBookInSearchField();
                Helper.Pause();
                bookPage.ClickOnPaperback();
                Helper.Pause();
                var paperbackElement = driver.FindElement(By.CssSelector(paperbackPriceSelector)).Text;
                driver.Navigate().Back();
                Helper.Pause();


                Assert.Equal(paperbackElement, bookPage.PriceOnMainSearch.Text);
            }
        }
    }
}
