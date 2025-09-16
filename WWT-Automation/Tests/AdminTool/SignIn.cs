using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using WWT_Automation.PageObjects;

namespace WWT_Automation.Tests.AdminTool
{
    public class SignIn : BaseTest
    {

        [Test]
        public void SignIn_ValidCredentials_SuccessfulSignIn()
        {
            Driver.Navigate().GoToUrl("https://apadmintool.zero21.eu/");

            var main = new SignInPage(Driver, Wait)
                .EnterUsername("SuperAdmin")
                .EnterPassword("T21kyytt$LVP#")
                .ClickSignIn();

            Wait.Until(ExpectedConditions.ElementIsVisible(By.Name("topnavForm")));
        }

        [Test]
        public void SignIn_InvalidPassword_ErrorMessage()
        {
            Driver.Navigate().GoToUrl("https://apadmintool.zero21.eu/");

            var main = new SignInPage(Driver, Wait)
                .EnterUsername("SuperAdmin")
                .EnterPassword("T21kyytt$LVP#T21kyytt$LVP#")
                .ClickSignIn();

            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//li[normalize-space()='Invalid credentials.']")));
        }

        [Test]
        public void SignIn_InvalidUsername_ErrorMessage()
        {
            Driver.Navigate().GoToUrl("https://apadmintool.zero21.eu/");

            var main = new SignInPage(Driver, Wait)
                .EnterUsername("NonExistingUser")
                .EnterPassword("T21kyytt$LVP#")
                .ClickSignIn();

            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//li[normalize-space()=\"User doesn't exist or user is not active.\"])")));
        }

        [Test]
        public void SignIn_ValidCredentialsWith2FA_6DigitScreenAppears()
        {
            Driver.Navigate().GoToUrl("https://apadmintool.zero21.eu/");

            var main = new SignInPage(Driver, Wait)
                .EnterUsername("testAdmin")
                .EnterPassword("testAdmin1")
                .ClickSignIn();

            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Code")));
        }

        /*
         TODO
        - ForgotPassword case
        - Complete 2FA is possible
         */

        
    }
}
