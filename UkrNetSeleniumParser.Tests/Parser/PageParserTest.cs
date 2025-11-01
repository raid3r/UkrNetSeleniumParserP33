using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkrNetSeleniumParser.Tests.Parser;

public class PageParserTest
{
    [Fact]
    public void MainPageParseTest()
    {
        var dataDir = Path.Combine("C:\\Users\\kvvkv\\source\\repos\\UkrNetSeleniumParserP33\\UkrNetSeleniumParser.Tests\\", "TestData");
        var mainPageHtml = File.ReadAllText(Path.Combine(dataDir, "ukrnet_page.html"), Encoding.UTF8);


        var parser = new UkrNetSeleniumParserP33.Parser.PageParser();

        var sections = parser.GetSections(mainPageHtml);

        Assert.NotNull(sections);
        Assert.True(sections.Count > 0);
        Assert.Contains(sections, s => s.Title.Contains("Авто"));
    }

    [Fact]
    public void SectionPageParseTest()
    {
        var dataDir = Path.Combine("C:\\Users\\kvvkv\\source\\repos\\UkrNetSeleniumParserP33\\UkrNetSeleniumParser.Tests\\", "TestData");
        var sectionPageHtml = File.ReadAllText(Path.Combine(dataDir, "ukrnet_auto_section_page.html"), Encoding.UTF8);

        var parser = new UkrNetSeleniumParserP33.Parser.PageParser();
        var newsItems = parser.GetNewsItems(sectionPageHtml);
        Assert.NotNull(newsItems);

        //Assert.True(newsItems.Count == 20, "Exactly 20 items"); // Failing test to check for exactly 20 items

        Assert.True(newsItems.Count > 0);
        Assert.All(newsItems, item =>
        {
            Assert.False(string.IsNullOrWhiteSpace(item.Title));
            Assert.False(string.IsNullOrWhiteSpace(item.Url));
            Assert.False(string.IsNullOrWhiteSpace(item.Source));
        });
    }
}
