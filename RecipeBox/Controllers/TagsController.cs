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
    }
}