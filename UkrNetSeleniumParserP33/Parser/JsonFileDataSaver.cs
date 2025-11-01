using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkrNetSeleniumParserP33.Parser;

public class JsonFileDataSaver: IDataSaver
{
    public void SaveNewsItems(List<Models.NewsItem> newsItems)
    {
        
        var json = System.Text.Json.JsonSerializer.Serialize(newsItems, new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });

        File.WriteAllText("parsed_news_items.json", json);
    }
}
