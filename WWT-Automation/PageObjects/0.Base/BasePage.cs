using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

public abstract class BasePage
{
    protected IWebDriver Driver { get; }
    protected WebDriverWait Wait { get; }

    protected BasePage(IWebDriver driver, WebDriverWait wait)
    {
        Driver = driver;
        Wait = wait;
    }

    protected IWebElement El(By by) => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
    protected IWebElement Visible(By by) => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
    protected IWebElement Clickable(By by) => Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));

    protected void EnterText(By by, string text, bool clear = true)
    {
        var e = Visible(by);
        if (clear) e.Clear();
        e.SendKeys(text);
    }

    protected void Click(By by) => Clickable(by).Click();
}
