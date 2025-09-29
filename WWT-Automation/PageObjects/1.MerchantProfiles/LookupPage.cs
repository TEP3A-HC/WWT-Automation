using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WWT_Automation.Components;

namespace WWT_Automation.PageObjects._1.MerchantProfiles
{
    public class LookupPage : BasePage
    {
        public DropdownComponent<LookupPage> AccountManagersDropdown { get; }
        public DropdownComponent<LookupPage> AgentsDropdown { get; }
        public DropdownComponent<LookupPage> AccountStatusesDropdown { get; }
        public DropdownComponent<LookupPage> MerchantStatusesDropdown { get; }
        public TableComponent Table { get; }
        public PaginationComponent Pagination { get; }

        public LookupPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
            AccountManagersDropdown = new DropdownComponent<LookupPage>(Driver, Wait, _accountManagerDropdown, _availableAccountManagers, this);
            AgentsDropdown = new DropdownComponent<LookupPage>(Driver, Wait, _agentDropdown, _availableAgents, this);
            AccountStatusesDropdown = new DropdownComponent<LookupPage>(Driver, Wait, _accountStatusDropdown, _availableAccountStatuses, this);
            MerchantStatusesDropdown = new DropdownComponent<LookupPage>(Driver, Wait, _merchantStatusDropdown, _availableMerchantStatuses, this);
            Table = new TableComponent(Driver, Wait, _tableRoot);
            Pagination = new PaginationComponent(Driver, Wait);
        }

        private readonly By _searchTextField = By.CssSelector("[ng-model='vm.Search']");

        private readonly By _accountManagerDropdown = By.CssSelector("md-select[ng-model='vm.AccountManagerSearch']");
        private readonly By _availableAccountManagers = By.CssSelector("md-select-menu[role='presentation'][class='_md md-overflow'] md-content md-option[ng-repeat='accountManager in vm.accountManagers']");

        private readonly By _agentDropdown = By.CssSelector("md-select[ng-model='vm.AgentSearch']");
        private readonly By _availableAgents = By.CssSelector("md-select-menu[role='presentation'][class='_md md-overflow'] md-content md-option[ng-repeat='a in vm.agents']");

        private readonly By _accountStatusDropdown = By.CssSelector("md-select[ng-model='vm.MasterStatusSearch']");
        private readonly By _availableAccountStatuses = By.CssSelector("md-select-menu[role='presentation'][class='_md'] md-content md-option[ng-repeat='masterStatus in vm.MerchantMasterStatus']");

        private readonly By _merchantStatusDropdown = By.CssSelector("md-select[ng-model='vm.ReviewStatusSearch']");
        private readonly By _availableMerchantStatuses = By.CssSelector("md-select-menu[role='presentation'][class='_md'] md-content md-option[ng-repeat='reviewStatus in vm.MerchantReviewStatus']");

        private readonly By _searchButton = By.CssSelector("button[type='button'][ng-click='vm.SearchMerchant()']");
        private readonly By _clearButton = By.CssSelector("button[type='button'][ng-click='vm.ClearSearch()']");

        private readonly By _tableRoot = By.CssSelector("table[id='printMerchantTable']");

        public LookupPage TypeInsideSearchField(string text)
        {
            EnterText(_searchTextField, text);
            return this;
        }

        public LookupPage ClickOnSearchButton()
        {
            Click(_searchButton);
            return this;
        }

        public LookupPage ClickOnClearButton()
        {
            Click(_clearButton);
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

        public MerchantPage ClickOnFirstMerchant()
        {
            WaitForPopupMessageToDisappear();
            Table.Cell(1, 1).Click();
            return new MerchantPage(Driver, Wait);
        }

        public MerchantPage OpenRandomMerchantFromResults()
        {
            Table.ClickOnRandomRow();
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("merchantDetailsEditBtn")));

            return new MerchantPage(Driver, Wait);
        }
    }
}
