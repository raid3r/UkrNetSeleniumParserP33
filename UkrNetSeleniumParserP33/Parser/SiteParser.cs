using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkrNetSeleniumParserP33.Models;

namespace UkrNetSeleniumParserP33.Parser;

public class SiteParser(IWebDriver driver, IDataSaver dataSaver)
{
    
    public void Run()
    {
        var pageParser = new PageParser();

        var url = "https://www.ukr.net/";
        driver.Navigate().GoToUrl(url);

        //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        Console.WriteLine("Page title is: " + driver.Title);
        Console.WriteLine("Page URL is: " + driver.Url);


        var pageContent = driver.PageSource;
        File.WriteAllText("ukrnet_page.html", pageContent);

        var newsSections = pageParser.GetSections(pageContent);

        // Знаходимо розділ "Авто"
        var autoNewsSection = newsSections.FirstOrDefault(s => s.Title.Contains("Авто"));

        // Переходимо до сторінки розділу
        driver.Navigate().GoToUrl(autoNewsSection.Url);
        Console.WriteLine("Page title is: " + driver.Title);
        Console.WriteLine("Page URL is: " + driver.Url);

        // Scroll to the bottom of the page to load all news items (if lazy loading is implemented)
        // Smuth scrolling
        // 5 times scroll
        for (int i = 0; i < 10; i++)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo({ top: document.body.scrollHeight, behavior: 'smooth' });");
            Thread.Sleep(2000); // Задержка 2 секунды, чтобы подождать завершения анимации и подгрузки контента
        }

        // парсимо новини розділу
        var sectionNewsPageContent = driver.PageSource;
        File.WriteAllText("ukrnet_auto_section_page.html", sectionNewsPageContent); // зберігаємо HTML сторінки розділу

        var parsedNewsItems = pageParser.GetNewsItems(sectionNewsPageContent);

        foreach (var newsItem in parsedNewsItems)
        {
            Console.WriteLine($"Title: {newsItem.Title}");
            Console.WriteLine($"URL: {newsItem.Url}");
            Console.WriteLine($"Source: {newsItem.Source}");
            Console.WriteLine($"Published At: {newsItem.PublishedAt?.ToString("dd.MM.yyyy HH:mm")}");
            Console.WriteLine();
        }

        Console.WriteLine("Count: " + parsedNewsItems.Count);

        dataSaver.SaveNewsItems(parsedNewsItems);

        Console.WriteLine("Press any key to exit...");


        Console.ReadKey();
    }

}
