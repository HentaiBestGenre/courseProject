using Microsoft.AspNetCore.Identity;

namespace courseProject.Models
{
    public class SeedData
    {
        private readonly ApplicationDbContext db;
        private UserManager<User> userManager;
        private RoleManager<IdentityRole> roleManager;
        public SeedData(ApplicationDbContext _db, UserManager<User> userMgr, RoleManager<IdentityRole> roleMgr) {
            db = _db;
            userManager = userMgr;
            roleManager = roleMgr;
        }
        public async Task Adding()
        {
            User Hen = db.Users.Where(x => x.Email == "gr801821@gmail.com").First();
            User qwe = db.Users.Where(x => x.Email == "qwe@gmail.com").First();

            ProfilePage profile2 = new ProfilePage()
            {
                Description = "qwe, the only web app user",
                Online = DateTime.Now,
                User = qwe
            };
            db.ProfilePages.AddRange(profile2);

            Tag FilmTag = new Tag() { Name = "Film" };
            Tag MovieTag = new Tag() { Name = "Movie" };
            Tag BookTag = new Tag() { Name = "Book" };
            Tag DaTag = new Tag() { Name = "Da" };
            Tag NetTag = new Tag() { Name = "Net" };
            db.Tags.AddRange(FilmTag, MovieTag, BookTag, DaTag, NetTag);

            Topic Movie = new Topic() { Name = "Movie" };
            Topic Book = new Topic() { Name = "Book" };
            db.Topics.AddRange(Movie, Book);

            Collection Movies = new Collection {
                Name = "Movies",
                Description = "Collection of Movies",
                StringFieldName1 = "Director",
                DateTimeFieldName1 = "Creation Data",
                IntFieldName1 = "Cost",
                Topic = Movie,
                Creater = Hen
            };

            Collection Books = new Collection
            {
                Name = "Books",
                Description = "Collection of Books",
                StringFieldName1 = "Writer",
                IntFieldName1 = "Num of pages",
                Topic = Book,
                Creater = Hen
            };
            db.Collections.AddRange(Movies);
            db.Collections.AddRange(Books);

            Item film1 = new Item
            {
                Name = "Da",
                Tags = new List<Tag>() { FilmTag, MovieTag, DaTag },
                DateTimeFieldValue1 = new DateTime(2020, 1, 1),
                StringFieldValue1 = "Yanchenko Vadim",
                IntFieldValue1 = 123,
                Collection = Movies,
                CreaterId = Hen.Id
            };

            Item film2 = new Item
            {
                Name = "Net",
                Tags = new List<Tag>() { FilmTag, MovieTag, NetTag },
                DateTimeFieldValue1 = new DateTime(2019, 1, 1),
                StringFieldValue1 = "Yanchenko Artem",
                IntFieldValue1 = 321,
                Collection = Movies,
                CreaterId = Hen.Id
            };
            Item book1 = new Item
            {
                Name = "Da",
                Tags = new List<Tag>() { BookTag, DaTag},
                StringFieldValue1 = "Yanchenko Vadim",
                IntFieldValue1 = 2,
                Collection = Books,
                CreaterId = Hen.Id
            };
            Item book2 = new Item
            {
                Name = "Net",
                Tags = new List<Tag>() { BookTag, NetTag },
                StringFieldValue1 = "Yanchenko Artem",
                IntFieldValue1 = 20,
                Collection = Books,
                CreaterId = Hen.Id
            };
            db.Items.AddRange(film1, film2, book1, book2);
            db.SaveChanges();
        }
    }
}
