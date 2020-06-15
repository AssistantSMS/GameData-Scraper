﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.Domain.AppFiles
{
    public class AppRecipe
    {
        [JsonProperty("Id")]
        public string AppId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<AppIngredient> Ingredients { get; set; }

        public AppRecipeBase ToBase(string icon)
        {
            AppRecipeBase baseObj = new AppRecipeBase
            {
                AppId = AppId,
                Icon = icon,
                Ingredients = Ingredients
            };
            return baseObj;
        }

        public AppRecipeLang ToLang()
        {
            AppRecipeLang baseObj = new AppRecipeLang
            {
                AppId = AppId,
                Title = Title,
                Description = Description
            };
            return baseObj;
        }
    }

    public class AppRecipeBase
    {
        [JsonProperty("Id")]
        public string AppId { get; set; }
        public string Icon { get; set; }
        public List<AppIngredient> Ingredients { get; set; }
    }

    public class AppIngredient
    {
        [JsonProperty("Id")]
        public string AppId { get; set; }
        public int Quantity { get; set; }
    }

    public class AppRecipeLang
    {
        [JsonProperty("Id")]
        public string AppId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
