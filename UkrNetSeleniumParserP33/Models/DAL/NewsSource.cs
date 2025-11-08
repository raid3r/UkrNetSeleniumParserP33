using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkrNetSeleniumParserP33.Models.DAL;

public class NewsSource
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<News> NewsItems { get; set; } = new List<News>();
}
