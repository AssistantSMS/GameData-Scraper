﻿using System.Collections.Generic;
using System.Linq;
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
                Box = localisedData.Box,
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
                QualityLevel = 0,
                Density = 0,
                Title = localisedData.Name,
                Description = category,
                Color = string.Empty,
                Ratings = null,
                Box = null,
            };
            return recipe;
        }
    }
}
