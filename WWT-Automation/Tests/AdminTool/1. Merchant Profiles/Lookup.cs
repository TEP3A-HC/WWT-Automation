using OpenQA.Selenium;
using WWT_Automation.PageObjects;

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

            #region Account managers
            var accountManager = lookupPage.AccountManagersDropdown.Open().ClickRandomOption();
            lookupPage.ClickOnSearchButton().WaitForPopupMessageToDisappear();

            var columnIndex = lookupPage.GetIndexPositionByColumnName("Account Manager");
            var accountManagerRows = lookupPage.Table.Rows();
            Assert.That(accountManagerRows.Count, Is.GreaterThan(0), "No merchants returned after filtering.");

            for (int i = 1; i <= accountManagerRows.Count; i++)
            {
                var accountManagerName = lookupPage.Table.GetCellText(i, columnIndex);
                Assert.That(accountManager, Is.EqualTo(accountManagerName));
            }
            #endregion

            lookupPage.ClickOnClearButton().WaitForPopupMessageToDisappear();

            #region Agent
            var agent = lookupPage.AgentsDropdown.Open().ClickRandomOption();
            lookupPage.ClickOnSearchButton().WaitForPopupMessageToDisappear();

            var columnAgentIndex = lookupPage.GetIndexPositionByColumnName("Agent");
            var agentRows = lookupPage.Table.Rows();
            Assert.That(agentRows.Count, Is.GreaterThan(0), "No merchants returned after filtering.");

            for (int i = 1; i <= agentRows.Count; i++)
            {
                var agentName = lookupPage.Table.GetCellText(i, columnAgentIndex);
                Assert.That(agent, Is.EqualTo(agentName));
            }
            #endregion

            lookupPage.ClickOnClearButton().WaitForPopupMessageToDisappear();

            #region Merchant status
            var merchantStatus = lookupPage.MerchantStatusesDropdown.Open().ClickRandomOption();
            lookupPage.ClickOnSearchButton().WaitForPopupMessageToDisappear();

            var columnMerchantStatusIndex = lookupPage.GetIndexPositionByColumnName("Merchant Status");
            var merchantStatusRows = lookupPage.Table.Rows();
            Assert.That(merchantStatusRows.Count, Is.GreaterThan(0), "No merchants returned after filtering.");

            for (int i = 1; i <= merchantStatusRows.Count; i++)
            {
                var merchantStatusName = lookupPage.Table.GetCellText(i, columnMerchantStatusIndex);
                Assert.That(merchantStatus, Is.EqualTo(merchantStatusName));
            }
            #endregion
        }

        [Test]
        public void MerchantProfilesLookup_FilterMerchantsByAccountStatus_DisplayFilteredMerchants()
        {
            Driver.Navigate().GoToUrl("https://apadmintool.zero21.eu/");

            string pickedAccountStatus;

            var lookupPage = new SignInPage(Driver, Wait)
                .EnterUsername("SuperAdmin")
                .EnterPassword("T21kyytt$LVP#")
                .ClickSignIn()
                .ClickOnMerchantProfiles()
                .ClickOnLookup()
                .AccountStatusesDropdown.Open()
                .ClickRandomOption(out pickedAccountStatus)
                .ClickOnSearchButton()
                .WaitForPopupMessageToDisappear()
                .OpenRandomMerchantFromResults();

            var accountStatus = lookupPage.GetAccountStatus();

            Assert.That(pickedAccountStatus, Is.EqualTo(accountStatus));
        }

        [Test]
        public void MerchantProfilesLookup_SearchForMerchant_MerchantIsDisplayed()
        {
            Driver.Navigate().GoToUrl("https://apadmintool.zero21.eu/");

            var lookupPage = new SignInPage(Driver, Wait)
                    .EnterUsername("SuperAdmin")
                    .EnterPassword("T21kyytt$LVP#")
                    .ClickSignIn()
                    .ClickOnMerchantProfiles()
                    .ClickOnLookup()
                    .TypeInsideSearchField("cbc151a")
                    .ClickOnSearchButton()
                    .WaitForPopupMessageToDisappear();

            #region Search by merchant code
            var filteredTableByMerchantCode = lookupPage.Table.Rows();
            Assert.That(filteredTableByMerchantCode.Count, Is.EqualTo(1));
            Assert.That(lookupPage.Table.GetCellText(1, 1), Is.EqualTo("mer_cbc151a")); 
            #endregion

            lookupPage.ClickOnClearButton().WaitForPopupMessageToDisappear();

            #region Search by company name
            lookupPage.TypeInsideSearchField("TestCompany").ClickOnSearchButton().WaitForPopupMessageToDisappear();
            var filteredTableByCompanyName = lookupPage.Table.Rows();

            Assert.That(filteredTableByCompanyName.Count.ToString(), Is.EqualTo(lookupPage.Pagination.GetRowsPerPageText()));
            for (int i = 1; i <= filteredTableByCompanyName.Count; i++)
            {
                var companyName = lookupPage.Table.GetCellText(i, 3);
                Assert.That(companyName, Is.EqualTo("TestCompany"));
            } 
            #endregion


        }
    }
}
