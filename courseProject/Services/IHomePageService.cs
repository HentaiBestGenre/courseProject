using courseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace courseProject.Services
{
    public class IHomePageService : IHomePageInterface
    {
        private readonly ApplicationDbContext db;
        public IHomePageService(ApplicationDbContext _db)
        {
            db = _db;
        }
        public Dictionary<string, IEnumerable<object>>  Collections(string mode, string searchString)
        {
            var collPath = db.Collections
                .Include(c => c.Creater)
                .Include(c => c.Topic)
                .Include(c => c.Items);
            var itemPath = db.Items
                .Include(c => c.Comments)
                .Include(c => c.Tags)
                .Include(c => c.Collection);

            IOrderedQueryable<Collection> collections;
            IOrderedQueryable<Item> items;
            switch (mode)
            {
                case "Name":
                    collections = collPath.OrderBy(c => c.Name);
                    items = itemPath.OrderBy(c => c.Name);
                    break;
                case "Data":
                    collections = collPath.OrderByDescending(c => c.CreationData);
                    items = itemPath.OrderByDescending(c => c.CreationData);
                    break;
                default:
                    collections = collPath.OrderByDescending(c => c.Items.Count());
                    items = itemPath.OrderByDescending(c => c.CreationData);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                return new Dictionary<string, IEnumerable<object>>()
                {
                    ["collections"] = collections
                        .Where(c => c.Name.Contains(searchString))
                        .Where(c => c.Description.Contains(searchString))
                        .ToList(),
                    ["items"] = items
                        .Where(c => c.Name.Contains(searchString))
                        .Union(items.Where(c => c.Tags.Any(c => c.Name.Contains(searchString))))
                        .Union(items.Where(c => c.Comments.Any(c => c.Text.Contains(searchString))))
                        //.Where(c => c.Comments.Any(c => c.Text.Contains(searchString)))
                        .ToList()
                };
            }
            return new Dictionary<string, IEnumerable<object>>()
            {
                ["collections"] = collections
                        .ToList(),
                ["items"] = items
                        .ToList()
            };
        }
    }
}
