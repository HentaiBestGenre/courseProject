using courseProject.Models;

namespace courseProject.Services
{
    public class ITopicService: ITopicInterface
    {
        private readonly ApplicationDbContext db;
        public ITopicService(ApplicationDbContext _db)
        {
            db = _db;
        }

        public IEnumerable<Topic> Topics() => db.Topics;
    }
}
