using System.Collections.Generic;
using AssistantScrapMechanic.Domain.GameFiles;
using AssistantScrapMechanic.Domain.IntermediateFiles;

namespace AssistantScrapMechanic.Logic.Localiser
{
    public static class GameItemLocaliser
    {
        public static GameItemLocalised Localise(this GameItem gameItem, string prefix, int index, Dictionary<string, InventoryDescription> itemNames)
        {
            GameItemLocalised blockLocalised = new GameItemLocalised
            {
                AppId = $"{prefix}{(index + 1)}",
                ItemId = gameItem.Uuid,
                Color = gameItem.Color,
                Density = gameItem.Density,
                Flammable = gameItem.Flammable,
                Name = itemNames.GetTitle(gameItem.Uuid),
                Description = itemNames.GetDescription(gameItem.Uuid),
                PhysicsMaterial = gameItem.PhysicsMaterial,
                QualityLevel = 0,
                Ratings = gameItem.Ratings,
                Box = gameItem.Box ?? new Box(),
            };
            return blockLocalised;
        }
    }
}
