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
    }
}