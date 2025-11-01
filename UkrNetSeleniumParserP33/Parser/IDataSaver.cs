using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkrNetSeleniumParserP33.Parser;

public interface IDataSaver
{
    public void SaveNewsItems(List<Models.NewsItem> newsItems);
}
