using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkrNetSeleniumParserP33.Models.DAL;
using UkrNetSeleniumParserP33.Models.DTO;

namespace UkrNetSeleniumParserP33.Parser;

public class DbDataSaver(NewsContext context): IDataSaver
{
    public void SaveNewsItems(UkrNetSeleniumParserP33.Models.DTO.NewsSection section)
    {
        // Знайти секцію в базі даних або додати нову
        var dbSection = context.NewsSections.FirstOrDefault(s => s.Url == section.Url);
        if (dbSection == null)
        {
            dbSection = new Models.DAL.NewsSection
            {
                Title = section.Title,
                Url = section.Url
            };
            context.NewsSections.Add(dbSection);
            context.SaveChanges();
        }

        // Пройтися по новинах і додати їх до бази даних, якщо вони ще не існують
        foreach (var newsItem in section.NewsItems)
        {
            var source = context.NewsSources.FirstOrDefault(s => s.Name == newsItem.Source);
            if (source == null)
            {
                source = new Models.DAL.NewsSource
                {
                    Name = newsItem.Source
                };
                context.NewsSources.Add(source);
                context.SaveChanges();
            }

            var dbNewsItem = context.NewsItems.FirstOrDefault(n => n.Url == newsItem.Url);
            if (dbNewsItem == null)
            {
                dbNewsItem = new Models.DAL.News
                {
                    Title = newsItem.Title,
                    Url = newsItem.Url,
                    Source = source,
                    NewsSection = dbSection,
                    PublishedAt = newsItem.PublishedAt,
                };
                context.NewsItems.Add(dbNewsItem);
            }
            context.SaveChanges();
        }
    }
}
