using courseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace courseProject.Services
{
    public class ICollectionService : ICollectionInterface
    {
        private readonly ApplicationDbContext db;
        public ICollectionService(ApplicationDbContext _db) {
            db = _db;
        }

        public IEnumerable<Collection> Collections(string userId) {
            IEnumerable<Collection> collections = db.Collections
                .Include(c => c.Creater)
                .Include(c => c.Topic)
                .Include(c => c.Items)
                .Where(c => c.Creater.Id == userId)
                .ToList();
            return collections;
        }
        public IEnumerable<Collection> CollectionsCotegory(string mode)
        {
            var collections = db.Collections
                .Include(c => c.Creater)
                .Include(c => c.Topic)
                .Include(c => c.Items);
            switch (mode)
            {
                case "Data":
                    return collections.OrderByDescending(c => c.CreationData).ToList();
                case "Name":
                    return collections.OrderBy(c => c.Name).ToList();
                default:
                    return collections.OrderByDescending(c => c.Items.Count()).ToList();
            }
        }
        public CreateCollectionModel CreateCollection(string createrId) => new CreateCollectionModel(){ 
            topics = db.Topics.ToList(),
            CreaterId = createrId
        };
    }
}
