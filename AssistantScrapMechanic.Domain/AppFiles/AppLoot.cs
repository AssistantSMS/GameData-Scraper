
using System.Collections.Generic;
using AssistantScrapMechanic.Domain.Enum;

namespace AssistantScrapMechanic.Domain.AppFiles
{
    public class AppLoot
    {
        public string AppId { get; set; }
        public List<AppLootChance> Chances { get; set; }
    }

    public class AppLootChance
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Chance { get; set; }
        public AppLootContainerType Type { get; set; }
    }
}
