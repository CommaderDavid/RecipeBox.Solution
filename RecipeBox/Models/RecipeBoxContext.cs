using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RecipeBox.Models
{
    public class RecipeBoxContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipeTag> RecipeTag { get; set; }

        public RecipeBoxContext(DbContextOptions options) : base(options) { }
    }
}