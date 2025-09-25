using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWT_Automation.Components
{
    public class PaginationComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By _currentPage;
        private readonly By _rowsPerPage;
        private readonly By _nextPage;
        private readonly By _previousPage;
        private readonly By _lastPage;
        private readonly By _firstPage;
        private readonly By _displayedRowsStat;

        public PaginationComponent(
            IWebDriver driver,
            WebDriverWait wait,
            By? currentPage = null,
            By? rowsPerPage = null,
            By? nextPage = null,
            By? previousPage = null,
            By? lastPage = null,
            By? firstPage = null,
            By? displayedRowsStat = null)
        {
            _driver = driver;
            _wait = wait;
            _currentPage = currentPage ?? By.CssSelector("div[ng-if='$pagination.showPageSelect()'] md-select md-select-value span div");
            _rowsPerPage = rowsPerPage ?? By.CssSelector("div[ng-if='$pagination.limitOptions'] md-select md-select-value span div");
            _nextPage = nextPage ?? By.CssSelector("button[ng-click='$pagination.next()']");
            _previousPage = previousPage ?? By.CssSelector("button[ng-click='$pagination.previous()']");
            _lastPage = lastPage ?? By.CssSelector("button[ng-click='$pagination.last()']");
            _firstPage = firstPage ?? By.CssSelector("button[ng-click='$pagination.first()']");
            _displayedRowsStat = displayedRowsStat ?? By.CssSelector("div[class='buttons'] div");
        }

        public PaginationComponent NextPage()
        {
            Click(_nextPage);
            WaitForTableToReload();
            return this;
        }

        public PaginationComponent PreviousPage()
        {
            Click(_previousPage);
            WaitForTableToReload();
            return this;
        }

        public PaginationComponent FirstPage()
        {
            Click(_firstPage);
            WaitForTableToReload();
            return this;
        }

        public PaginationComponent LastPage()
        {
            Click(_lastPage);
            WaitForTableToReload();
            return this;
        }

        public string GetCurrentPageText() =>
            _wait.Until(d => d.FindElement(_currentPage)).Text.Trim();

        public string GetRowsPerPageText() =>
            _wait.Until(d => d.FindElement(_rowsPerPage)).Text.Trim();

        public string GetDisplayedRowsStat() =>
            _wait.Until(d => d.FindElement(_displayedRowsStat)).Text.Trim();

        private void Click(By locator)
        {
            var element = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
            element.Click();
        }

        private void WaitForTableToReload()
        {
            // Example: wait for "tbody" to refresh
            var table = _driver.FindElement(By.CssSelector("table tbody"));
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(table));
            _wait.Until(d => d.FindElements(By.CssSelector("table tbody tr")).Count > 0);
        }
    }
}
