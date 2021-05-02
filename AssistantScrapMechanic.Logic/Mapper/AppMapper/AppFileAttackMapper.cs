using System;
using System.Collections.Generic;
using System.Linq;
using AssistantScrapMechanic.Domain.AppFiles;
using AssistantScrapMechanic.Domain.DataFiles;

namespace AssistantScrapMechanic.Logic.Mapper.AppMapper
{
    public static class AppFileAttackMapper
    {
        public static AppAttackTypeWithHitChances MapToAppAttackHitChances(string type, DamageStruct damage)
        {
            decimal totalChance = damage.Destruction.DestructionLevels.Sum(dl => dl.Chance);

            List<AppAttackHitChance> hitChances = new List<AppAttackHitChance>();
            foreach (DestructionLevel destructionDestructionLevel in damage.Destruction.DestructionLevels)
            {
                if (destructionDestructionLevel.Chance == 0) continue;
                int percentChance = Convert.ToInt32(destructionDestructionLevel.Chance / totalChance * 10000); // Round to 2 decimal

                hitChances.Add(new AppAttackHitChance
                {
                    Chance = percentChance / 100.0M,
                    QualityLevel = destructionDestructionLevel.QualityLevel,
                });
            }

            return new AppAttackTypeWithHitChances
            {
                Type = type,
                HitChances = hitChances.OrderByDescending(hc => hc.Chance).ToList()
            };
        }
    }
}
