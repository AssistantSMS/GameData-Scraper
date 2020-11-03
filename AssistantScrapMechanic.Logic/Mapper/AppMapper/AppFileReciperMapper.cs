using System;
using System.Collections.Generic;
using System.Linq;
using AssistantScrapMechanic.Domain.AppFiles;
using AssistantScrapMechanic.Domain.IntermediateFiles;

namespace AssistantScrapMechanic.Logic.Mapper.AppMapper
{
    public static class AppFileReciperMapper
    {
        public static AppRecipe ToAppFile(RecipeLocalised localisedData, Dictionary<string, List<ILocalised>> lookup)
        {
            AppRecipe recipe = new AppRecipe
            {
                AppId = localisedData.AppId,
                Title = localisedData.Title,
                Description = localisedData.Description,
                CraftingTime = localisedData.CraftTime,
                Output = localisedData.ItemId.GetAppIngredient(localisedData.Quantity, lookup),
                Inputs = localisedData.IngredientListLocalised.Select(il => il.ItemId.GetAppIngredient(il.Quantity, lookup)).Where(il => !string.IsNullOrEmpty(il.AppId)).ToList()
            };
            return recipe;
        }
        
        private static AppIngredient GetAppIngredient(this string gameId, int quantity, IReadOnlyDictionary<string, List<ILocalised>> lookup)
        {
            AppIngredient defaultObj = new AppIngredient
            {
                AppId = string.Empty,
                Quantity = 0
            };
            if (gameId == null) return defaultObj;
            if (!lookup.ContainsKey(gameId)) return defaultObj;

            List<ILocalised> matches = lookup[gameId];
            string appId = string.Empty;

            foreach (dynamic match in matches)
            {
                if (match is RecipeLocalised recipeLocalised) appId = recipeLocalised.AppId;
                if (match is GameItemLocalised blockLocalised) appId = blockLocalised.AppId;
                if (appId.Contains("recipe")) continue;
                return new AppIngredient
                {
                    AppId = appId,
                    Quantity = quantity
                };
            }

            return defaultObj;
        }
    }
}
