using System.Collections.Generic;
using AssistantScrapMechanic.Domain.AppFiles;
using AssistantScrapMechanic.Domain.Constant;
using AssistantScrapMechanic.Domain.IntermediateFiles;

namespace AssistantScrapMechanic.Logic.Mapper.AppMapper
{
    public static class AppFileBlockMapper
    {
        public static AppGameItem ToAppFile(GameItemLocalised localisedData, List<Feature> features, Upgrade upgrades)
        {
            AppGameItem recipe = new AppGameItem
            {
                AppId = localisedData.AppId,
                Flammable = localisedData.Flammable,
                PhysicsMaterial = localisedData.PhysicsMaterial,
                Title = localisedData.Name,
                Color = localisedData.Color,
                Ratings = localisedData.Ratings,
                Box = localisedData.Box,
                Cylinder = localisedData.Cylinder,
                Features = features,
                Upgrade = upgrades,
            };
            return recipe;
        }
        
        public static AppGameItem ToAppFile(CustomisationItemLocalised localisedData, string category)
        {
            AppGameItem recipe = new AppGameItem
            {
                AppId = localisedData.AppId,
                Flammable = false,
                PhysicsMaterial = "",
                Title = localisedData.Name,
                Description = category,
                Color = string.Empty,
                Ratings = null,
                Box = null,
                CustomisationSource = localisedData.Tier,
            };
            return recipe;
        }
    }
}
