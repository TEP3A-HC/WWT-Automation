using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWT_Automation.Components
{
    public sealed class TableComponent
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By _root;               // table container
        private readonly By _headerCells;        // th under thead
        private readonly By _rows;               // tr under tbody
        private readonly By _cells;              // td under tr

        public TableComponent(IWebDriver driver, WebDriverWait wait, By root, By? headerCells = null, By? rows = null, By? cells = null)
        {
            _driver = driver;
            _wait = wait;
            _root = root;
            _headerCells = headerCells ?? By.CssSelector("thead tr th");
            _rows = rows ?? By.CssSelector("tbody tr");
            _cells = cells ?? By.CssSelector("td");
        }

        public int GetIndexOfColumn(string columnName)
        {
            var columns = _wait.Until(d => d.FindElement(_headerCells));

            // Locate all header <th> elements inside the table head
            var headers = _driver.FindElements(By.CssSelector("thead tr th"));

            for (int i = 0; i < headers.Count; i++)
            {
                var headerText = headers[i].Text.Trim();

                if (string.Equals(headerText, columnName, StringComparison.OrdinalIgnoreCase))
                {
                    // Return as 1-based index (common for XPath/table operations)
                    return i + 1;
                }
            }

            throw new NoSuchElementException($"Column with name '{columnName}' was not found in the table header.");
        }

    }
}
