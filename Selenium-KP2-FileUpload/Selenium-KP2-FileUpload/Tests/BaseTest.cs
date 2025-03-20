using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Text.Json;
using NUnit.Framework;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Selenium_KP2_FileUpload.Tests
{
    [TestFixture]
    public class BaseTest
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        
        protected static readonly string relativePathFolder = @"..\Resources\";
        protected string downloadDirectory = Path.GetFullPath(relativePathFolder);
        
        protected static readonly int maxWait = 5;
        
        private readonly string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "config.json");
        private readonly string allureConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "allureConfig.json");

        [SetUp]
        public void Setup()
        {
            string json = File.ReadAllText(configPath);
            string url = JsonDocument.Parse(json).RootElement.GetProperty("url").GetString();
            
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", downloadDirectory);
            
            driver = new ChromeDriver(chromeOptions);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(maxWait));

            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}