using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkrNetSeleniumParserP33.Models.DAL;

    public class NewsContext: DbContext
{
    // Конструктор за замовченням
    public NewsContext() : base() { }

    // Конструктор з параметрами для налаштування контексту
    public NewsContext(DbContextOptions<NewsContext> options) : base(options) { }

    // Визначення DbSet для сутностей
    public virtual DbSet<News> NewsItems { get; set; }
    public virtual DbSet<NewsSource> NewsSources { get; set; }
    public virtual DbSet<NewsSection> NewsSections { get; set; }


    // Метод для налаштування моделі та конфігурації бази даних
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=SILVERSTONE\\SQLEXPRESS;Initial Catalog=UkrNetNewsP33;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;");
        }
    }
}

