using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVC_1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Director = table.Column<string>(type: "TEXT", nullable: false),
                    Genre = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    PosterFileName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Genre", "PosterFileName", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "Історія родини мафіозі Корлеоне та боротьби за владу в кримінальному світі Нью-Йорка.", "Френсіс Форд Коппола", "Драма, кримінал", "", "Хрещений батько", 1972 },
                    { 2, "Банкір, засуджений за вбивство, яке не скоював, шукає надію та свободу у в'язниці Шоушенк.", "Френк Дарабонт", "Драма", "", "Втеча з Шоушенку", 1994 },
                    { 3, "Бетмен протистоїть Джокеру, який занурює Готем у хаос та анархію.", "Крістофер Нолан", "Бойовик, драма", "", "Темний лицар", 2008 },
                    { 4, "Команда дослідників подорожує крізь червоточину у пошуках нового дому для людства.", "Крістофер Нолан", "Фантастика, драма", "", "Інтерстеллар", 2014 },
                    { 5, "Історія життя простодушного чоловіка, який став свідком і учасником ключових подій американської історії.", "Роберт Земекіс", "Драма, комедія", "", "Форрест Гамп", 1994 },
                    { 6, "Переплетені історії двох найманих вбивць, боксера та дружини гангстера в Лос-Анджелесі.", "Квентін Тарантіно", "Кримінал, драма", "", "Кримінальне чтиво", 1994 },
                    { 7, "Злодій, який краде секрети зі снів, отримує завдання навпаки — впровадити ідею в чужу підсвідомість.", "Крістофер Нолан", "Фантастика, трилер", "", "Початок", 2010 },
                    { 8, "Програміст дізнається, що реальність навколо нього — симуляція, і приєднується до повстання проти машин.", "Лана та Ліллі Вачовскі", "Фантастика, бойовик", "", "Матриця", 1999 },
                    { 9, "Офісний працівник, страждаючи від безсоння, засновує підпільний бійцівський клуб зі своїм новим знайомим.", "Девід Фінчер", "Драма", "", "Бійцівський клуб", 1999 },
                    { 10, "Гобіт Фродо вирушає у небезпечну подорож, щоб знищити могутній перстень і врятувати Середзем'я.", "Пітер Джексон", "Фентезі, пригоди", "", "Володар перснів: Братство персня", 2001 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
