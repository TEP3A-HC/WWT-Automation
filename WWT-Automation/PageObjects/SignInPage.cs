using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace WWT_Automation.PageObjects
{
    public class SignInPage : BasePage
    {
        public SignInPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        private readonly By _username = By.Id("UserName");
        private readonly By _password = By.Id("Password");
        private readonly By _submit = By.CssSelector("button[type='submit']");

        public SignInPage EnterUsername(string username)
        {
            EnterText(_username, username);
            return this;
        }
        public SignInPage EnterPassword(string password)
        {
            EnterText(_password, password);
            return this;
        }

        public MainPage ClickSignIn()
        {
            Click(_submit);
            return new MainPage(Driver, Wait);
        }

        public MainPage SignInAs(string username, string password) =>
            EnterUsername(username).EnterPassword(password).ClickSignIn();
    }
}
