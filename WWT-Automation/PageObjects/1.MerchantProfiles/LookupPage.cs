using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WWT_Automation.Components;

namespace WWT_Automation.PageObjects._1.MerchantProfiles
{
    public class LookupPage : BasePage
    {
        public DropdownComponent AccountManagersDropdown { get; }
        public DropdownComponent AgentsDropdown { get; }
        public DropdownComponent MerchantStatusesDropdown { get; }
        public TableComponent Table { get; }

        public LookupPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
            AccountManagersDropdown = new DropdownComponent(Driver, Wait, _accountManagerDropdown, _availableAccountManagers);
            AgentsDropdown = new DropdownComponent(Driver, Wait, _agentDropdown, _availableAgents);
            MerchantStatusesDropdown = new DropdownComponent(Driver, Wait, _merchantStatusDropdown, _availableMerchantStatuses);
            Table = new TableComponent(Driver, Wait, _tableRoot);
        }

        private readonly By _accountManagerDropdown = By.CssSelector("md-select[ng-model='vm.AccountManagerSearch']");
        private readonly By _availableAccountManagers = By.CssSelector("md-select-menu[role='presentation'][class='_md md-overflow'] md-content md-option[ng-repeat='accountManager in vm.accountManagers']");

        private readonly By _agentDropdown = By.CssSelector("md-select[ng-model='vm.AgentSearch']");
        private readonly By _availableAgents = By.CssSelector("md-select-menu[role='presentation'][class='_md md-overflow'] md-content md-option[ng-repeat='a in vm.agents']");

        private readonly By _merchantStatusDropdown = By.CssSelector("md-select[ng-model='vm.ReviewStatusSearch']");
        private readonly By _availableMerchantStatuses = By.CssSelector("md-select-menu[role='presentation'][class='_md'] md-content md-option[ng-repeat='reviewStatus in vm.MerchantReviewStatus']");

        private readonly By _searchButton = By.CssSelector("button[type='button'][ng-click='vm.SearchMerchant()']");

        private readonly By _tableRoot = By.CssSelector("table[id='printTable']");

        public LookupPage ClickOnSearchButton()
        {
            Click(_searchButton);
            return this;
        }

        public LookupPage WaitForPopupMessageToDisappear()
        {
            Toast.WaitForToastToAppear();
            Toast.WaitForToastToDisappear();
            return this;
        }

        public int GetIndexPositionByColumnName(string columnName)
        {
            return Table.GetIndexOfColumn(columnName);
        }
    }
}
