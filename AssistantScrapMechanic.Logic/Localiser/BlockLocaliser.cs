using AssistantScrapMechanic.Domain.GameFiles;
using AssistantScrapMechanic.Domain.IntermediateFiles;
using System.Collections.Generic;

namespace AssistantScrapMechanic.Logic.Localiser
{
    public static class BlockLocaliser
    {
        public static GameItemLocalised Localise(this Blocks block, string prefix, int index, Dictionary<string, InventoryDescription> itemNames)
        {
            GameItemLocalised blockLocalised = new GameItemLocalised
            {
                AppId = $"{prefix}{(index + 1)}",
                GameName = block.Name,
                ItemId = block.Uuid,
                Color = block.Color,
                Flammable = block.Flammable,
                IsCreative = block.Name.Contains("creative", System.StringComparison.InvariantCultureIgnoreCase),
                Name = itemNames.GetTitle(block.Uuid),
                PhysicsMaterial = block.PhysicsMaterial,
                Ratings = block.Ratings,
                Box = null,
                Cylinder = null,
            };
            return blockLocalised;
        }
    }
}
