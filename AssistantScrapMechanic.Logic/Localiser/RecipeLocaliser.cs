using System.Collections.Generic;
using System.Linq;
using AssistantScrapMechanic.Domain.IntermediateFiles;

namespace AssistantScrapMechanic.Logic.Localiser
{
    public static class RecipeLocaliser
    {
        public static RecipeLocalised Localise(this Recipe recipe, string prefix, int index, Dictionary<string, InventoryDescription> itemNames)
        {
            RecipeLocalised localised = new RecipeLocalised
            {
                AppId = $"{prefix}{(index + 1)}",
                CraftTime = recipe.CraftTime,
                IngredientList = recipe.IngredientList,
                ItemId = recipe.ItemId,
                Title = itemNames.GetTitle(recipe.ItemId),
                Description = itemNames.GetDescription(recipe.ItemId),
                Quantity = recipe.Quantity,
                OutputLocalised = (new IngredientListLocalised
                {
                    ItemId = recipe.ItemId,
                    Quantity = recipe.Quantity,
                    //Title = itemNames.GetTitle(recipe.ItemId),
                    //Description = itemNames.GetDescription(recipe.ItemId),
                }).Localise(itemNames),
                IngredientListLocalised = recipe.IngredientList.Select(i => i.Localise(itemNames)).ToList()
            };

            return localised;
        }

        public static IngredientListLocalised Localise(this IngredientList ingredientList, Dictionary<string, InventoryDescription> itemNames)
        {
            IngredientListLocalised localised = new IngredientListLocalised
            {
                ItemId = ingredientList.ItemId,
                Quantity = ingredientList.Quantity,
                //Title = itemNames.GetTitle(ingredientList.ItemId),
                //Description = itemNames.GetDescription(ingredientList.ItemId),
            };

            return localised;
        }

        public static RecipeLocalised ReLocalise(this RecipeLocalised recipeItem, Dictionary<string, InventoryDescription> itemNames)
        {
            string locTitle = itemNames.GetTitle(recipeItem.ItemId);
            if (!string.IsNullOrEmpty(locTitle))
            {
                recipeItem.Title = locTitle;
            }

            string locDescription = itemNames.GetDescription(recipeItem.ItemId);
            if (!string.IsNullOrEmpty(locDescription))
            {
                recipeItem.Description = locDescription;
            }

            return recipeItem;
        }
    }
}
