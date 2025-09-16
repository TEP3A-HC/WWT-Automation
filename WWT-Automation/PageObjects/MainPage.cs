using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WWT_Automation.PageObjects
{
    public class MainPage : BasePage
    {
        IWebDriver _driver;
        public MainPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }
        
    }
}
