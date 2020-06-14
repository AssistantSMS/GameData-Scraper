using System.Collections.Generic;
using AssistantScrapMechanic.Domain.IntermediateFiles;

namespace AssistantScrapMechanic.Logic.Localiser
{
    public static class MiscLocaliser
    {
        public static InventoryDescription GetInventoryDescription(this Dictionary<string, InventoryDescription> itemNames, string key)
        {
            if (itemNames.ContainsKey(key))
            {
                return itemNames[key];
            }

            return new InventoryDescription
            {
                Title = string.Empty,
                Description= string.Empty,
            };
        }
        public static string GetTitle(this Dictionary<string, InventoryDescription> itemNames, string key) => itemNames.GetInventoryDescription(key).Title;
        public static string GetDescription(this Dictionary<string, InventoryDescription> itemNames, string key) => itemNames.GetInventoryDescription(key).Description;
    }
}
