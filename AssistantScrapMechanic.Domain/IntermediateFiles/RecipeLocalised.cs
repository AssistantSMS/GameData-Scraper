using System.Collections.Generic;

namespace AssistantScrapMechanic.Domain.IntermediateFiles
{
    public class RecipeLocalised : Recipe, ILocalised
    {
        public string AppId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IngredientListLocalised OutputLocalised { get; set; }
        public List<IngredientListLocalised> IngredientListLocalised { get; set; }
    }

    public class IngredientListLocalised: IngredientList
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
