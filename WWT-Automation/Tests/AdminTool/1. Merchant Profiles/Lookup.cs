using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using WWT_Automation.PageObjects;
using WWT_Automation.PageObjects._1.MerchantProfiles;

namespace WWT_Automation.Tests.AdminTool._1._Merchant_Profiles
{
    public class Lookup : BaseTest
    {
        private static readonly Random _random = new Random();

        [Test]
        public void MerchantProfilesLookup_FilterMerchants_DisplayFilteredMerchants()
        {
            Driver.Navigate().GoToUrl("https://apadmintool.zero21.eu/");

            var lookupPage = new SignInPage(Driver, Wait)
                .EnterUsername("SuperAdmin")
                .EnterPassword("T21kyytt$LVP#")
                .ClickSignIn()
                .ClickOnMerchantProfiles()
                .ClickOnLookup();

            var accountManagers = lookupPage.AccountManagersDropdown.Open().GetOptions();
            var chosenIndex = PickRandomIndex(accountManagers);
            var selectedAccountManagerFullName = accountManagers[chosenIndex].Text;
            lookupPage.AccountManagersDropdown.ClickByIndex(chosenIndex);
            lookupPage.ClickOnSearchButton().WaitForPopupMessageToDisappear();

            var columnIndex = lookupPage.GetIndexPositionByColumnName("Account Manager");
            var accountManagerRows = lookupPage.Table.Rows();
            Assert.That(accountManagerRows.Count, Is.GreaterThan(0), "No merchants returned after filtering.");

            for (int i = 1; i <= accountManagerRows.Count; i++)
            {
                var accountManagerName = lookupPage.Table.GetCellText(i, columnIndex);
                Assert.That(selectedAccountManagerFullName, Is.EqualTo(accountManagerName));
            }

            lookupPage.ClickOnClearButton().WaitForPopupMessageToDisappear();

            var agents = lookupPage.AgentsDropdown.Open().GetOptions();
            var chosenAgentIndex = PickRandomIndex(agents);
            var selectedAgentFullName = agents[chosenAgentIndex].Text;
            lookupPage.AgentsDropdown.ClickByIndex(chosenAgentIndex);
            lookupPage.ClickOnSearchButton().WaitForPopupMessageToDisappear();

            var columnAgentIndex = lookupPage.GetIndexPositionByColumnName("Agent");
            var agentRows = lookupPage.Table.Rows();
            Assert.That(agentRows.Count, Is.GreaterThan(0), "No merchants returned after filtering.");

            for (int i = 1; i <= agentRows.Count; i++)
            {
                var agentName = lookupPage.Table.GetCellText(i, columnAgentIndex);
                Assert.That(selectedAgentFullName, Is.EqualTo(agentName));
            }

            lookupPage.ClickOnClearButton().WaitForPopupMessageToDisappear();

            var merchantStatuses = lookupPage.MerchantStatusesDropdown.Open().GetOptions();
            var chosenMerchantStatusIndex = PickRandomIndex(merchantStatuses);
            var selectedMerchantStatus = merchantStatuses[chosenMerchantStatusIndex].Text;
            lookupPage.MerchantStatusesDropdown.ClickByIndex(chosenMerchantStatusIndex);
            lookupPage.ClickOnSearchButton().WaitForPopupMessageToDisappear();

            var columnMerchantStatusIndex = lookupPage.GetIndexPositionByColumnName("Merchant Status");
            var merchantStatusRows = lookupPage.Table.Rows();
            Assert.That(merchantStatusRows.Count, Is.GreaterThan(0), "No merchants returned after filtering.");

            for (int i = 1; i <= merchantStatusRows.Count; i++)
            {
                var merchantStatusName = lookupPage.Table.GetCellText(i, columnMerchantStatusIndex);
                Assert.That(selectedMerchantStatus, Is.EqualTo(merchantStatusName));
            }
        }

        private int PickRandomIndex(IList<IWebElement> accountManagers)
        {
            int startIndex = accountManagers.Count > 0 && accountManagers[0].Text.Trim().Equals("All", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
            return _random.Next(startIndex, accountManagers.Count);

        }
    }
}
