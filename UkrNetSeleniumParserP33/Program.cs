using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UkrNetSeleniumParserP33.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("ukr.net Selenium parser");



using IWebDriver driver = new ChromeDriver();

try
{
    var parser = new UkrNetSeleniumParserP33.Parser.SiteParser(driver);
    parser.Run();
}
catch (Exception ex)
{
    Console.WriteLine("An error occurred: " + ex.Message);
}
finally
{
    driver.Quit();
}