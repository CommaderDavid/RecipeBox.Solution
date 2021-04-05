using System.Collections.Generic;

namespace RecipeBox.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<RecipeTag> Recipes { get; set; }

        public Tag()
        {
            this.Recipes = new HashSet<RecipeTag>();
        }
    }
}