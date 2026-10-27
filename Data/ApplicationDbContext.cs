using Microsoft.EntityFrameworkCore;
using MVC_1.Models;

namespace MVC_1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Хрещений батько", Director = "Френсіс Форд Коппола", Genre = "Драма, кримінал", Year = 1972, Description = "Історія родини мафіозі Корлеоне та боротьби за владу в кримінальному світі Нью-Йорка." },
                new Movie { Id = 2, Title = "Втеча з Шоушенку", Director = "Френк Дарабонт", Genre = "Драма", Year = 1994, Description = "Банкір, засуджений за вбивство, яке не скоював, шукає надію та свободу у в'язниці Шоушенк." },
                new Movie { Id = 3, Title = "Темний лицар", Director = "Крістофер Нолан", Genre = "Бойовик, драма", Year = 2008, Description = "Бетмен протистоїть Джокеру, який занурює Готем у хаос та анархію." },
                new Movie { Id = 4, Title = "Інтерстеллар", Director = "Крістофер Нолан", Genre = "Фантастика, драма", Year = 2014, Description = "Команда дослідників подорожує крізь червоточину у пошуках нового дому для людства." },
                new Movie { Id = 5, Title = "Форрест Гамп", Director = "Роберт Земекіс", Genre = "Драма, комедія", Year = 1994, Description = "Історія життя простодушного чоловіка, який став свідком і учасником ключових подій американської історії." },
                new Movie { Id = 6, Title = "Кримінальне чтиво", Director = "Квентін Тарантіно", Genre = "Кримінал, драма", Year = 1994, Description = "Переплетені історії двох найманих вбивць, боксера та дружини гангстера в Лос-Анджелесі." },
                new Movie { Id = 7, Title = "Початок", Director = "Крістофер Нолан", Genre = "Фантастика, трилер", Year = 2010, Description = "Злодій, який краде секрети зі снів, отримує завдання навпаки — впровадити ідею в чужу підсвідомість." },
                new Movie { Id = 8, Title = "Матриця", Director = "Лана та Ліллі Вачовскі", Genre = "Фантастика, бойовик", Year = 1999, Description = "Програміст дізнається, що реальність навколо нього — симуляція, і приєднується до повстання проти машин." },
                new Movie { Id = 9, Title = "Бійцівський клуб", Director = "Девід Фінчер", Genre = "Драма", Year = 1999, Description = "Офісний працівник, страждаючи від безсоння, засновує підпільний бійцівський клуб зі своїм новим знайомим." },
                new Movie { Id = 10, Title = "Володар перснів: Братство персня", Director = "Пітер Джексон", Genre = "Фентезі, пригоди", Year = 2001, Description = "Гобіт Фродо вирушає у небезпечну подорож, щоб знищити могутній перстень і врятувати Середзем'я." }
            );
        }
    }
}
