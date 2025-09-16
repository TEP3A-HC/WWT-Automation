using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WWT_Automation.PageObjects._1.MerchantProfiles
{
    public class LookupPage : BasePage
    {
        public LookupPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        private readonly By _accountManagerDropdown = By.CssSelector("md-select[ng-model='vm.AccountManagerSearch']");
        private IWebElement _accountManagerDropdownElement => Driver.FindElement(By.CssSelector("md-select[ng-model='vm.AccountManagerSearch']"));
        private readonly By _accountManagers = By.CssSelector("md-select-menu[role='presentation'][class='_md md-overflow']");

        private readonly By _agentDropdown = By.CssSelector("md-select[ng-model='vm.AccountManagerSearch']");
        private readonly By _merchantStatusDropdown = By.CssSelector("md-select[ng-model='vm.AccountManagerSearch']");

        public LookupPage OpenAccountManagerDropdown()
        {
            Click(_accountManagerDropdown);
            return new LookupPage(Driver, Wait);
        }

        public IList<IWebElement> GetAvailableAccountManagers()
        {
            var select = new SelectElement(_accountManagerDropdownElement);
            return select.Options;
        }
    }
}
