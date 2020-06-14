using System.Collections.Generic;

namespace AssistantScrapMechanic.Domain.AppFiles
{
    public class AppRecipe
    {
        public string AppId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<AppIngredient> Ingredients { get; set; }
    }

    public class AppIngredient
    {
        public string AppId { get; set; }
        public int Quantity { get; set; }
    }
}
