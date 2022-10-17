namespace AmazonUK.UITests.ObjectModels
{
    using AmazonUK.UITests.Extensions;
    using OpenQA.Selenium;

    public class BooksPage
    {
        private readonly IWebDriver Driver;

        // Texts
        private const string BooksSectionLink = "Books";
        private const string BookTitleContainsText = "Harry Potter and The Cursed Child - Parts One and Two";
        private const string BookTitle = "Harry Potter and the Cursed Child";
        private const string Paperback = "Paperback";
        private const string OneItemText = "1 item";
        public const string Price = "£11.12";

        // Selectors
        private const string NavSearchFieldId = "twotabsearchtextbox";
        private const string DataIndexAtrFirstElement = "div[data-index='1']";
        private const string BookPriceMainSearchPath = "span[class='a-offscreen']";
        private const string PaperbackPriceSelector = "span[class='a-price a-text-price header-price a-size-base a-text-normal']";
        private const string ActiveCartViewForm = "activeCartViewForm";
        private const string SubtotalId = "sc-subtotal-label-activecart";
        private const string CheckBoxSelector = "input[type='checkbox']";
        private const string AddToCardButtonId = "add-to-cart-button";
        private const string NavCartId = "nav-cart";

        public BooksPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public bool FirstResult => Driver.FindElementWithWait(By.CssSelector(DataIndexAtrFirstElement))
                    .Text.ToLower().Contains(BookTitleContainsText.ToLower());

        public bool CheckIfHavePaperback => Driver.FindElementWithWait(By.CssSelector(DataIndexAtrFirstElement))
                        .FindElement(By.LinkText(Paperback)).Displayed;

        public bool IsOneItemInSubtotal => Driver.FindElementWithWait(By.Id(ActiveCartViewForm))
                    .FindElement(By.Id(SubtotalId)).Text.ToLower().Contains(OneItemText);

        public bool CheckboxIsSelected => Driver.FindElementWithWait(By.Id(ActiveCartViewForm))
                    .FindElement(By.CssSelector(CheckBoxSelector)).Selected;

        // Other way to use explicit wait
        //public bool IsTheSameTitle()
        //{
        //    WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));
        //    IWebElement titleBook = wait.Until((d) => Driver.FindElement(By.ClassName("a-truncate-full")));
        //    var isSameTitle = titleBook.Text.ToLower().Contains(BookTitle.ToLower());
        //    return isSameTitle;
        //}

        public bool IsTheSameTitle => Driver.FindElementWithWait(By.ClassName("a-truncate-cut"))
                    .Text.ToLower().Contains(BookTitle.ToLower());

        public IWebElement PriceOnMainSearch => Driver.FindElementWithWait(By.CssSelector(BookPriceMainSearchPath));

        public IWebElement FindPaperbackElementPrice => Driver.FindElementWithWait(By.CssSelector(PaperbackPriceSelector));

        public string PriceInBasket => Driver.FindElementWithWait(By.Id(ActiveCartViewForm))
                    .FindElement(By.ClassName("sc-price")).Text.ToLower();

        public void ClickBooksSectionLink() => Driver.FindElementWithWait(By.LinkText(BooksSectionLink)).Click();

        public void FindBookInSearchField() => Driver.FindElementWithWait(By.Id(NavSearchFieldId)).SendKeys(BookTitle + "\n");

        public void ClickOnPaperback() => Driver.FindElementWithWait(By.CssSelector(DataIndexAtrFirstElement))
            .FindElement(By.LinkText(Paperback)).Click();

        public void ClickOnCheckboxGift() => Driver.FindElementWithWait(By.Id(ActiveCartViewForm))
                    .FindElement(By.CssSelector(CheckBoxSelector)).Click();

        public void AddToCardButtonClick() => Driver.FindElementWithWait(By.Id(AddToCardButtonId)).Click();

        public void NavCardClick() => Driver.FindElementWithWait(By.Id(NavCartId)).Click();
    }
}
