using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace WWT_Automation.Components
{
    public sealed class DropdownComponent<TOwner> where TOwner : BasePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly By _dropdownLocator;
        private readonly By _optionsLocator;
        private readonly TOwner _owner;
        private static readonly Random _random = new Random();

        public string LastSelectedText { get; private set; } = string.Empty;

        public DropdownComponent(IWebDriver driver, WebDriverWait wait, By dropdownLocator, By optionsLocator, TOwner owner)
        {
            _driver = driver;
            _wait = wait;
            _dropdownLocator = dropdownLocator;
            _optionsLocator = optionsLocator;
            _owner = owner;
        }

        public DropdownComponent<TOwner> Open()
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

        private static string LabelOf(IWebElement option)
        {
            try { return option.FindElement(By.CssSelector(".md-text")).Text.Trim(); }
            catch { return (option.Text ?? "").Trim(); }
        }

        public DropdownComponent<TOwner> ClickByIndex(int index)
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

        public DropdownComponent<TOwner> ClickByText(string text, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            var options = GetOptions();
            var target = options.FirstOrDefault(o => o.Text.Trim().Equals(text.Trim(), comparison));

            if (target == null)
                throw new NoSuchElementException(
                    $"No dropdown option with text '{text}' was found.");

            ((IJavaScriptExecutor)_driver).ExecuteScript(
                "arguments[0].scrollIntoView({block:'center'})", target);

            target.Click();
            return this;
        }

        public TOwner ClickRandomOption(out string selectedText, bool skipFirstIfPlaceholder = true)
        {
            var options = GetOptions();
            if (options.Count == 0)
                throw new InvalidOperationException("No dropdown options available.");

            int start = 0;
            if (skipFirstIfPlaceholder && options.Count > 0)
            {
                var firstText = options[0].Text.Trim();
                if (string.Equals(firstText, "[All]", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(firstText, "N/A", StringComparison.OrdinalIgnoreCase))
                {
                    start = 1;
                }
            }

            var pick = options[_random.Next(start, options.Count)];
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'})", pick);
            selectedText = LastSelectedText = LabelOf(pick);
            pick.Click();

            return _owner; // return the selected text
        }

        public string ClickRandomOption(bool skipFirstIfPlaceholder = true)
        {
            ClickRandomOption(out var selected, skipFirstIfPlaceholder);
            return selected;
        }

        public string PickAnyValueExcept(string excludedValue)
        {
            var options = GetOptions();
            if (options.Count == 0)
                throw new InvalidOperationException("No dropdown options available.");

            // Build candidate list excluding the provided value
            var candidates = options
                .Where(o => !string.Equals(o.FindElement(By.CssSelector(".md-text")).Text.Trim(), excludedValue.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (candidates.Count == 0)
                throw new InvalidOperationException($"No available options different from '{excludedValue}'.");

            // Randomly pick one
            var pick = candidates[_random.Next(candidates.Count)];

            // Scroll into view and click
            //((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'})", pick);
            pick.Click();

            return pick.FindElement(By.CssSelector(".md-text")).Text.Trim(); // return the newly selected value
        }
    }
}
