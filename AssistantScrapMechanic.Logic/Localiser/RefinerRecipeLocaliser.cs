using System.Collections.Generic;
using AssistantScrapMechanic.Domain.IntermediateFiles;

namespace AssistantScrapMechanic.Logic.Localiser
{
    public static class RefinerRecipeLocaliser
    {
        public static RefinerRecipeLocalised Localise(this RefinerRecipe refinerRecipe, string itemId, string prefix, int index, Dictionary<string, InventoryDescription> itemNames)
        {
            RefinerRecipeLocalised localised = new RefinerRecipeLocalised
            {
                AppId = $"{prefix}{(index + 1)}",
                ItemId = itemId,
                Title = itemNames.GetTitle(itemId),
                IngredientListLocalised = new List<IngredientListLocalised>
                {
                    new IngredientListLocalised
                    {
                        ItemId = refinerRecipe.ItemId,
                        Title = itemNames.GetTitle(refinerRecipe.ItemId),
                        Description = itemNames.GetDescription(refinerRecipe.ItemId),
                        Quantity = refinerRecipe.Quantity
                    }
                }
            };

            return localised;
        }
    }
}
