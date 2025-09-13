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
        public void SignIn_ValidCredentials_SuccessfulSignIn()
        {
            driver.Url = "https://apadmintool.zero21.eu/";
            driver.FindElement(By.Id("UserName")).SendKeys("SuperAdmin");
            driver.FindElement(By.Id("Password")).SendKeys("T21kyytt$LVP#");

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("topnavForm")));
        }

        [Test]
        public void SignIn_InvalidPassword_ErrorMessage()
        {
            driver.Url = "https://apadmintool.zero21.eu/";
            driver.FindElement(By.Id("UserName")).SendKeys("SuperAdmin");
            driver.FindElement(By.Id("Password")).SendKeys("T21kyytt$LVP#T21kyytt$LVP#");

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//li[normalize-space()='Invalid credentials.']")));
        }

        [Test]
        public void SignIn_InvalidUsername_ErrorMessage()
        {
            driver.Url = "https://apadmintool.zero21.eu/";
            driver.FindElement(By.Id("UserName")).SendKeys("NonExistingUser");
            driver.FindElement(By.Id("Password")).SendKeys("T21kyytt$LVP#");

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//li[normalize-space()=\"User doesn't exist or user is not active.\"])")));
        }

        [Test]
        public void SignIn_ValidCredentialsWith2FA_6DigitScreenAppears()
        {
            driver.Url = "https://apadmintool.zero21.eu/";
            driver.FindElement(By.Id("UserName")).SendKeys("testAdmin");
            driver.FindElement(By.Id("Password")).SendKeys("testAdmin1");

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Code")));
        }

        /*
         TODO
        - ForgotPassword case
        - Complete 2FA is possible
         */

        [TearDown]
        public void Dispose()
        {
            driver.Quit();
        }
    }
}
