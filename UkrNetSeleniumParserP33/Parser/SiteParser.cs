using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkrNetSeleniumParserP33.Models.DTO;

namespace UkrNetSeleniumParserP33.Parser;

public class SiteParser(IWebDriver driver, IDataSaver dataSaver)
{
    
    private void ParseSrection(IWebDriver driver, NewsSection section)
    {
        var pageParser = new PageParser();

        Console.WriteLine($"Parsing section: {section.Title}, URL: {section.Url}");
        // Переходимо до сторінки розділу
        driver.Navigate().GoToUrl(section.Url);
        Console.WriteLine("Page title is: " + driver.Title);
        Console.WriteLine("Page URL is: " + driver.Url);

        // Scroll to the bottom of the page to load all news items (if lazy loading is implemented)
        // Smuth scrolling
        // 5 times scroll
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"Scrolling {i + 1}/10");
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo({ top: document.body.scrollHeight, behavior: 'smooth' });");
            Thread.Sleep(2000); // Задержка 2 секунды, чтобы подождать завершения анимации и подгрузки контента
        }

        // парсимо новини розділу
        var sectionNewsPageContent = driver.PageSource;
        var sectionUrlPart = section.Url.Split('/').Last().Replace(".html", "").Replace(" ", "_").ToLower();
        File.WriteAllText($"ukrnet_${sectionUrlPart}.html", sectionNewsPageContent); // зберігаємо HTML сторінки розділу

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

        section.NewsItems = parsedNewsItems;
    }

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

        var max = 3;
        foreach (var section in newsSections)
        {
            ParseSrection(driver, section);
            dataSaver.SaveNewsItems(section);
            max--;
            if (max <= 0)
                break;
        }

                

        Console.WriteLine("Press any key to exit...");


        Console.ReadKey();
    }

}
