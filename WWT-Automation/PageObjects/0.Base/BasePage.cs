using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WWT_Automation.Components;

public abstract class BasePage
{
    protected IWebDriver Driver { get; }
    protected WebDriverWait Wait { get; }
    protected ToastComponent Toast { get; }

    protected BasePage(IWebDriver driver, WebDriverWait wait)
    {
        Driver = driver;
        Wait = wait;
        Toast = new ToastComponent(driver, wait);
    }

    protected IWebElement El(By by) => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
    protected IWebElement Visible(By by) => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
    protected IWebElement Clickable(By by) => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
    protected IReadOnlyCollection<IWebElement> Els(By by) => Wait.Until(driver => driver.FindElements(by));

    protected IReadOnlyCollection<IWebElement> VisibleEls(By by) =>
        Wait.Until(driver =>
        {
            var elements = driver.FindElements(by);
            return elements.Any(e => e.Displayed) ? elements : null;
        });

    protected void EnterText(By by, string text, bool clear = true)
    {
        var e = Visible(by);
        if (clear) e.Clear();
        e.SendKeys(text);
    }

    protected void Click(By by) => Clickable(by).Click();
}
