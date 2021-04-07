using System.Collections.Generic;
using System;

namespace RecipeBox.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public int RecipeRate { get; set; }
        public string Ingredient { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<RecipeTag> Tags { get; }

        public Recipe()
        {
            this.Tags = new HashSet<RecipeTag>();
        }
    }
}