using WWT_Automation.PageObjects;

namespace WWT_Automation.Tests.AdminTool._1._Merchant_Profiles
{
    public class Lookup : BaseTest
    {
        [Test]
        public void MerchantProfilesLookup_FilterMerchants_DisplayFilteredMerchants()
        {
            Driver.Navigate().GoToUrl("https://apadmintool.zero21.eu/");

            var merchantLookupPage = new SignInPage(Driver, Wait)
                .EnterUsername("SuperAdmin")
                .EnterPassword("T21kyytt$LVP#")
                .ClickSignIn()
                .GoToMerchantProfiles()
                .ClickOnLookup();

            var accountManagers = merchantLookupPage.OpenAccountManagerDropdown().GetAvailableAccountManagers();
        }
    }
}
