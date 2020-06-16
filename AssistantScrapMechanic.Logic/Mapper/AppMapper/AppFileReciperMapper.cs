using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssistantScrapMechanic.Domain.AppFiles;
using AssistantScrapMechanic.Domain.IntermediateFiles;

namespace AssistantScrapMechanic.Logic.Mapper.AppMapper
{
    public static class AppFileReciperMapper
    {
        public static AppRecipe ToAppFile(RecipeLocalised localisedData, Dictionary<string, List<dynamic>> lookup)
        {
            AppRecipe recipe = new AppRecipe
            {
                AppId = localisedData.AppId,
                Title = localisedData.Title,
                Description = localisedData.Description,
                Output = localisedData.ItemId.GetAppIngredient(lookup),
                Inputs = localisedData.IngredientListLocalised.Select(il => il.ItemId.GetAppIngredient(lookup)).ToList()
            };
            return recipe;
        }
        
        private static AppIngredient GetAppIngredient(this string gameId, IReadOnlyDictionary<string, List<dynamic>> lookup)
        {
            AppIngredient defaultObj = new AppIngredient
            {
                AppId = string.Empty,
                Quantity = 0
            };
            if (gameId == null) return defaultObj;
            if (!lookup.ContainsKey(gameId)) return defaultObj;

            List<dynamic> matches = lookup[gameId];
            string appId = string.Empty;

            foreach (dynamic match in matches)
            {
                if (match is RecipeLocalised recipeLocalised) appId = recipeLocalised.AppId;
                if (match is GameItemLocalised blockLocalised) appId = blockLocalised.AppId;
                if (appId.Contains("recipe")) continue;
                return new AppIngredient
                {
                    AppId = appId,
                    Quantity = 1
                };
            }

            return defaultObj;
        }
    }
}
