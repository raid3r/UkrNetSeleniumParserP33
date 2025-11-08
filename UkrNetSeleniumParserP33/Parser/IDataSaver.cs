using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkrNetSeleniumParserP33.Models.DTO;
using static System.Collections.Specialized.BitVector32;

namespace UkrNetSeleniumParserP33.Parser;

public interface IDataSaver
{
    public void SaveNewsItems(NewsSection section);
}
