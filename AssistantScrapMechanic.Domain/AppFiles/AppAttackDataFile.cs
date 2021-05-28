using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.Domain.AppFiles
{
    public class AppAttackDataFile
    {
        [JsonIgnore]
        public string GameId { get; set; }
        
        public string AppId { get; set; }

        public List<AppAttackTypeWithHitChances> AttackHitChances { get; set; }
    }

    public class AppAttackTypeWithHitChances
    {
        public string Type { get; set; }

        public List<AppAttackHitChance> HitChances { get; set; }
    }

    public class AppAttackHitChance
    {
        public decimal Chance { get; set; }

        public int QualityLevel { get; set; }
    }
}
