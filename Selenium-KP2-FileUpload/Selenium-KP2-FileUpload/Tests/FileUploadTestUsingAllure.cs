using System.IO;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using Selenium_KP2_FileUpload.PageObjects;

namespace Selenium_KP2_FileUpload.Tests
{
    [AllureNUnit]
    [AllureSuite("File Upload Test")]
    internal class FileUploadTestUsingAllure : BaseTest
    {
        private static readonly string fileName = "uploadTestFile.txt";
        private static readonly string filePath = relativePathFolder + fileName;
        private FileUploadPage fileUploadPage;

        [SetUp]
        public void SetupTest()
        {
            fileUploadPage = new FileUploadPage(driver);
        }

        [Test]
        public void FileUploadTest()
        {
            NavigateToFileUploadPage();
            UploadTheFileToFileUploadPage();
            IsFileUploadedToFileUploadPage();
        }

        [AllureStep("Navigate to file upload page")]
        private void NavigateToFileUploadPage()
        {
            fileUploadPage.NavigateToFileUploadPage();
        }

        [AllureStep("Upload the file to file upload page")]
        private void UploadTheFileToFileUploadPage()
        {
            FileInfo fileToUpload = new FileInfo(filePath);
            fileUploadPage.UploadFile(fileToUpload.FullName);
        }

        [AllureStep("Verify the file is uploaded to file upload page")]
        private void IsFileUploadedToFileUploadPage()
        {
            Assert.That(fileUploadPage.GetUploadedFileName(), Is.EqualTo(fileName), "File is missing or wrong file");
        }
    }
}