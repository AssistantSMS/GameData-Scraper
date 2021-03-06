﻿using System;
using System.Collections.Generic;
using System.Linq;
using AssistantScrapMechanic.Domain.Enum;
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

                CustomisationSourceType tier = CustomisationSourceType.Unknown;
                if (!string.IsNullOrEmpty(customisedOption.Tier))
                {
                    if (customisedOption.Tier.Equals("common", StringComparison.InvariantCultureIgnoreCase)) tier = CustomisationSourceType.Common;
                    if (customisedOption.Tier.Equals("rare", StringComparison.InvariantCultureIgnoreCase)) tier = CustomisationSourceType.Rare;
                    if (customisedOption.Tier.Equals("epic", StringComparison.InvariantCultureIgnoreCase)) tier = CustomisationSourceType.Epic;
                }

                string groupName = customised.Name.Substring(0, 3);
                optionsLocalised.Add(new CustomisationItemLocalised
                {
                    AppId = $"{prefix}{groupName}M{(cusOptIndex + 1)}",
                    ItemId = $"{customisedOption.Uuid}_male",
                    Name = name,
                    Tier = tier,
                });
                optionsLocalised.Add(new CustomisationItemLocalised
                {
                    AppId = $"{prefix}{groupName}F{(cusOptIndex + 1)}",
                    ItemId = $"{customisedOption.Uuid}_female",
                    Name = name,
                    Tier = tier,
                });
            }

            CustomisationLocalised blockLocalised = new CustomisationLocalised
            {
                Category = customised.Name,
                Items = optionsLocalised,
            };
            return blockLocalised;
        }

        public static CustomisationLocalised ReLocalise(this CustomisationLocalised gameItem, Dictionary<string, InventoryDescription> itemNames)
        {
            gameItem.Items = gameItem.Items.Select(gi => ReLocaliseItem(gi, itemNames)).ToList();
            return gameItem;
        }

        public static CustomisationItemLocalised ReLocaliseItem(this CustomisationItemLocalised gameItem, Dictionary<string, InventoryDescription> itemNames)
        {
            string locName = itemNames.GetTitle(gameItem.ItemId);
            if (!string.IsNullOrEmpty(locName))
            {
                gameItem.Name = locName;
            }

            return gameItem;
        }
    }
}
