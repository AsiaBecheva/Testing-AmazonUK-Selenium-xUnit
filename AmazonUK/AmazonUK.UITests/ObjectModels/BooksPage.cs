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
        private const string BookPriceMainSearchPath = "span[class='a-offscreen']";
        private const string PaperbackPriceSelector = "span[class='a-size-base a-color-price a-color-price']";
        public const string Price = "£11.12";
        private const string ActiveCartViewForm = "activeCartViewForm";
        private const string SubtotalId = "sc-subtotal-label-activecart";
        private const string OneItemText = "1 item";
        private const string CheckBoxSelector = "input[type='checkbox']";
        private const string AddToCardButtonId = "add-to-cart-button";
        private const string NavCartId = "nav-cart";

        public BooksPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public bool FirstResult => Driver.FindElement(By.CssSelector(DataIndexAtrFirstElement))
                    .Text.ToLower().Contains(BookTitleContainsText.ToLower());

        public bool CheckIfHavePaperback => Driver.FindElement(By.CssSelector(DataIndexAtrFirstElement))
                        .FindElement(By.LinkText(Paperback)).Displayed;

        public bool IsOneItemInSubtotal => Driver.FindElement(By.Id(ActiveCartViewForm))
                    .FindElement(By.Id(SubtotalId)).Text.ToLower().Contains(OneItemText);

        public bool CheckboxIsSelected => Driver.FindElement(By.Id(ActiveCartViewForm))
                    .FindElement(By.CssSelector(CheckBoxSelector)).Selected;

        public bool IsTheSameTitle => Driver.FindElement(By.ClassName("a-truncate-cut"))
                    .Text.ToLower().Contains(BookTitle.ToLower());

        public IWebElement PriceOnMainSearch => Driver.FindElement(By.CssSelector(BookPriceMainSearchPath));

        public IWebElement FindPaperbackElementPrice => Driver.FindElement(By.CssSelector(PaperbackPriceSelector));

        public string PriceInBasket => Driver.FindElement(By.Id(ActiveCartViewForm))
                    .FindElement(By.ClassName("sc-price")).Text.ToLower();

        public void ClickBooksSectionLink() => Driver.FindElement(By.LinkText(BooksSectionLink)).Click();

        public void FindBookInSearchField() => Driver.FindElement(By.Id(NavSearchFieldId)).SendKeys(BookTitle + "\n");

        public void ClickOnPaperback() => Driver.FindElement(By.CssSelector(DataIndexAtrFirstElement))
            .FindElement(By.LinkText(Paperback)).Click();

        public void ClickOnCheckboxGift() => Driver.FindElement(By.Id(ActiveCartViewForm))
                    .FindElement(By.CssSelector(CheckBoxSelector)).Click();

        public void AddToCardButtonClick() => Driver.FindElement(By.Id(AddToCardButtonId)).Click();

        public void NavCardClick() => Driver.FindElement(By.Id(NavCartId)).Click();
    }
}
