using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium;

namespace TestRail_Integration.Utils
{
    public static class CaptureScreenshot
    {
        public static string TakeScreenshot(IWebDriver driver, string screenshotName, string saveDirectory)
        {
            try
            {
                if (!Directory.Exists(saveDirectory))
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                string screenshotPath = Path.Combine(saveDirectory, $"{screenshotName}_{DateTime.Now:yyyyMMddHHmmss}.png");

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string screenshotBase64 = screenshot.AsBase64EncodedString;
                
                byte[] imageBytes = Convert.FromBase64String(screenshotBase64);
                using (var ms = new MemoryStream(imageBytes))
                {
                    using (Image image = Image.FromStream(ms))
                    {
                        image.Save(screenshotPath, ImageFormat.Png);
                    }
                }

                return screenshotPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to capture screenshot: {ex.Message}");
                throw;
            }
        }
    }
}