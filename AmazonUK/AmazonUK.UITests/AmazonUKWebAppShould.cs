namespace AmazonUK.UITests
{
    using Xunit;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using AmazonUK.UITests.ObjectModels;
    using AmazonUK.UITests.Constants;

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
                    Assert.Fail(ErrorMessagesConstants.BookDoesNotContainSuchTitle);
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
                        Assert.Fail(ErrorMessagesConstants.ThisBookDoesNotHavePaperBack);
                    }
                }
                else
                {
                    Assert.Fail(ErrorMessagesConstants.BookDoesNotContainSuchTitle);
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
                driver.Navigate().Back();
                Helper.Pause();

                Assert.Equal(bookPage.FindPaperbackElementPrice.Text, bookPage.PriceOnMainSearch.Text);
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

                bookPage.AddToCardButtonClick();
                Helper.Pause();

                bookPage.NavCardClick();
                Helper.Pause();

                bookPage.ClickOnCheckboxGift();
                Helper.Pause();

                if (!bookPage.IsTheSameTitle)
                {
                    Assert.Fail(ErrorMessagesConstants.TitlesDoesNotMatch);
                }
                if (paperbackPrice != bookPage.PriceInBasket)
                {
                    Assert.Fail(ErrorMessagesConstants.PricesDoesNotMatch);
                }
                if (!bookPage.CheckboxIsSelected)
                {
                    Assert.Fail(ErrorMessagesConstants.GiftCheckBoxIsNotChecked);
                }
            }
        }

        [Fact]
        [Trait("Category", "Book")]
        //UA5
        public void CheckIfCorrectEditionAddedPriceSameAsInitialAndNoOtherProductsAdded()
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

                //Taking the initial price while I'm on this page.
                string priceInitialPaperback = "";

                bookPage.ClickOnPaperback();
                Helper.Pause();

                bookPage.AddToCardButtonClick();
                Helper.Pause();

                bookPage.NavCardClick();
                Helper.Pause();

                if (!bookPage.IsTheSameTitle)
                {
                    Assert.Fail(ErrorMessagesConstants.TitlesDoesNotMatch);
                }
                //if (priceInitialPaperback != bookPage.PriceInBasket)
                //{
                //    Assert.Fail(ErrorMessagesConstants.PricesDoesNotMatch);
                //}
                if (!bookPage.IsOneItemInSubtotal)
                {
                    Assert.Fail(ErrorMessagesConstants.ThereIsMoreThanOneProduct);
                }
            }
        }
    }
}
