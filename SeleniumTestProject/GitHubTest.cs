using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

public class GitHubTest
{

    private const string SearchPhrase = "selenium";

    private static IWebDriver driver;

    [OneTimeSetUp]
    public static void SetUpDriver()
    {
        new DriverManager().SetUpDriver(new ChromeConfig());
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
    }

    [Test]

    public void CheckGitHubSearch()
    {

        

        driver.Navigate().GoToUrl("https://www.github.com");

        

        IWebElement searchInput = driver.FindElement(By.CssSelector("[name='q']"));
        searchInput.SendKeys(SearchPhrase);
        searchInput.SendKeys(Keys.Enter);

        IList<string> actualItems = driver.FindElements(By.CssSelector(".repo-list-item"))
            .Select(item => item.Text.ToLower())
            .ToList();
        IList<string> expectedItems = actualItems
            .Where(item => item.Contains("Invalid search phrase"))
            .ToList();

        Assert.AreEqual(expectedItems, actualItems);

        
    }

    [OneTimeTearDown]
    public static void TearDownDriver()
    {
        driver.Quit();
    }
}
