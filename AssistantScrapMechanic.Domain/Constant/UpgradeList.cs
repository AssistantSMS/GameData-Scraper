using System.Collections.Generic;
using System.Linq;

namespace AssistantScrapMechanic.Domain.Constant
{
    public static class UpgradeList
    {
        public static List<Upgrade> DriverSeatUpgrades = new List<Upgrade>
        {
            new Upgrade
            {
                Uuid = "77c2687c-2e13-4df8-996a-96fb26d75ee0",
                LocaleKey = "level2",
                TargetUuid = "efbf45f8-62ec-4541-9eb1-d529966f6a29",
                Cost = 1,
            },
            new Upgrade
            {
                Uuid = "efbf45f8-62ec-4541-9eb1-d529966f6a29",
                LocaleKey = "level3",
                TargetUuid = "c3ef3008-9367-4ab7-813a-24195d63e5a3",
                Cost = 2,
            },
            new Upgrade
            {
                Uuid = "c3ef3008-9367-4ab7-813a-24195d63e5a3",
                LocaleKey = "level4",
                TargetUuid = "d30dcd12-ec39-43b9-a115-44c08e1b9091",
                Cost = 3,
            },
            new Upgrade
            {
                Uuid = "d30dcd12-ec39-43b9-a115-44c08e1b9091",
                LocaleKey = "level5",
                TargetUuid = "ffa3a47e-fc0d-4977-802f-bd15683bbe5c",
                Cost = 5,
            }
        };

        public static List<Upgrade> DriverSaddleUpgrades = new List<Upgrade>
        {
            new Upgrade
            {
                Uuid = "7d601a5a-796d-4cae-be88-b47479d38d11",
                LocaleKey = "level2",
                TargetUuid = "bb2ed406-f0d3-4fd6-b3f9-7caadfa8e4e4",
                Cost = 1,
            },
            new Upgrade
            {
                Uuid = "bb2ed406-f0d3-4fd6-b3f9-7caadfa8e4e4",
                LocaleKey = "level3",
                TargetUuid = "6953b17e-0a38-4107-8c56-5ee97e68bee3",
                Cost = 2,
            },
            new Upgrade
            {
                Uuid = "6953b17e-0a38-4107-8c56-5ee97e68bee3",
                LocaleKey = "level4",
                TargetUuid = "41960868-6245-47b5-97c4-f446e199812f",
                Cost = 3,
            },
            new Upgrade
            {
                Uuid = "41960868-6245-47b5-97c4-f446e199812f",
                LocaleKey = "level5",
                TargetUuid = "9dd1ccea-1e44-430d-b706-3ff45416583e",
                Cost = 5,
            }
        };

        public static List<Upgrade> SeatUpgrades = new List<Upgrade>
        {
            new Upgrade
            {
                Uuid = "3b972f2f-30c7-4a5e-a100-5e257e62295d",
                LocaleKey = "level2",
                TargetUuid = "2b9c6e87-1b75-4a57-8979-74d9f95668ba",
                Cost = 1,
            },
            new Upgrade
            {
                Uuid = "2b9c6e87-1b75-4a57-8979-74d9f95668ba",
                LocaleKey = "level3",
                TargetUuid = "ebe2782e-a4f5-4d91-83cc-db110179393b",
                Cost = 1,
            },
            new Upgrade
            {
                Uuid = "ebe2782e-a4f5-4d91-83cc-db110179393b",
                LocaleKey = "level4",
                TargetUuid = "46465697-ed36-4720-ba8a-08c568b4e36c",
                Cost = 1,
            },
            new Upgrade
            {
                Uuid = "46465697-ed36-4720-ba8a-08c568b4e36c",
                LocaleKey = "level5",
                TargetUuid = "703ca746-d802-4e76-b443-4881e83afb73",
                Cost = 1,
            }
        };

