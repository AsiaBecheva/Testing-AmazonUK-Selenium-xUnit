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

                Assert.Equal(BooksPage.Price, bookPage.FindPaperbackElementPrice.Text);
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

        [Fact]
        [Trait("Category", "Book")]
        //UA4
        public void VerifyThatCorrectTitleAndPriceWereAddedInBasketAndCheckedAsGift()
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
                bookPage.ClickOnPaperback();
                Helper.Pause();

                //Taking the price while I'm on this page.
                string paperbackPrice = bookPage.FindPaperbackElementPrice.Text.ToLower();
                Helper.Pause();

                driver.FindElement(By.Id("add-to-cart-button")).Click();
                Helper.Pause();
                driver.FindElement(By.Id("nav-cart")).Click();
                Helper.Pause();

                bool isTheSameTitle = driver.FindElement(By.ClassName("a-truncate-cut"))
                    .Text.ToLower().Contains(BooksPage.BookTitle.ToLower());
                Helper.Pause();

                //Taking the price in Basket
                string pathToPriceInBasket = driver.FindElement(By.Id("activeCartViewForm"))
                    .FindElement(By.ClassName("sc-price")).Text.ToLower();

                driver.FindElement(By.Id("activeCartViewForm"))
                    .FindElement(By.CssSelector("input[type='checkbox']")).Click();
                Helper.Pause();

                bool checkboxIsSelected = driver.FindElement(By.Id("activeCartViewForm"))
                    .FindElement(By.CssSelector("input[type='checkbox']")).Selected;
                Helper.Pause();

                if (!isTheSameTitle)
                {
                    Assert.Fail("Titles does not match!");
                }
                if (paperbackPrice != pathToPriceInBasket)
                {
                    Assert.Fail("Prices does not match!");
                }
                if (!checkboxIsSelected)
                {
                    Assert.Fail("Gift checkbox is not checked!");
                }
            }
        }
    }
}
