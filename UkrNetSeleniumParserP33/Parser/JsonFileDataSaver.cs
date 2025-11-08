using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkrNetSeleniumParserP33.Models.DTO;

namespace UkrNetSeleniumParserP33.Parser;

public class JsonFileDataSaver: IDataSaver
{
    public void SaveNewsItems(NewsSection section)
    {
        
        var json = System.Text.Json.JsonSerializer.Serialize(section.NewsItems, new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
        // URL https://www.ukr.net/news/russianaggression.html
        var filename = $"parsed_news_items_{section.Url.Split('/').Last().Replace(".html", "").Replace(" ", "_").ToLower()}.json";
        File.WriteAllText(filename, json);
    }
}
