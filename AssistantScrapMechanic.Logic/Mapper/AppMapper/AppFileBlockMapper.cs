using System.Collections.Generic;
using AssistantScrapMechanic.Domain.AppFiles;
using AssistantScrapMechanic.Domain.IntermediateFiles;

namespace AssistantScrapMechanic.Logic.Mapper.AppMapper
{
    public static class AppFileBlockMapper
    {
        public static AppGameItem ToAppFile(GameItemLocalised localisedData)
        {
            AppGameItem recipe = new AppGameItem
            {
                AppId = localisedData.AppId,
                Flammable = localisedData.Flammable,
                PhysicsMaterial = localisedData.PhysicsMaterial,
                QualityLevel = localisedData.QualityLevel,
                Density = localisedData.Density,
                Title = localisedData.Name,
                Color = localisedData.Color,
                Ratings = localisedData.Ratings,
            };
            return recipe;
        }
    }
}
