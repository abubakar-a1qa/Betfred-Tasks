using System;
using System.Threading.Tasks;
using NUnit.Framework;
using TestRail_Integration.Pages;
using TestRail_Integration.Utils;

namespace TestRail_Integration.Tests
{
    internal class CookiesTests : BaseTest
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
                _cookiesPage.AddCookie("testKey", "testValue");
                
                var cookie = _cookiesPage.GetCookie("testKey");
                Assert.That(cookie, Is.EqualTo("testValue"), "Cookie was not added");
                
                _cookiesPage.RemoveCookie("testKey");
                
                var cookieAfterRemove = _cookiesPage.GetCookie("testKey");
                Assert.That(cookieAfterRemove, Is.Null, "Cookie was not removed");

                // Report to TestRail
                await TestRailManager.AddResultsForTestCase(testCaseId, TestRailManager.TestCasePassStatus, "Test Passed");
            }
            catch (Exception ex)
            {
                await TestRailManager.AddResultsForTestCase(testCaseId, TestRailManager.TestCaseFailStatus, $"Test Failed: {ex.Message}");
                throw;
            }
        }
    }
}