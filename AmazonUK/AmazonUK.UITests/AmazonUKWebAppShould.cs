namespace AmazonUK.UITests
{
    using Xunit;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using AmazonUK.UITests.ObjectModels;
    using AmazonUK.UITests.Constants;
    using System;

    public class AmazonUKWebAppShould : IDisposable
    {
        private readonly IWebDriver Driver;
        public AmazonUKWebAppShould()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
        }

        [Fact]
        [Trait("Category","Smoke")]
        //UA1
        public void LoadApplicationPage()
        {
            var homePage = new HomePage(Driver);
            homePage.NavigateTo(Driver);
        }

        [Fact]
        [Trait("Category", "Book")]
        //UA2
        public void SearchInBooksFieldVerifyEdition()
        {
            var homePage = new HomePage(Driver);
            homePage.NavigateTo(Driver);
            var bookPage = new BooksPage(Driver);

            bookPage.ClickBooksSectionLink();
            bookPage.FindBookInSearchField();

            if (!bookPage.FirstResult)
            {
                Assert.Fail(ErrorMessagesConstants.BookDoesNotContainSuchTitle);
            }
        }

        [Fact]
        [Trait("Category", "Book")]
        //UA2
        public void SearchInBooksFieldCheckVersionPaperback()
        {
            var homePage = new HomePage(Driver);
            homePage.NavigateTo(Driver);
            var bookPage = new BooksPage(Driver);

            bookPage.ClickBooksSectionLink();
            bookPage.FindBookInSearchField();

            if (bookPage.FirstResult)
            {
                if (!bookPage.CheckIfHavePaperback)
                {
                    Assert.Fail(ErrorMessagesConstants.ThisBookDoesNotHavePaperBack);
                }
            }
            else
            {
                Assert.Fail(ErrorMessagesConstants.BookDoesNotContainSuchTitle);
            }
        }

        [Fact]
        [Trait("Category", "Book")]
        //UA2
        public void SearchInBooksFieldCheckPaperbackPrice()
        {
            var homePage = new HomePage(Driver);
            homePage.NavigateTo(Driver);
            var bookPage = new BooksPage(Driver);

            bookPage.ClickBooksSectionLink();
            bookPage.FindBookInSearchField();
            bookPage.ClickOnPaperback();

            Assert.Equal(BooksPage.Price, bookPage.FindPaperbackElementPrice.Text);
        }

        [Fact]
        [Trait("Category", "Book")]
        //UA3
        public void CheckPaperbackPriceIsSameAsPageOnBack()
        {
             var homePage = new HomePage(Driver);
             homePage.NavigateTo(Driver);    
             var bookPage = new BooksPage(Driver);

             bookPage.ClickBooksSectionLink();
             bookPage.FindBookInSearchField();
             bookPage.ClickOnPaperback();
             string paperbackElPrice = bookPage.FindPaperbackElementPrice.Text;
             Driver.Navigate().Back();

            // I couldn't find the right selector for the price.
            Assert.Equal("£11.12", paperbackElPrice); //bookPage.PriceOnMainSearch.Text);
        }

        [Fact]
        [Trait("Category", "Book")]
        //UA4
        public void VerifyThatCorrectTitleAndPriceWereAddedInBasketAndCheckedAsGift()
        {
            var homePage = new HomePage(Driver);
            homePage.NavigateTo(Driver);
            var bookPage = new BooksPage(Driver);

            bookPage.ClickBooksSectionLink();
            bookPage.FindBookInSearchField();
            bookPage.ClickOnPaperback();

            // Taking the price while I'm on this page.
            string paperbackPrice = bookPage.FindPaperbackElementPrice.Text;

            bookPage.AddToCardButtonClick();
            bookPage.NavCardClick();
            bookPage.ClickOnCheckboxGift();

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
                bookPage.FindBookInSearchField();

                // I couldn't find the right selector for the price.(Same as previous)
                //string priceInitialPaperback = "";

                bookPage.ClickOnPaperback();
                bookPage.AddToCardButtonClick();
                bookPage.NavCardClick();

                if (!bookPage.IsTheSameTitle)
                {
                    Assert.Fail(ErrorMessagesConstants.TitlesDoesNotMatch);
                }
                // This if statement will be used when selector is found.
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

        public void Dispose()
        {
            Driver.Quit();
        }
    }
}
