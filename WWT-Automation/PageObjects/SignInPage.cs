using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace WWT_Automation.PageObjects
{
    public class SignInPage
    {
        private IWebDriver _driver;
        public SignInPage(IWebDriver driver)
        {
               _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.Id, Using = "UserName")]
        public IWebElement Username { get; }
    }
}
