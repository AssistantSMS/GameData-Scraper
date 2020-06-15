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
                AppId = localisedData.ItemId.GetAppId(lookup),
                Title = localisedData.Title,
                Description = localisedData.Description,
                Inputs = localisedData.IngredientListLocalised.Select(il => il.ToAppFile(lookup)).ToList()
            };
            return recipe;
        }

        private static AppIngredient ToAppFile(this IngredientList localisedData, Dictionary<string, List<dynamic>> lookup)
        {
            AppIngredient recipe = new AppIngredient
            {
                AppId = localisedData.ItemId.GetAppId(lookup),
                Quantity = localisedData.Quantity
            };
            return recipe;
        }

        private static string GetAppId(this string gameId, IReadOnlyDictionary<string, List<dynamic>> lookup)
        {
            if (gameId == null) return string.Empty;
            if (!lookup.ContainsKey(gameId)) return string.Empty;

            List<dynamic> matches = lookup[gameId];
            string appId = string.Empty;

            foreach (dynamic match in matches)
            {
                if (match is RecipeLocalised recipeLocalised) appId = recipeLocalised.AppId;
                if (match is GameItemLocalised blockLocalised) appId = blockLocalised.AppId;
                return appId;
            }

            return string.Empty;
        }
    }
}
