using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssistantScrapMechanic.Domain.DataFiles;

namespace AssistantScrapMechanic.Logic.Calculator
{
    public static class LootCalculator
    {
        public static List<string> GetListOfGameNames(List<LootChance> lootChances)
        {
            HashSet<string> gameNames = new HashSet<string>();
            foreach (LootChance lootChance in lootChances)
            {
                gameNames.Add(lootChance.GameName);
            }

            return gameNames.ToList();
        }
        public static int TotalChanceValue(List<LootChance> lootChances)
        {
            int totalChance = 0;
            foreach (LootChance lootChance in lootChances)
            {
                totalChance += lootChance.Chance;
            }

            return totalChance;
        }
    }
}
