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
    

    public void SaveNewsItems(NewsSection section)
    {
        // Знайти секцію в базі даних або додати нову

        // Пройтися по новинах і додати їх до бази даних, якщо вони ще не існують

        // Зберегти зміни в базі даних

        //var json = System.Text.Json.JsonSerializer.Serialize(section.NewsItems, new System.Text.Json.JsonSerializerOptions
        //{
        //    WriteIndented = true,
        //    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        //});
        //// URL https://www.ukr.net/news/russianaggression.html
        //var filename = $"parsed_news_items_{section.Url.Split('/').Last().Replace(".html", "").Replace(" ", "_").ToLower()}.json";
        //File.WriteAllText(filename, json);
    }
}
