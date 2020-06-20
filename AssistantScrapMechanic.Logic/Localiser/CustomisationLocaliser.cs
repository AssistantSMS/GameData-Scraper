using System.Collections.Generic;
using AssistantScrapMechanic.Domain.GameFiles;
using AssistantScrapMechanic.Domain.IntermediateFiles;

namespace AssistantScrapMechanic.Logic.Localiser
{
    public static class CustomisationLocaliser
    {
        public static CustomisationLocalised Localise(this CustomizationListItem customised, string prefix, int index, Dictionary<string, InventoryDescription> itemNames)
        {
            List<CustomisationItemLocalised> optionsLocalised = new List<CustomisationItemLocalised>();
            for (int cusOptIndex = 0; cusOptIndex < customised.Options.Count; cusOptIndex++)
            {
                Option customisedOption = customised.Options[cusOptIndex];
                string name = itemNames.GetTitle(customisedOption.Uuid);
                if (string.IsNullOrEmpty(name)) name = customised.Name;

                string groupName = customised.Name.Substring(0, 3);
                optionsLocalised.Add(new CustomisationItemLocalised
                {
                    AppId = $"{prefix}{groupName}M{(cusOptIndex + 1)}",
                    ItemId = $"{customisedOption.Uuid}_male",
                    //ItemId = customisedOption.Uuid,
                    Name = name,
                });
                optionsLocalised.Add(new CustomisationItemLocalised
                {
                    AppId = $"{prefix}{groupName}F{(cusOptIndex + 1)}",
                    ItemId = $"{customisedOption.Uuid}_female",
                    Name = name,
                });
            }

            CustomisationLocalised blockLocalised = new CustomisationLocalised
            {
                Category = customised.Name,
                Items = optionsLocalised,
            };
            return blockLocalised;
        }
    }
}
