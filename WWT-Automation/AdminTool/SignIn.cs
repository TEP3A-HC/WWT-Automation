using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace WWT_Automation.AdminTool
{
    public class SignIn : IDisposable
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            
        }

        [Test]
        public void SuccessfulSignIn()
        {
            driver.Url = "https://apadmintool.zero21.eu/";
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
