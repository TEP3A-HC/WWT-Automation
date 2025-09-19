using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WWT_Automation.Components
{
    public sealed class ToastComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly By _toast = By.CssSelector("div[class='toast toast-success']");

        public ToastComponent(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;   
        }

        public void WaitForToastToAppear(TimeSpan? timeout = null)
        {
            var localWait = timeout.HasValue ? new WebDriverWait(_driver, timeout.Value) : _wait;
            localWait.Until(d => d.FindElements(_toast).Any(t => t.Displayed));
        }

        public void WaitForToastToDisappear(TimeSpan? timeout = null)
        {
            var localWait = timeout.HasValue ? new WebDriverWait(_driver, timeout.Value) : _wait;
            localWait.Until(d => !d.FindElements(_toast).Any(t => t.Displayed));
        }
    }
}
