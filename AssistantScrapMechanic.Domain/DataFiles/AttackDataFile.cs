using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.Domain.DataFiles
{
    public class AttackDataFile
    {
        [JsonProperty("Sledgehammer")]
        public DamageStruct Sledgehammer { get; set; }

        [JsonProperty("ToteBotAttack")]
        public DamageStruct ToteBotAttack { get; set; }

        [JsonProperty("ToteBotWhip")]
        public DamageStruct ToteBotWhip { get; set; }

        [JsonProperty("HaybotPitchfork")]
        public DamageStruct HaybotPitchfork { get; set; }

        [JsonProperty("HaybotPitchforkSwipe")]
        public DamageStruct HaybotPitchforkSwipe { get; set; }

        [JsonProperty("FarmBotSwipe")]
        public DamageStruct FarmbotSwipe { get; set; }

        [JsonProperty("FarmBotBreach")]
        public DamageStruct FarmbotBreach { get; set; }

        [JsonProperty("FarmBotStep")]
        public DamageStruct FarmbotStep { get; set; }
    }

    public class DamageStruct
    {
        [JsonProperty("destruction")]
        public Destruction Destruction { get; set; }

        [JsonProperty("hitBox")]
        public HitBox HitBox { get; set; }

        [JsonProperty("hitSphere")]
        public HitSphere HitSphere { get; set; }

        [JsonProperty("impactEffect")]
        public string ImpactEffect { get; set; }
    }

    public class Destruction
    {
        [JsonProperty("destructionLevels")]
        public List<DestructionLevel> DestructionLevels { get; set; }
    }

    public class DestructionLevel
    {
        [JsonProperty("qualityLevel")]
        public int QualityLevel { get; set; }

        [JsonProperty("chance")]
        public decimal Chance { get; set; }
    }

    public class HitSphere
    {
        [JsonProperty("radius")]
        public decimal Radius { get; set; }
    }
    

    public class HitBox
    {
        [JsonProperty("halfWidth")]
        public decimal HalfWidth { get; set; }

        [JsonProperty("halfHeight")]
        public decimal HalfHeight { get; set; }
    }
}
