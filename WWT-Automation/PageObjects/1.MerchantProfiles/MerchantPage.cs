using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WWT_Automation.Components;

namespace WWT_Automation.PageObjects._1.MerchantProfiles
{
    public class MerchantPage : BasePage
    {
        private readonly By _accountManager = By.CssSelector("div table tbody tr td strong[ng-show*=\"AccountManager\"][aria-hidden='false']");
        private readonly By _agent = By.CssSelector("div table tbody tr td strong[ng-show*=\"Agent\"][aria-hidden='false']");
        private readonly By _complianceOfficer = By.CssSelector("div table tbody tr td strong[ng-show*=\"ComplianceOfficer\"][aria-hidden='false']");
        private readonly By _fraudWatch = By.CssSelector("td[class='padding-sm'] span[aria-hidden='false']");
        private readonly By _fraudWatchReason = By.CssSelector("div table tbody tr[ng-show='vm.merchant.IsFraudWatch'] td strong");

        private readonly By _accountManagerDropdown = By.CssSelector("md-select[ng-model='form.AccountManagerId']");
        private readonly By _availableAccountManagers = By.CssSelector("md-select-menu md-option[ng-repeat='am in accountManagers']");

        private readonly By _agentDropdown = By.CssSelector("md-select[ng-model='form.AgentId']");
        private readonly By _availableAgents = By.CssSelector("md-select-menu md-content md-option[ng-repeat='a in agents']");

        private readonly By _complianceOfficerDropdown = By.CssSelector("md-select[ng-model='form.ComplianceOfficerId']");
        private readonly By _availableComplianceOfficer = By.CssSelector("md-select-menu md-content md-option[ng-repeat='co in complianceOfficers']");

        private readonly By _fraudWatchCheckbox = By.CssSelector("md-checkbox[ng-model='form.IsFraudWatch']");

        private readonly By _fraudWatchDropdown = By.CssSelector("md-input-container md-select[name='fraudwatch']");
        private readonly By _availableFraudWatchReasonsDropdown = By.CssSelector("md-select-menu md-content md-option[ng-repeat='r in fraudWatchReasons']");

        private readonly By _editButton = By.Id("merchantDetailsEditBtn");
        private readonly By _saveButton = By.Id("merchantSummarySaveBtn");

        public DropdownComponent AccountManagersDropdown { get; }
        public DropdownComponent AgentsDropdown { get; }
        public DropdownComponent ComplianceOfficerDropdown { get; }
        public DropdownComponent FraudWatchDropdown { get; }
        public ToastComponent ToastComponent { get; }

        public MerchantPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
            AccountManagersDropdown = new DropdownComponent(Driver, Wait, _accountManagerDropdown, _availableAccountManagers);
            AgentsDropdown = new DropdownComponent(Driver, Wait, _agentDropdown, _availableAgents);
            ComplianceOfficerDropdown = new DropdownComponent(Driver, Wait, _complianceOfficerDropdown, _availableComplianceOfficer);
            FraudWatchDropdown = new DropdownComponent(Driver, Wait, _fraudWatchDropdown, _availableFraudWatchReasonsDropdown);
            ToastComponent = new ToastComponent(Driver, Wait);
        }

        public string GetAccountManagerName()
        {
            return GetText(_accountManager);
        }

        public string GetAgentName()
        {
            return GetText(_agent);
        }

        public string GetComplianceOfficerName()
        {
            return GetText(_complianceOfficer);
        }

        public string GetFraudWatch()
        {
            return GetText(_fraudWatch);
        }

        public string GetFraudWatchReason()
        {
            if (IsDisplayed(_fraudWatchReason))
            {
                return GetText(_fraudWatchReason);
            }
            else
            {
                return string.Empty;
            }
        }

        public MerchantPage ClickOnEditButton()
        {
            Click(_editButton);
            return this;
        }

        public bool IsMerchantOnFraudWatch()
        {
            return IsChecked(_fraudWatchCheckbox);
        }

        public MerchantPage ClickOnFraudWatchCheckbox()
        {
            Click(_fraudWatchCheckbox);
            return this;
        }

        public bool IsFraudWatchDropdownDisplayed()
        {
            return IsDisplayed(_fraudWatchDropdown);
        }

        public MerchantPage SaveChanges()
        {
            Click(_saveButton);
            return this;
        }
    }
}
