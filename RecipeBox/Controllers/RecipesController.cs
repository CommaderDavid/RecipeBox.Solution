using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using RecipeBox.Models;

namespace RecipeBox.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        private readonly RecipeBoxContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public RecipesController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public async Task<ActionResult> Index()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            List<Recipe> userRecipes = _db.Recipes.Where(entry => entry.User.Id == currentUser.Id).ToList();
            return View(userRecipes);
        }

        public ActionResult Create()
        {
            ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Recipe recipe, int TagId)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            recipe.User = currentUser;
            _db.Recipes.Add(recipe);
            if (TagId != 0)
            {
                _db.RecipeTag.Add(new RecipeTag() { TagId = TagId, RecipeId = recipe.RecipeId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}