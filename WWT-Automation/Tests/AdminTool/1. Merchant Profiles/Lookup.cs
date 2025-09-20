using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using WWT_Automation.PageObjects;
using WWT_Automation.PageObjects._1.MerchantProfiles;

namespace WWT_Automation.Tests.AdminTool._1._Merchant_Profiles
{
    public class Lookup : BaseTest
    {
        [Test]
        public void MerchantProfilesLookup_FilterMerchants_DisplayFilteredMerchants()
        {
            Driver.Navigate().GoToUrl("https://apadmintool.zero21.eu/");

            var merchantLookupPage = new SignInPage(Driver, Wait)
                .EnterUsername("SuperAdmin")
                .EnterPassword("T21kyytt$LVP#")
                .ClickSignIn()
                .ClickOnMerchantProfiles()
                .ClickOnLookup();

            var accountManagers = merchantLookupPage.AccountManagersDropdown.Open().GetOptions();
            var indexOfChosenAccountManager = PickRandomIWebElement(accountManagers);
            var selectedAccountManagerFullName = accountManagers[indexOfChosenAccountManager].Text;
            merchantLookupPage.AccountManagersDropdown.ClickByIndex(indexOfChosenAccountManager);

            merchantLookupPage.ClickOnSearchButton().WaitForPopupMessageToDisappear();

            LookupPage lookupPage = new LookupPage(Driver, Wait);
            var columnIndex = lookupPage.GetIndexPositionByColumnName("Account Manager");
            var rows = lookupPage.Table.Rows();

            for (int i = 0; i < rows.Count; i++)
            {
                IWebElement? row = rows[i];
                Assert.AreSame(lookupPage.Table.Cell(i, indexOfChosenAccountManager).Text, selectedAccountManagerFullName);
            }

        }

        private int PickRandomIWebElement(IList<IWebElement> accountManagers)
        {
            int startIndex = accountManagers.Count > 0 && accountManagers[0].Text.Trim().Equals("All", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
            var random = new Random();
            return random.Next(startIndex, accountManagers.Count);

        }
    }
}
