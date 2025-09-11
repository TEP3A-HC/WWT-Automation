using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Diagnostics;
using WebDriverManager.DriverConfigs.Impl;

namespace WWT_Automation.AdminTool
{
    public class SignIn : IDisposable
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

        }

        [Test]
        public void SuccessfulSignIn()
        {
            driver.Url = "https://apadmintool.zero21.eu/";
            driver.FindElement(By.Id("UserName")).SendKeys("SuperAdmin");
            driver.FindElement(By.Id("Password")).SendKeys("T21kyytt$LVP#");

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("topnavForm")));
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