        public static List<Upgrade> SaddleUpgrades = new List<Upgrade>
        {
            new Upgrade
            {
                Uuid = "2d3016f7-febe-416e-93bc-41d80ca3910d",
                LocaleKey = "level2",
                TargetUuid = "bb2ed406-f0d3-4fd6-b3f9-7caadfa8e4e4",
                Cost = 1,
            },
            new Upgrade
            {
                Uuid = "bb2ed406-f0d3-4fd6-b3f9-7caadfa8e4e4",
                LocaleKey = "level3",
                TargetUuid = "6953b17e-0a38-4107-8c56-5ee97e68bee3",
                Cost = 2,
            },
            new Upgrade
            {
                Uuid = "6953b17e-0a38-4107-8c56-5ee97e68bee3",
                LocaleKey = "level4",
                TargetUuid = "41960868-6245-47b5-97c4-f446e199812f",
                Cost = 3,
            },
            new Upgrade
            {
                Uuid = "41960868-6245-47b5-97c4-f446e199812f",
                LocaleKey = "level5",
                TargetUuid = "9dd1ccea-1e44-430d-b706-3ff45416583e",
                Cost = 5,
            }
        };

        public static List<Upgrade> ElectricEngineUpgrades = new List<Upgrade>
        {
            new Upgrade
            {
                Uuid = "5e5d231e-405e-4f45-9bd0-b3557dbb42eb",
                LocaleKey = "level2",
                TargetUuid = "0c9cc5bb-af2f-4023-b8d8-cd7d52a60efe",
                Cost = 4,
            },
            new Upgrade
            {
                Uuid = "0c9cc5bb-af2f-4023-b8d8-cd7d52a60efe",
                LocaleKey = "level3",
                TargetUuid = "56cea967-a685-494d-85ef-3aa121a0c193",
                Cost = 6,
            },
            new Upgrade
            {
                Uuid = "56cea967-a685-494d-85ef-3aa121a0c193",
                LocaleKey = "level4",
                TargetUuid = "5e57e0f7-e87c-4269-b274-146fe40e1b44",
                Cost = 8,
            },
            new Upgrade
            {
                Uuid = "5e57e0f7-e87c-4269-b274-146fe40e1b44",
                LocaleKey = "level5",
                TargetUuid = "22f3e797-82f5-4819-a085-c3cc28ec9025",
                Cost = 10,
            }
        };

        public static List<Upgrade> GasEngineUpgrades = new List<Upgrade>
        {
            new Upgrade
            {
                Uuid = "1bfccc0a-828f-475c-882c-87d5a96054c9",
                LocaleKey = "level2",
                TargetUuid = "33d01ddd-f32b-4a9a-87d6-efb6710b389c",
                Cost = 4,
            },
            new Upgrade
            {
                Uuid = "33d01ddd-f32b-4a9a-87d6-efb6710b389c",
                LocaleKey = "level3",
                TargetUuid = "470b9a92-ed94-4ef2-b1ea-b45f47ef0982",
                Cost = 6,
            },
            new Upgrade
            {
                Uuid = "470b9a92-ed94-4ef2-b1ea-b45f47ef0982",
                LocaleKey = "level4",
                TargetUuid = "bfcaac1a-5a7f-4fba-9980-1159617a7212",
                Cost = 8,
            },
            new Upgrade
            {
                Uuid = "bfcaac1a-5a7f-4fba-9980-1159617a7212",
                LocaleKey = "level5",
                TargetUuid = "3091926a-9340-46d9-83d6-4fd7c68ad950",
                Cost = 10,
            },
        };

        public static List<Upgrade> Upgrades = DriverSeatUpgrades
            .Concat(SeatUpgrades)
            .Concat(DriverSaddleUpgrades)
            .Concat(SaddleUpgrades)
            .Concat(ElectricEngineUpgrades)
            .Concat(GasEngineUpgrades)
            .ToList();
    }

    public class Upgrade
    {
        public string Uuid { get; set; }
        public string LocaleKey { get; set; }
        public string TargetUuid { get; set; }
        public int Cost { get; set; }
    }
}
