using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkrNetSeleniumParserP33.Models.DAL;

public class NewsSection
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public virtual ICollection<News> NewsItems { get; set; } = new List<News>();
}
