using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkrNetSeleniumParserP33.Models;

public class NewsSection
{
    public string Title { get; set; }
    public string Url { get; set; }
    public List<NewsItem> NewsItems { get; set; } = [];
}
