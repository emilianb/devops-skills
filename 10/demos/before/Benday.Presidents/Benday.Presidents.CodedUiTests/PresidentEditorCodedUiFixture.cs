using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace Benday.Presidents.CodedUiTests
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class PresidentEditorCodedUiFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _Browser = null;
        }

        private BrowserWindow _Browser;

        [TestMethod]
        [TestCategory("CodedUi")]
        public void VerifyChesterArthurInIE()
        {
            // this speeds up playback dramatically
            /*
            Playback.PlaybackSettings.WaitForReadyLevel =
                WaitForReadyLevel.Disabled;
                */

            // this records screenshots
            Playback.PlaybackSettings.LoggerOverrideState = 
                HtmlLoggerState.AllActionSnapshot;
            
            _Browser = RunApp();

            var firstName = "Chester";
            var lastName = "Arthur";
            
            NavigateToSearch();
            RunSearch(firstName, lastName);
            VerifySearchResults(firstName, lastName);

            Playback.Wait(2000);
        }
        
        private void NavigateToSearch()
        {
            HtmlHyperlink link = GetHtmlHyperlink("search-nav-button");

            ExpandNavMenu();
            
            try
            {
                Mouse.Click(link);
            }
            catch(FailedToPerformActionOnHiddenControlException)
            {
                ExpandNavMenu();

                link.EnsureClickable();

                Mouse.Click(link);
            }
        }

        private bool IsClickable(UITestControl control)
        {
            bool found = control.TryFind();

            if (found == false)
            {
                return false;
            }

            Point point;
            
            return control.TryGetClickablePoint(out point);
        }

        private void ExpandNavMenu()
        {
            var temp = new HtmlButton(_Browser);

            temp.SearchProperties.Add(
                HtmlButton.PropertyNames.Class,
                "navbar-toggle");

            if (IsClickable(temp) == true)
            {
                TestContext.WriteLine("Navbar point is clickable");
                Mouse.Click(temp);
                Playback.Wait(500);
            }
            else
            {
                TestContext.WriteLine("Navbar point is not clickable");
            }
        }

        private HtmlHyperlink GetHtmlHyperlink(string id)
        {
            var link = new HtmlHyperlink(_Browser);

            link.SearchProperties.Add(
                HtmlHyperlink.PropertyNames.Id,
                id);

            return link;
        }

        private HtmlEdit GetTextbox(string id)
        {
            var temp = new HtmlEdit(_Browser);

            temp.SearchProperties.Add(
                HtmlHyperlink.PropertyNames.Id,
                id);

            return temp;
        }

        private HtmlTable GetTable(string id)
        {
            var temp = new HtmlTable(_Browser);

            temp.SearchProperties.Add(
                HtmlTable.PropertyNames.Id,
                id);

            return temp;
        }

        private HtmlInputButton GetInputButton(string id)
        {
            var temp = new HtmlInputButton(_Browser);

            temp.SearchProperties.Add(
                HtmlHyperlink.PropertyNames.Id,
                id);

            return temp;
        }

        void RunSearch(string firstName, string lastName)
        {
            var firstNameSearchBox = GetTextbox("FirstName");
            var lastNameSearchBox = GetTextbox("LastName");
            var searchButton = GetInputButton("search-button");

            firstNameSearchBox.Text = firstName;
            lastNameSearchBox.Text = lastName;

            Mouse.Click(searchButton);            
        }

        private void VerifySearchResults(string firstName, string lastName)
        {
            var table = GetTable("search-results");

            TestContext.WriteLine("Result table row count: {0}", table.RowCount);

            Assert.AreEqual<int>(2, table.RowCount,
                "No matching rows in result table.");

            VerifyResultRow(table, firstName, lastName);
        }

        private void VerifyResultRow(
            HtmlTable table, string firstName, string lastName)
        {
            var firstNonHeaderRow = table.Rows[1];

            var firstNameCell = GetCell(table, 1, 0);
            var lastNameCell = GetCell(table, 1, 1);
            
            Assert.AreEqual<string>(firstName, firstNameCell.Value.Trim(), 
                "Wrong first name value in cell.");

            Assert.AreEqual<string>(lastName, lastNameCell.Value.Trim(), 
                "Wrong last name value in cell.");

            var linksCell = GetCell(table, 1, 2);

            var detailsLink = GetHtmlHyperlinkByText(linksCell, "Details");

            Mouse.Click(detailsLink);
        }

        private HtmlHyperlink GetHtmlHyperlinkByText(UITestControl parent, string linkText)
        {
            var temp = new HtmlHyperlink(parent);

            temp.SearchProperties.Add(
                HtmlHyperlink.PropertyNames.InnerText,
                linkText);

            return temp;
        }

        private HtmlCell GetCell(HtmlTable table, int rowIndex, int columnIndex)
        {
            var temp = new HtmlCell(table);

            temp.FilterProperties[
                HtmlCell.PropertyNames.RowIndex] = 
                rowIndex.ToString();

            temp.FilterProperties[
                HtmlCell.PropertyNames.ColumnIndex] = 
                columnIndex.ToString();

            return temp;
        }

        private string GetConfigurationValue(string key, string defaultValue)
        {
            var environmentVariables = Environment.GetEnvironmentVariables();

            if (environmentVariables.Contains(key) == false)
            {
                TestContext.WriteLine(
                    "No environment variable named '{0}'.  Returning default value '{1}'.",
                    key, defaultValue);

                return defaultValue;
            }
            else
            {
                var temp = environmentVariables[key];

                if (temp == null)
                {
                    TestContext.WriteLine(
                        "Environment variable named '{0}' is null.  Returning default value '{1}'.",
                        key, defaultValue);

                    return defaultValue;
                }
                else
                {
                    TestContext.WriteLine(
                        "Environment variable '{0}' found with value '{1}'.",
                        key, temp);

                    return temp.ToString();
                }
            }
        }

        private BrowserWindow RunApp()
        {
            // PrintDebugInfo();

            string defaultUrl = "http://localhost:21310/";
            string url = GetConfigurationValue("Presidents.CodedUI.WebAppUrl", defaultUrl);

            TestContext.WriteLine("Running app with url '{0}'.", url);

            return BrowserWindow.Launch(new Uri(url));
        }

        private void PrintDebugInfo()
        {
            TestContext.WriteLine("*** TestContext.Properties -- START ***");

            foreach (var key in TestContext.Properties.Keys)
            {
                TestContext.WriteLine("TestContext.Property '{0}' - '{1}'", key, TestContext.Properties[key]);
            }

            TestContext.WriteLine("*** TestContext.Properties -- END ***");

            TestContext.WriteLine("*** Environment Variables -- START ***");

            var envVariables = Environment.GetEnvironmentVariables();

            foreach (var key in envVariables.Keys)
            {
                TestContext.WriteLine("Env Var '{0}' - '{1}'", key, envVariables[key]);
            }

            TestContext.WriteLine("*** Environment Variables -- END ***");
        }



        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
