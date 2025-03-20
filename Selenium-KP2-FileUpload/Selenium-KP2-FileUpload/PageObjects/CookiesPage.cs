using NUnit.Framework;
using OpenQA.Selenium;
using Selenium_KP2_FileUpload.Tests;

namespace Selenium_KP2_FileUpload.PageObjects
{
    internal class CookiesPage : BaseTest
    {
        public CookiesPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void AddCookie(string cookieName, string cookieValue)
        {
            var cookie = new Cookie(cookieName, cookieValue);
            driver.Manage().Cookies.AddCookie(cookie);
        }

        public void RemoveCookie(string cookie)
        {
            driver.Manage().Cookies.DeleteCookieNamed(cookie);
        }

        public string GetCookie(string cookieName)
        {
            var cookie = driver.Manage().Cookies.GetCookieNamed(cookieName);
            return cookie?.Value;
        }
    }
}