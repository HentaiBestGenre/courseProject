using courseProject.Models;
using Microsoft.AspNetCore.Identity;

namespace courseProject.Services {
    public interface ICollectionInterface {
        public IEnumerable<Collection> Collections(string userId);
        public CreateCollectionModel CreateCollection(string createrId);
        public IEnumerable<Collection> CollectionsCotegory(string mode);
    }
    public interface IItemInterface
    {
        public Collection CreateItem(int collectionId);
    }
    public interface ITopicInterface
    {
        public IEnumerable<Topic> Topics();
    }
    public interface IHomePageInterface
    {
        public Dictionary<string, IEnumerable<object>> Collections(string mode, string searchString);
    }
}