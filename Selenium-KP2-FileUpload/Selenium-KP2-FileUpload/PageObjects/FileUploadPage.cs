using OpenQA.Selenium;

namespace Selenium_KP2_FileUpload.PageObjects
{
    public class FileUploadPage
    {
        private readonly IWebDriver driver;

        public FileUploadPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public readonly By FileUploadLinkLocator = By.XPath("//a[contains(@href, 'upload')]");
        public readonly By ChooseFileButtonLocator = By.XPath("//input[contains(@id, 'file-upload')]");
        public readonly By UploadButtonLocator = By.XPath("//input[contains(@id, 'file-submit')]");
        public readonly By SuccessMessageLocator = By.XPath("//*[contains(text(), 'File Uploaded!')]");
        public readonly By UploadedFileNameLocator = By.XPath("//div[contains(@id, 'uploaded-files')]");

        public void NavigateToFileUploadPage()
        {
            driver.FindElement(FileUploadLinkLocator).Click();
        }

        public void UploadFile(string filePath)
        {
            driver.FindElement(ChooseFileButtonLocator).SendKeys(filePath);
            driver.FindElement(UploadButtonLocator).Click();
        }

        public string GetUploadedFileName()
        {
            return driver.FindElement(UploadedFileNameLocator).Text;
        }

        public bool IsUploadSuccess()
        {
            return driver.FindElement(SuccessMessageLocator).Displayed;
        }
    }
}