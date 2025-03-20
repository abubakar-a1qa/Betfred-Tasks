using Selenium_KP2_FileUpload.PageObjects;
using System.IO;
using NUnit.Framework;

namespace Selenium_KP2_FileUpload.Tests
{
    public class FileUploadTests : BaseTest
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
            fileUploadPage.NavigateToFileUploadPage();
            FileInfo fileToUpload = new FileInfo(filePath);
            fileUploadPage.UploadFile(fileToUpload.FullName);

            Assert.That(fileUploadPage.GetUploadedFileName(), Is.EqualTo(fileName), "File is missing or wrong file");
            Assert.That(fileUploadPage.IsUploadSuccess(), Is.True, "Upload was not successful");
        }
    }
}