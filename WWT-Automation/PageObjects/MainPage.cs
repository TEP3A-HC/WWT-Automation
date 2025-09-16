using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WWT_Automation.PageObjects._1.MerchantProfiles;

namespace WWT_Automation.PageObjects
{
    public class MainPage : BasePage
    {
        public MainPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        private readonly By _merchantProfiles = By.CssSelector("li[role='button'][data-custom-id = 'merchant-profiles']");
        private readonly By _lookup = By.CssSelector("li[role='button'][data-custom-id = 'merchants']");

        public MainPage GoToMerchantProfiles()
        {
            Click(_merchantProfiles);
            return new MainPage(Driver, Wait);
        }

        public LookupPage ClickOnLookup()
        {
            Click(_lookup);
            return new LookupPage(Driver, Wait);
        }

    }
}
