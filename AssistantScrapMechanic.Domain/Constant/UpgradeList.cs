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

        public static List<Upgrade> SportSuspensionUpgrades = new List<Upgrade>
        {
            new Upgrade
            {
                Uuid = "67da25c9-3825-41f6-9724-4546a11cb2a5",
                LocaleKey = "level2",
                TargetUuid = "aae686a2-0eb3-43b3-b998-def282de79e9",
                Cost = 1,
            },
            new Upgrade
            {
                Uuid = "aae686a2-0eb3-43b3-b998-def282de79e9",
                LocaleKey = "level3",
                TargetUuid = "d0aa2676-5266-432a-bf7e-3887e6ddedd5",
                Cost = 2,
            },
            new Upgrade
            {
                Uuid = "d0aa2676-5266-432a-bf7e-3887e6ddedd5",
                LocaleKey = "level4",
                TargetUuid = "d9adddcc-972d-4726-a376-67f950b99a44",
                Cost = 4,
            },
            new Upgrade
            {
                Uuid = "d9adddcc-972d-4726-a376-67f950b99a44",
                LocaleKey = "level5",
                TargetUuid = "52855106-a95c-4427-9970-3f227109b66d",
                Cost = 5,
            }
        };

        public static List<Upgrade> OffroadSuspensionUpgrades = new List<Upgrade>
        {
            new Upgrade
            {
                Uuid = "f3cfef9d-faef-4be8-9283-476eb99614d7",
                LocaleKey = "level2",
                TargetUuid = "00284190-1484-4286-a198-b2ddef768c2e",
                Cost = 1,
            },
            new Upgrade
            {
                Uuid = "00284190-1484-4286-a198-b2ddef768c2e",
                LocaleKey = "level3",
                TargetUuid = "a9658eaf-0dd8-46a6-8cac-be6978f19b79",
                Cost = 2,
            },
            new Upgrade
            {
                Uuid = "a9658eaf-0dd8-46a6-8cac-be6978f19b79",
                LocaleKey = "level4",
                TargetUuid = "4c3f6a7c-45c6-4ed8-bf13-c247c3db6b81",
                Cost = 4,
            },
            new Upgrade
            {
                Uuid = "4c3f6a7c-45c6-4ed8-bf13-c247c3db6b81",
                LocaleKey = "level5",
                TargetUuid = "73f838db-783e-4a41-bc0f-9008967780f3",
                Cost = 5,
            }
        };

        public static List<Upgrade> ThrusterUpgrades = new List<Upgrade>
        {
            new Upgrade
            {
                Uuid = "df8528ed-15ad-4a39-a33a-698880684001",
                LocaleKey = "level2",
                TargetUuid = "9fc793b2-250b-40ab-bcb3-97cf97c7b481",
                Cost = 10,
            },
            new Upgrade
            {
                Uuid = "9fc793b2-250b-40ab-bcb3-97cf97c7b481",
                LocaleKey = "level3",
                TargetUuid = "4c1cc8de-7af1-4f8e-a5c4-c583460af9e5",
                Cost = 10,
            },
            new Upgrade
            {
                Uuid = "4c1cc8de-7af1-4f8e-a5c4-c583460af9e5",
                LocaleKey = "level4",
                TargetUuid = "e6db321c-6f98-47f6-9f7f-4e6794a62cb8",
                Cost = 10,
            },
            new Upgrade
            {
                Uuid = "e6db321c-6f98-47f6-9f7f-4e6794a62cb8",
                LocaleKey = "level5",
                TargetUuid = "a736ffdf-22c1-40f2-8e40-988cab7c0559",
                Cost = 10,
            }
        };

        public static List<Upgrade> ControllerUpgrades = new List<Upgrade>
        {
            new Upgrade
            {
                Uuid = "598d865c-324c-4129-9c57-21a6abd2cb2e",
                LocaleKey = "level2",
                TargetUuid = "1872d83a-d1a1-4cb7-ad46-9e4468d2548c",
                Cost = 2,
            },
            new Upgrade
            {
                Uuid = "1872d83a-d1a1-4cb7-ad46-9e4468d2548c",
                LocaleKey = "level3",
                TargetUuid = "6bb84152-c4d7-4644-bc37-a3becd79298d",
                Cost = 3,
            },
            new Upgrade
            {
                Uuid = "6bb84152-c4d7-4644-bc37-a3becd79298d",
                LocaleKey = "level4",
                TargetUuid = "2354cd24-3dd3-4db5-84ab-df64c32d2c72",
                Cost = 5,
            },
            new Upgrade
            {
                Uuid = "2354cd24-3dd3-4db5-84ab-df64c32d2c72",
                LocaleKey = "level5",
                TargetUuid = "a092359d-5cea-484d-a274-470d9a567632",
                Cost = 10,
            },
        };

        public static List<Upgrade> SensorUpgrades = new List<Upgrade>
        {
            new Upgrade
            {
                Uuid = "1d4793af-cb66-4628-804a-9d7404712643",
                LocaleKey = "level2",
                TargetUuid = "cf46678b-c947-4267-ba85-f66930f5faa4",
                Cost = 1,
            },
            new Upgrade
            {
                Uuid = "cf46678b-c947-4267-ba85-f66930f5faa4",
                LocaleKey = "level3",
                TargetUuid = "90fc3603-3544-4254-97ef-ea6723510961",
                Cost = 1,
            },
            new Upgrade
            {
                Uuid = "90fc3603-3544-4254-97ef-ea6723510961",
                LocaleKey = "level4",
                TargetUuid = "de018bc6-1db5-492c-bfec-045e63f9d64b",
                Cost = 1,
            },
            new Upgrade
            {
                Uuid = "de018bc6-1db5-492c-bfec-045e63f9d64b",
                LocaleKey = "level5",
                TargetUuid = "20dcd41c-0a11-4668-9b00-97f278ce21af",
                Cost = 3,
            }
        };

        public static List<Upgrade> PistonUpgrades = new List<Upgrade>
        {
            new Upgrade
            {
                Uuid = "8c741785-5eae-4c48-9f99-d62bf522a83f",
                LocaleKey = "level2",
                TargetUuid = "31f14f52-f4d8-4b9f-9d6e-7412497c9284",
                Cost = 2,
            },
            new Upgrade
            {
                Uuid = "31f14f52-f4d8-4b9f-9d6e-7412497c9284",
                LocaleKey = "level3",
                TargetUuid = "46396518-8c29-4da9-81bb-a020f4baf5b2",
                Cost = 2,
            },
            new Upgrade
            {
                Uuid = "46396518-8c29-4da9-81bb-a020f4baf5b2",
                LocaleKey = "level4",
                TargetUuid = "7324219e-2b19-4098-baa3-9876984ead08",
                Cost = 2,
            },
            new Upgrade
            {
                Uuid = "7324219e-2b19-4098-baa3-9876984ead08",
                LocaleKey = "level5",
                TargetUuid = "2f004fdf-bfb0-46f3-a7ac-7711100bee0c",
                Cost = 2,
            }
        };


        public static List<Upgrade> Upgrades = DriverSeatUpgrades
            .Concat(SeatUpgrades)
            .Concat(DriverSaddleUpgrades)
            .Concat(SaddleUpgrades)
            .Concat(ElectricEngineUpgrades)
            .Concat(GasEngineUpgrades)
            .Concat(SportSuspensionUpgrades)
            .Concat(OffroadSuspensionUpgrades)
            .Concat(ThrusterUpgrades)
            .Concat(ControllerUpgrades)
            .Concat(SensorUpgrades)
            .Concat(PistonUpgrades)
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
