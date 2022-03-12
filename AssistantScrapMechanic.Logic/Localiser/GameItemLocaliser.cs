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
                GameName = gameItem.Name,
                Color = gameItem.Color,
                Tiling = gameItem.Tiling,
                Flammable = gameItem.Flammable,
                Name = itemNames.GetTitle(gameItem.Uuid),
                Description = itemNames.GetDescription(gameItem.Uuid),
                PhysicsMaterial = gameItem.PhysicsMaterial,
                Ratings = gameItem.Ratings,
                Box = gameItem.Box,
                Cylinder = gameItem.Cylinder,
                StackSize = gameItem.StackSize,
                Edible = gameItem.Edible,
            };

            List<int> boxSizes = GetSizesFromBox(blockLocalised.Box);
            List<int> hullSizes = GetSizesFromBox(gameItem.Hull);
            if (hullSizes.Count > boxSizes.Count)
            {
                blockLocalised.Box = gameItem.Hull;
            }

            return blockLocalised;
        }

        public static GameItemLocalised ReLocalise(this GameItemLocalised gameItem, Dictionary<string, InventoryDescription> itemNames)
        {
            string locName = itemNames.GetTitle(gameItem.ItemId);
            if (!string.IsNullOrEmpty(locName))
            {
                gameItem.Name = locName;
            }

            string locDescription = itemNames.GetDescription(gameItem.ItemId);
            if (!string.IsNullOrEmpty(locDescription))
            {
                gameItem.Description = locDescription;
            }

            return gameItem;
        }

        private static List<int> GetSizesFromBox(Box box)
        {
            List<int> sizes = new List<int>();
            if (box == null) return sizes;
            if (box.X > 0) sizes.Add(box.X);
            if (box.Y > 0) sizes.Add(box.Y);
            if (box.Z > 0) sizes.Add(box.Z);

            return sizes;
        }
    }
}
