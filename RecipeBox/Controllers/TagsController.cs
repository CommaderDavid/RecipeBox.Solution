using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using RecipeBox.Models;

namespace RecipeBox.Controllers
{
    public class TagsController : Controller
    {
        private readonly RecipeBoxContext _db;
        public TagsController(RecipeBoxContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Tag> model = _db.Tags.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Tag tag)
        {
            _db.Tags.Add(tag);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Tag thisTag = _db.Tags
              .Include(tag => tag.Recipes)
                .ThenInclude(join => join.Recipe)
              .FirstOrDefault(tag => tag.TagId == id);
            return View(thisTag);
        }

        public ActionResult Update(int id)
        {
            Tag thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
            ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name");
            return View(thisTag);
        }

        [HttpPost]
        public ActionResult Update(Tag tag, int tagId)
        {
            bool duplicate = _db.RecipeTag.Any(recTag => recTag.TagId == tagId && recTag.TagId == tag.TagId);
            if (tagId != 0 && !duplicate)
            {
                _db.RecipeTag.Add(new RecipeTag() { TagId = tagId, RecipeId = tag.TagId });
            }
            _db.Entry(tag).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Tag thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
            return View(thisTag);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Tag thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
            _db.Tags.Remove(thisTag);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}