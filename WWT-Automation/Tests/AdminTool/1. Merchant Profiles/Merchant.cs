using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var availableAccountManagers = merchantPage.AccountManagersDropdown.Open().GetOptions();
            var newAccountManager = merchantPage.AccountManagersDropdown.ChooseNewDropdownValue(initialAccountManagerFullName, availableAccountManagers);
            merchantPage.AccountManagersDropdown.ClickByText(newAccountManager);

            var availableAgents = merchantPage.AgentsDropdown.Open().GetOptions();
            var newAgent = merchantPage.AgentsDropdown.ChooseNewDropdownValue(initialAgentFullName, availableAgents);
            merchantPage.AgentsDropdown.ClickByText(newAgent);

            var availableComplianceOfficers = merchantPage.ComplianceOfficerDropdown.Open().GetOptions();
            var newComplianceOfficer = merchantPage.ComplianceOfficerDropdown.ChooseNewDropdownValue(initialComplianceOfficerFullName, availableComplianceOfficers);
            merchantPage.ComplianceOfficerDropdown.ClickByText(newComplianceOfficer);

            if (merchantPage.IsMerchantOnFraudWatch())
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
