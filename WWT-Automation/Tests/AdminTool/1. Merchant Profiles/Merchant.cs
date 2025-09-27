using WWT_Automation.PageObjects;

namespace WWT_Automation.Tests.AdminTool._1._Merchant_Profiles
{
    public class Merchant : BaseTest
    {
        [Test]
        public void Merchant_UpdateAccountManagerAgentAndComplianceOfficer_MerchantIsUpdated()
        {
            Driver.Navigate().GoToUrl("https://apadmintool.zero21.eu/");

            var merchantPage = new SignInPage(Driver, Wait)
                .EnterUsername("SuperAdmin")
                .EnterPassword("T21kyytt$LVP#")
                .ClickSignIn()
                .ClickOnMerchantProfiles()
                .ClickOnLookup()
                .ClickOnFirstMerchant();

            var initialAccountManagerFullName = merchantPage.GetAccountManagerName();
            var initialAgentFullName = merchantPage.GetAgentName();
            var initialComplianceOfficerFullName = merchantPage.GetComplianceOfficerName();
            var initialFraudWatch = merchantPage.GetFraudWatch();
            var initialFraudWatchReason = merchantPage.GetFraudWatchReason();
            var fraudWatchReason = "";

            merchantPage.ClickOnEditButton();

            var newAccountManager = merchantPage.AccountManagersDropdown.Open().PickAnyValueExcept(initialAccountManagerFullName);
            var newAgent = merchantPage.AgentsDropdown.Open().PickAnyValueExcept(initialAgentFullName);
            var newComplianceOfficer = merchantPage.ComplianceOfficerDropdown.Open().PickAnyValueExcept(initialComplianceOfficerFullName);

            if (initialFraudWatch == "Yes")
            {
                merchantPage.ClickOnFraudWatchCheckbox();
                Assert.That(merchantPage.IsFraudWatchDropdownDisplayed(), Is.False);
            }
            else
            {
                merchantPage.ClickOnFraudWatchCheckbox();
                fraudWatchReason = merchantPage.FraudWatchDropdown.Open().ClickRandomOption();
            }

            merchantPage.SaveChanges();
            merchantPage.ToastComponent.WaitForPopupMessageToDisappear();

            Assert.That(initialAccountManagerFullName, Is.Not.EqualTo(newAccountManager));
            Assert.That(initialAgentFullName, Is.Not.EqualTo(newAgent));
            Assert.That(initialComplianceOfficerFullName, Is.Not.EqualTo(newComplianceOfficer));

            Assert.That(newAccountManager, Is.EqualTo(merchantPage.GetAccountManagerName()));
            Assert.That(newAgent, Is.EqualTo(merchantPage.GetAgentName()));
            Assert.That(newComplianceOfficer, Is.EqualTo(merchantPage.GetComplianceOfficerName()));

            Assert.That(initialFraudWatch, Is.Not.EqualTo(merchantPage.GetFraudWatch()));
            if (initialFraudWatchReason == string.Empty)
            {
                Assert.That(merchantPage.GetFraudWatchReason(), Is.EqualTo(fraudWatchReason));
            }
        }

    }
}
