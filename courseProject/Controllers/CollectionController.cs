using courseProject.Models;
using courseProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace courseProject.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private IItemInterface ItemInterface;
        private ApplicationDbContext db;
        private ICollectionInterface CollectionInterface;
        public CollectionController(IItemInterface III, 
            ApplicationDbContext _db,
            ICollectionInterface ici
            )
        {
            db = _db;
            ItemInterface = III;
            CollectionInterface = ici;
        }

        [AllowAnonymous]
        public IActionResult ShowCollection(int collectionId) {
            Collection? collection = db.Collections.Include(c => c.Creater).Include(c => c.Items)
                    .Where(c => c.Id == collectionId).FirstOrDefault();
            if(collection == null)
            {
                return View("Error");
            }
            return View("ShowCollection", collection);
        }

        public IActionResult CreateCollection(string createrId) => View(CollectionInterface.CreateCollection(createrId));

        [HttpPost]
        public IActionResult CreateCollection(CreateCollectionModel collection)
        {
            if (ModelState.IsValid)
            {
                User? creater = db.Users.Where(c => c.Id == collection.CreaterId).FirstOrDefault();
                Topic? topic = db.Topics.Where(c => c.Id == collection.TopicId).FirstOrDefault();
                if (creater != null && topic != null)
                {
                    Collection newCollection = new Collection()
                    {
                        Name = collection.Name,
                        Description = collection.Description,
                        StringFieldName1 = collection.StringFieldName1,
                        StringFieldName2 = collection.StringFieldName2,
                        StringFieldName3 = collection.StringFieldName3,
                        IntFieldName1 = collection.IntFieldName1,
                        IntFieldName2 = collection.IntFieldName2,
                        IntFieldName3 = collection.IntFieldName3,
                        DateTimeFieldName1 = collection.DateTimeFieldName1,
                        DateTimeFieldName2 = collection.DateTimeFieldName2,
                        DateTimeFieldName3 = collection.DateTimeFieldName3,
                        BoolFieldName1 = collection.BoolFieldName1,
                        BoolFieldName2 = collection.BoolFieldName2,
                        BoolFieldName3 = collection.BoolFieldName3,
                        Creater = creater,
                        Topic = topic
                    };
                    db.Collections.AddRange(newCollection);
                    db.SaveChanges();
                    return RedirectToAction("ShowCollection", new { collectionId = newCollection.Id });
                }
                ModelState.AddModelError("", "Wrong Topic or Creater does not exist");
            }
            return View(CollectionInterface.CreateCollection(collection.CreaterId));
        }

        [HttpPost]
        public IActionResult Delete(int collectionId)
        {
            Collection? collection = db.Collections.Include(c => c.Creater).Where(c => c.Id == collectionId).FirstOrDefault();
            db.Collections.Remove(collection);
            db.SaveChanges();
            return RedirectToAction("Profile", "Account", new { userId = collection.Creater.Id});
            //будет работать только для профиля и перенаправлять в профиль
        }

        public IActionResult CreateItem(int collectionId) { return View(ItemInterface.CreateItem(collectionId)); }

        [HttpPost]
        public IActionResult CreateItem(CreateItemModel item)
        {
            if (ModelState.IsValid)
            {
                Collection collection = db.Collections.Where(c => c.Id == item.CollectionId).FirstOrDefault();
                List<Tag> tags = new List<Tag>();
                foreach(var tag in item.tags)
                {
                    Tag? tag1 = db.Tags.Where(c => c.Name == tag).FirstOrDefault();
                    if(tag1 == null)
                    {
                        Tag newTag = new Tag() { Name = tag };
                        db.Tags.Add(newTag);
                        tags.Add(newTag);
                    }
                    else
                    {
                        tags.Add(tag1);
                    }
                }
                Item newItem = new Item()
                {
                    Name = item.Name,
                    Collection = collection,
                    CreaterId = item.CreaterId,
                    Tags = tags,
                    StringFieldValue1 = item.StringFieldValue1,
                    StringFieldValue2 = item.StringFieldValue2,
                    StringFieldValue3 = item.StringFieldValue3,
                    IntFieldValue1 = item.IntFieldValue1,
                    IntFieldValue2 = item.IntFieldValue2,
                    IntFieldValue3 = item.IntFieldValue3,
                    DateTimeFieldValue1 = item.DateTimeFieldValue1,
                    DateTimeFieldValue2 = item.DateTimeFieldValue2,
                    DateTimeFieldValue3 = item.DateTimeFieldValue3,
                    BoolFieldValue1 = item.BoolFieldValue1,
                    BoolFieldValue2 = item.BoolFieldValue2,
                    BoolFieldValue3 = item.BoolFieldValue3
                };
                db.Items.Add(newItem);
                db.SaveChanges();
                return RedirectToAction("ShowCollection", new { collectionId = item.CollectionId });
            }
            return View(ItemInterface.CreateItem(item.CollectionId));
        }

        [HttpPost]
        public IActionResult DeleteItem(int itemId)
        {
            Item? item = db.Items.Include(c=>c.Collection).Where(c => c.Id == itemId).FirstOrDefault();
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("ShowCollection", new { collectionId = item.Collection.Id });
        }

        [AllowAnonymous]
        public IActionResult ShowItem(int itemId)
        {
            Item? item = db.Items.Include(c => c.Tags).Include(c => c.Collection.Creater).Include(c => c.Comments).Where(c => c.Id == itemId)
                .FirstOrDefault();
            if (item == null) { return View("Error"); }
            return View("ShowItem", item);
        }

        [HttpPost]
        public IActionResult CreateComment(CreateComment comment)
        {
            if (ModelState.IsValid)
            {
                Item item = db.Items.Where(c => c.Id == comment.ItemId).First();
                Comment newComment = new Comment() {
                    Text = comment.Text,
                    CreaterId = comment.CreaterId,
                    Item = item
                };
                db.Comments.Add(newComment);
                db.SaveChanges();
                return RedirectToAction("ShowItem", new { itemId = comment.ItemId });
            }
            return RedirectToAction("ShowItem", new { itemId = comment.ItemId });
        }
    }
}
