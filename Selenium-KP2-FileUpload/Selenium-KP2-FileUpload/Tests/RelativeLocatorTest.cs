using NUnit.Framework;
using OpenQA.Selenium;

namespace Selenium_KP2_FileUpload.Tests
{
    internal class RelativeLocatorTest : BaseTest
    {
        public static By EmailTextBox = By.Id("email");
        public static By RetrivePasswordButton = By.Id("form_submit");
        private static readonly string email = "test@test.com";

        [Test]
        public void RelativeLocatorTests()
        {
            var emailTextBoxRelative =
                driver.FindElement(RelativeBy.WithLocator(EmailTextBox).Above(RetrivePasswordButton));
            emailTextBoxRelative.SendKeys(email);

            wait.Until(driver => driver.FindElement(EmailTextBox).GetAttribute("value") == email);

            var getTextFromEmailTextBox = driver.FindElement(EmailTextBox).GetAttribute("value");
            Assert.That(getTextFromEmailTextBox, Is.EqualTo(email));
        }
    }
}