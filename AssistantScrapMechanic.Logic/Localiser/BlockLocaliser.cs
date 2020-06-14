using AssistantScrapMechanic.Domain.GameFiles;
using AssistantScrapMechanic.Domain.IntermediateFiles;

namespace AssistantScrapMechanic.Logic.Localiser
{
    public static class BlockLocaliser
    {
        public static BlockLocalised Localise(this Blocks block, string prefix, int index)
        {
            BlockLocalised blockLocalised = new BlockLocalised
            {
                AppId = $"{prefix}{(index + 1)}",
                ItemId = block.Uuid,
                Color = block.Color,
                Density = block.Density,
                Flammable = block.Flammable,
                Name = block.Name,
                PhysicsMaterial = block.PhysicsMaterial,
                QualityLevel = block.QualityLevel,
                Ratings = block.Ratings,
            };
            return blockLocalised;
        }
    }
}
