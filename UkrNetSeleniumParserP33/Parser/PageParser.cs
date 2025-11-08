using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkrNetSeleniumParserP33.Models.DTO;

namespace UkrNetSeleniumParserP33.Parser;

public class PageParser
{
    public List<NewsSection> GetSections(string mainPageHtml)
    {
        var doc = LoadHtml(mainPageHtml);

        var mainNewsSections = doc.QuerySelectorAll("h2.feed__section--title");

        // Створюємо список розділів новин
        var newsSections = new List<NewsSection>();

        // Парсимо розділи новин
        foreach (var section in mainNewsSections)
        {
            // отримуємо посилання та назву розділу
            var link = section.QuerySelector("a");
            if (link != null)
            {
                var sectionTitle = link.InnerText.Trim();
                var sectionUrl = link.GetAttributeValue("href", string.Empty).Trim();
                Console.WriteLine($"Section: {sectionTitle}");
                Console.WriteLine($"URL: {sectionUrl}");
                Console.WriteLine();
            }
            // Додаємо розділ до списку
            newsSections.Add(new NewsSection
            {
                Title = link?.InnerText.Trim() ?? "No Title",
                Url = link?.GetAttributeValue("href", string.Empty).Trim() ?? string.Empty
            });
        }
        return newsSections;
    }


    public List<NewsItem> GetNewsItems(string sectionPageHtml)
    {
        var doc = LoadHtml(sectionPageHtml);


        /*
         * <section class="im">
            <time class="im-tm">12:46</time>
                <div class="im-tl-bk">
                <div class="im-tl">
                <a href="https://vnedorozhnik.net.ua/rejtingi/top-7-avtomobiliv-yaki-irzhaviyut-shvydshe-za-inshyh-eksperty-nazvaly-antyrejtyng" class="im-tl_a" rel="nofollow" target="_blank" data-count="114286722,67277428,20,">Топ-7 автомобілів, які іржавіють швидше за інших: експерти назвали антирейтинг</a>
                <div class="im-pr">
                    <a href="https://www.ukr.net/ru/source/suv-news-4453.html" pst="suv-news" pid="4453" class="im-pr_a">(SUV News)</a>
                </div>
                </div>
                </div>
            </section>

        <section class="im">
            <time class="im-tm">31&nbsp;жов</time>
            <div class="im-tl-bk">
            <div class="im-tl">
            <a href="https://ampercar.com/vlasnyky-holovnoho-svitovoho-khita-toyota-perelichyly-vse-shco-im-ne-podobaietsia-u-svoikh-krosoverakh" class="im-tl_a" rel="nofollow" target="_blank" data-count="114280131,67273526,20," data-dups="true">Власники головного світового хіта Toyota перелічили все, що їм не подобається у своїх кросоверах</a>





                <div class="im-pr">
                    <a href="https://www.ukr.net/source/ampercar-4036.html" pst="ampercar" pid="4036" class="im-pr_a">(AMPERCAR)</a>
                    + <span class="im-pr-ds">1 схожа</span>
                </div>
            </div>
        </div>

            <!-- AMOUNT BLOCK -->
            <div class="im-at-bk dups" id="114280131"></div>
            <!-- END AMOUNT BLOCK -->

    </section>

         * 
         */
        var newsItems = doc.QuerySelectorAll("section.im");
        List<NewsItem> parsedNewsItems = new List<NewsItem>();
        foreach (var item in newsItems)
        {
            var timeNode = item.QuerySelector("time.im-tm");
            var titleLinkNode = item.QuerySelector("div.im-tl a.im-tl_a");
            var sourceLinkNode = item.QuerySelector("div.im-pr a.im-pr_a");
            if (timeNode != null && titleLinkNode != null && sourceLinkNode != null)
            {
                var timeText = timeNode.InnerText.Trim();
                var titleText = titleLinkNode.InnerText.Trim();
                var titleUrl = titleLinkNode.GetAttributeValue("href", string.Empty).Trim();
                var sourceText = sourceLinkNode.InnerText.Trim().TrimEnd(')').TrimStart('(');
                // Формуємо дату публікації
                // 31&nbsp;жов
                // 11:10

                DateTime? publishedAt = null;
                if (DateTime.TryParseExact(timeText, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime timePart))
                {
                    publishedAt = DateTime.Today;
                    publishedAt = publishedAt?.Date + timePart.TimeOfDay;
                }
                var newsItem = new NewsItem
                {
                    Title = titleText,
                    Url = titleUrl,
                    Source = sourceText,
                    PublishedAt = publishedAt
                };
                parsedNewsItems.Add(newsItem);

            }

        }
        return parsedNewsItems;
    }


    private HtmlNode LoadHtml(string htmlContent)
    {
        var htmlDocument = new HtmlAgilityPack.HtmlDocument();
        htmlDocument.LoadHtml(htmlContent);
        return htmlDocument.DocumentNode;
    }
}
