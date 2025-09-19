using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WWT_Automation.Components
{
    public sealed class DropdownComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly By _dropdownLocator;
        private readonly By _optionsLocator;

        public DropdownComponent(IWebDriver driver, WebDriverWait wait, By dropdownLocator, By optionsLocator)
        {
            _driver = driver;
            _wait = wait;
            _dropdownLocator = dropdownLocator;
            _optionsLocator = optionsLocator;
        }

        public DropdownComponent Open()
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_dropdownLocator)).Click();

            // Wait until at least one option is visible
            _wait.Until(d =>
            {
                var opts = d.FindElements(_optionsLocator);
                return opts.Count > 0 && opts.Any(o => o.Displayed) ? opts : null;
            });
            return this;
        }

        public IList<IWebElement> GetOptions()
        {
            return _wait.Until(d => d.FindElements(_optionsLocator)).ToList();
        }

        public DropdownComponent ClickByIndex(int index)
        {
            var options = GetOptions();
            if (index < 0 || index >= options.Count)
                throw new ArgumentOutOfRangeException(nameof(index),
                    $"Index {index} outside range 0..{options.Count - 1}");

            var target = options[index];
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'})", target);
            target.Click();
            return this;
        }
    }
}
