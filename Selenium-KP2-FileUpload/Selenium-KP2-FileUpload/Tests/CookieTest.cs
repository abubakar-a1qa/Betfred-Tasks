using System;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using Selenium_KP2_FileUpload.PageObjects;
using Selenium_KP2_FileUpload.Utils;
using System.Threading.Tasks;

namespace Selenium_KP2_FileUpload.Tests
{
    [AllureNUnit]
    [AllureSuite("Cookies Test")]
    internal class CookieTest : BaseTest
    {
        private CookiesPage _cookiesPage;

        [SetUp]
        public void Setup()
        {
            _cookiesPage = new CookiesPage(driver);
        }

        [Test]
        public async Task CookieTests()
        {
            string testCaseId = "28641865"; 

            try
            {
                // Test Steps
                AddCookieToHomepage();
                VerifyCookieWasAddedToHomepage();
                RemoveCookieFromHomepage();
                VerifyCookieWasRemovedFromHomepage();

                // Report to TestRail
                await TestRailManager.AddResultsForTestCase(testCaseId, TestRailManager.TestCasePassStatus, "Test Passed");
            }
            catch (Exception ex)
            {
                await TestRailManager.AddResultsForTestCase(testCaseId, TestRailManager.TestCaseFailStatus, $"Test Failed: {ex.Message}");
                throw;
            }
        }

        [AllureStep("Add Cookie to Homepage")]
        public void AddCookieToHomepage()
        {
            _cookiesPage.AddCookie("testKey", "testValue");
        }

        [AllureStep("Verify cookie was added to Homepage")]
        public void VerifyCookieWasAddedToHomepage()
        {
            var cookie = _cookiesPage.GetCookie("testKey");
            Assert.That(cookie, Is.EqualTo("testValue"), "Cookie was not added");
        }

        [AllureStep("Remove Cookie from Homepage")]
        public void RemoveCookieFromHomepage()
        {
            _cookiesPage.RemoveCookie("testKey");
        }

        [AllureStep("Verify cookie was removed from Homepage")]
        public void VerifyCookieWasRemovedFromHomepage()
        {
            var cookieAfterRemove = _cookiesPage.GetCookie("testKey");
            Assert.That(cookieAfterRemove, Is.Null, "Cookie was not removed");
        }
    }
}