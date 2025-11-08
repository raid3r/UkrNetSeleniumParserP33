using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UkrNetSeleniumParserP33.Models;
using UkrNetSeleniumParserP33.Parser;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("ukr.net Selenium parser");



using IWebDriver driver = new ChromeDriver();

try
{
    //var parser = new UkrNetSeleniumParserP33.Parser.SiteParser(driver, new JsonFileDataSaver());
    var parser = new UkrNetSeleniumParserP33.Parser.SiteParser(
        driver,
        new DbDataSaver(
            new UkrNetSeleniumParserP33.Models.DAL.NewsContext()
            )
        );
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