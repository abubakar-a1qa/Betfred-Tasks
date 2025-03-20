using System;
using System.IO;
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
        public void CookieTests()
        {
            const string testCaseId = "28641865";
            string screenshotPath = string.Empty;

            try
            {
                // Test Steps
                _cookiesPage.AddCookie("testKey", "testValue");
                Assert.That(_cookiesPage.GetCookie("testKey"), Is.EqualTo("testValue"));
                
                _cookiesPage.RemoveCookie("testKey");
                Assert.That(_cookiesPage.GetCookie("testKey"), Is.Null);

                // Report success with screenshot
                screenshotPath = CaptureScreenshot("CookieTest_Success");
                TestRailManager.AddResultsForTestCase(
                    testCaseId,
                    TestRailManager.TestCasePassStatus,
                    "All cookie operations completed successfully",
                    screenshotPath
                );
            }
            catch (Exception ex)
            {
                // Report failure with screenshot
                screenshotPath = CaptureScreenshot("CookieTest_Failure");
                TestRailManager.AddResultsForTestCase(
                    testCaseId,
                    TestRailManager.TestCaseFailStatus,
                    $"Test Failed: {ex.Message}",
                    screenshotPath
                );
                throw;
            }
            finally
            {
                // Cleanup temporary files
                if (File.Exists(screenshotPath))
                {
                    File.Delete(screenshotPath);
                }
            }
        }
    }
}