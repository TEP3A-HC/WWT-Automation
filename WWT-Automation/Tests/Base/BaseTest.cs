using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WWT_Automation.Config;

[TestFixture]
public abstract class BaseTest
{
    protected IWebDriver Driver { get; private set; }
    protected WebDriverWait Wait { get; private set; }

    // Central place to tweak all timeouts
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(15);
    private static readonly TimeSpan DefaultPolling = TimeSpan.FromMilliseconds(250);

    [SetUp]
    public void SetUp()
    {
        var browserName = (TestConfig.Current?.Selenium?.Browser ?? "chrome").Trim().ToLowerInvariant();
        Driver = CreateDriver(browserName);

        // Window & timeouts (explicit waits only)
        Driver.Manage().Window.Maximize();
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;                   // important
        Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
        Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(30);

        // Shared explicit wait
        Wait = new WebDriverWait(new SystemClock(), Driver, DefaultTimeout, DefaultPolling);
        Wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
    }

    private static IWebDriver CreateDriver(string browser)
    {
        // Selenium Manager resolves drivers automatically (no WebDriverManager needed)
        switch (browser)
        {
            case "firefox":
                var ffOpts = new FirefoxOptions();
                // ffOpts.AddArgument("-headless");  // enable on CI if needed
                return new FirefoxDriver(ffOpts);

            case "edge":
                var edgeOpts = new EdgeOptions();
                // edgeOpts.AddArgument("--headless=new");
                return new EdgeDriver(edgeOpts);

            case "chrome":
            default:
                var chOpts = new ChromeOptions();
                // chOpts.AddArgument("--headless=new"); // enable on CI
                chOpts.AddArgument("--start-maximized");
                chOpts.AddArgument("--disable-gpu");
                chOpts.AddArgument("--no-sandbox");
                chOpts.AddArgument("--disable-dev-shm-usage");
                return new ChromeDriver(chOpts);
        }
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            Driver?.Quit();
        }
        catch
        {
            // swallow cleanup errors to not mask test results
        }
        finally
        {
            Driver?.Dispose();
            Driver = null;
            Wait = null;
        }
    }

    // Small helpers you can reuse in tests or BasePage
    protected IWebElement WaitVisible(By locator) =>
        Wait.Until(ExpectedConditions.ElementIsVisible(locator));

    protected IWebElement WaitClickable(By locator) =>
        Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
}
