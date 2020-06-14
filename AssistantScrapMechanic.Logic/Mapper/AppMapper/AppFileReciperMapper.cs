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
        public static AppRecipe ToAppFile(RecipeLocalised localisedData, List<RecipeLocalised> all, List<BlockLocalised> blocks)
        {
            AppRecipe recipe = new AppRecipe
            {
                AppId = localisedData.ItemId.GetAppId(all, blocks),
                Title = localisedData.Title,
                Description = localisedData.Description,
                Ingredients = localisedData.IngredientListLocalised.Select(il => il.ToAppFile(all, blocks)).ToList()
            };
            return recipe;
        }

        private static AppIngredient ToAppFile(this IngredientList localisedData, IReadOnlyCollection<RecipeLocalised> all, List<BlockLocalised> blocks)
        {
            AppIngredient recipe = new AppIngredient
            {
                AppId = localisedData.ItemId.GetAppId(all, blocks),
                Quantity = localisedData.Quantity
            };
            return recipe;
        }

        private static string GetAppId(this string gameId, IEnumerable<RecipeLocalised> all, IReadOnlyCollection<BlockLocalised> blocks)
        {
            if (gameId == null) return string.Empty;

            RecipeLocalised match = all.FirstOrDefault(a => a.ItemId.Equals(gameId));
            if (match != null) return match.AppId;

            BlockLocalised blockMatch = blocks.FirstOrDefault(a => a.ItemId.Equals(gameId));
            if (blockMatch != null) return blockMatch.AppId;

            return string.Empty;
        }
    }
}
