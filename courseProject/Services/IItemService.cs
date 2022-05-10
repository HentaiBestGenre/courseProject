using courseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace courseProject.Services
{
    public class IItemService : IItemInterface
    {
        private readonly ApplicationDbContext db;
        public IItemService(ApplicationDbContext _db) {
            db = _db;
        }

        public Collection CreateItem(int collectionId) =>
            db.Collections
            .Include(c => c.Creater)
            .Where(c => c.Id == collectionId).First();
    }
}
