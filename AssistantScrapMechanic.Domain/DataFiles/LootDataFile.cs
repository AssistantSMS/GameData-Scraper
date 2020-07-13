using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.Domain.DataFiles
{
    public class LootDataFile
    {
        [JsonProperty("lootTables")]
        public LootTable LootTable { get; set; }

        [JsonProperty("lootQuantitiesLookup")]
        public List<LootQuantitiesLookup> LootQuantitiesLookup { get; set; }

        [JsonProperty("ruinChest")]
        public LootContainer RuinChest { get; set; }

        [JsonProperty("lootCrate")]
        public LootContainer LootCrate { get; set; }

        [JsonProperty("lootCrateEpic")]
        public LootContainer LootCrateEpic { get; set; }

        [JsonProperty("lootCrateEpicWareHouse")]
        public LootContainer LootCrateEpicWareHouse { get; set; }
    }

    public class LootTable
    {
        [JsonProperty("randomLoot")]
        public List<LootChance> RandomLoot { get; set; }

        [JsonProperty("randomEpicLoot")]
        public List<LootChance> RandomEpicLoot { get; set; }

        [JsonProperty("randomLootWarehouse")]
        public List<LootChance> RandomLootWarehouse { get; set; }
    }

    public class LootChance
    {
        [JsonProperty("gameName")]
        public string GameName { get; set; }

        [JsonProperty("chance")]
        public int Chance { get; set; }

        [JsonProperty("quantity")]
        public string QuantityKey { get; set; }
    }
    
    public class LootQuantitiesLookup
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("min")]
        public int Min { get; set; }

        [JsonProperty("max")]
        public int Max { get; set; }
    }

    public class LootContainer
    {
        [JsonProperty("selectOne")]
        public List<LootChance> SelectOne { get; set; }

        [JsonProperty("randomLoot")]
        public string RandomLootType { get; set; }
    }
}
