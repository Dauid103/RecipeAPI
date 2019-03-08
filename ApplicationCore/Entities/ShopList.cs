using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class ShopList
    {
        public int ShopListId { get; set; }
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<ShopListRecipe> ShopListRecipes { get; set; }

    }
}
