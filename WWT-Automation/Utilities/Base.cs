using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Communication;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using WebDriverManager.DriverConfigs.Impl;
using System;
using WWT_Automation.Config;

namespace WWT_Automation.Utilities
{
    public class Base : IDisposable
    {
        public IWebDriver driver;
        public WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            string browserName = TestConfig.Current.Selenium.Browser;
            InitBrowser(browserName);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

        }

        public void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "FireFox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver();
                    break;
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver();
                    break;
            }
        }

        [TearDown]
        public void Dispose()
        {
            driver.Quit();
        }
    }
}
