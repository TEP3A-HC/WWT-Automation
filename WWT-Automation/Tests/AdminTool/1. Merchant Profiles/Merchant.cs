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

            var accountManagerFullName = merchantPage.GetAccountManagerName();
            var agentFullName = merchantPage.GetAgentName();
            var complianceOfficerFullName = merchantPage.GetComplianceOfficerName();
            var fraudWatch = merchantPage.GetFraudWatch();
            var fraudWatchReason = merchantPage.GetFraudWatchReason();

            merchantPage.ClickOnEditButton();

            var availableAccountManagers = merchantPage.AccountManagersDropdown.Open().GetOptions();
            var newAccountManager = merchantPage.AccountManagersDropdown.ChooseNewDropdownValue(accountManagerFullName, availableAccountManagers);
            merchantPage.AccountManagersDropdown.ClickByText(newAccountManager);

            var availableAgents = merchantPage.AgentsDropdown.Open().GetOptions();
            var newAgent = merchantPage.AgentsDropdown.ChooseNewDropdownValue(agentFullName, availableAgents);
            merchantPage.AgentsDropdown.ClickByText(newAgent);

            var availableComplianceOfficers = merchantPage.ComplianceOfficerDropdown.Open().GetOptions();
            var newComplianceOfficer = merchantPage.ComplianceOfficerDropdown.ChooseNewDropdownValue(complianceOfficerFullName, availableComplianceOfficers);
            merchantPage.ComplianceOfficerDropdown.ClickByText(newComplianceOfficer);




        }

    }
}
