using System.Collections.Generic;

namespace AssistantScrapMechanic.Domain.IntermediateFiles
{
    public class Recipe
    {
        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public int CraftTime { get; set; }
        public List<IngredientList> IngredientList { get; set; }
    }

    public class IngredientList
    {
        public int Quantity { get; set; }
        public string ItemId { get; set; }
    }
}
