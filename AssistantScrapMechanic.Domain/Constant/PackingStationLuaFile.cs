using System.Collections.Generic;
using System.Linq;
using AssistantScrapMechanic.Domain.GameFiles;

namespace AssistantScrapMechanic.Domain.Constant
{
    // Scrap Mechanic\Survival\Scripts\game\interactables\PackingStationScreen.lua
    public static class PackingStationLuaFile
    {
        private static string obj_crates_banana = "0bc74539-df8a-47c7-aad8-d55d809a01e4";
        private static string obj_crates_blueberry = "e77d9577-589a-446b-96c1-f6d0d7495489";
        private static string obj_crates_orange = "c10a77d5-3357-4cb4-8113-a2cbe69c7ff2";
        private static string obj_crates_pineapple = "bc69cb3b-7e0c-4c36-805d-f8d89fcfced3";
        private static string obj_crates_carrot = "9cd8288c-5a19-479f-af47-9eb55230ade2";
        private static string obj_crates_redbeet = "628fd350-577d-413f-82a8-7f08a83de3d8";
        private static string obj_crates_tomato = "1dcd74ca-39ba-4b00-a36a-3381b25055f4";
        private static string obj_crates_broccoli = "99477093-e819-4199-b62a-fda6143aae89";

        public static Dictionary<string, string> NameToGuidDictionary = new Dictionary<string, string>
        {
            { "banana", "aa4c9c5e-7fc6-4c27-967f-c550e551c872" },
            { "blueberry", "6a43fff2-8c6d-4460-9f44-e5483b5267dd" },
            { "orange", "f5098301-1693-457b-8efc-83b3504105ac" },
            { "pineapple", "4ec64cda-1a5b-4465-88b4-5ea452c4a556" },
            { "carrot", "47ece75a-bfca-4e8a-b618-4f609fcea0da" },
            { "redbeet", "4ce00048-f735-4fab-b978-5f405e60f48f" },
            { "tomato", "6d92d8e7-25e9-4698-b83d-a64dc97978c8" },
            { "broccoli", "b5cdd503-fe1c-482b-86ab-6a5d2cc4fc8f" },
            { "potato", "bfcfac34-db0f-42d6-bd0c-74a7a5c95e82" },
        };

		public static List<PackingStationItem> VeggieList = new List<PackingStationItem>
        {
            new PackingStationItem { projectileName = "broccoli", fullAmount = 10, shape = obj_crates_broccoli, effect = "Packingstation - Brocolicrate" },
            new PackingStationItem { projectileName = "carrot", fullAmount = 10, shape = obj_crates_carrot, effect = "Packingstation - Carrotcrate" },
            new PackingStationItem { projectileName = "redbeet", fullAmount = 10, shape = obj_crates_redbeet, effect = "Packingstation - Redbeetcrate" },
			new PackingStationItem { projectileName = "tomato", fullAmount = 10, shape = obj_crates_tomato, effect = "Packingstation - Tomatocrate" }
		};

        public static List<PackingStationItem> FruitList = new List<PackingStationItem> 
        {
            new PackingStationItem { projectileName = "banana", fullAmount = 10, shape = obj_crates_banana, effect = "Packingstation - Bananacrate" },
            new PackingStationItem { projectileName = "blueberry", fullAmount = 10, shape = obj_crates_blueberry, effect = "Packingstation - Blueberrycrate" },
            new PackingStationItem { projectileName = "orange", fullAmount = 10, shape = obj_crates_orange, effect = "Packingstation - Orange" },
            new PackingStationItem { projectileName = "pineapple", fullAmount = 10, shape = obj_crates_pineapple, effect = "Packingstation - Pineapplecrate" }
        };

        public static List<PackingStationItem> AllItems = VeggieList.Union(FruitList).ToList();

        /*
         * 
local veggie_dataset = {
	{ projectileName = "broccoli", fullAmount = 10, shape = obj_crates_broccoli, effect = "Packingstation - Brocolicrate" },
	{ projectileName = "carrot", fullAmount = 10, shape = obj_crates_carrot, effect = "Packingstation - Carrotcrate" },
	{ projectileName = "redbeet", fullAmount = 10, shape = obj_crates_redbeet, effect = "Packingstation - Redbeetcrate" },
	{ projectileName = "tomato", fullAmount = 10, shape = obj_crates_tomato, effect = "Packingstation - Tomatocrate" }
}

local fruit_dataset = {
	{ projectileName = "banana", fullAmount = 10, shape = obj_crates_banana, effect = "Packingstation - Bananacrate" },
	{ projectileName = "blueberry", fullAmount = 10, shape = obj_crates_blueberry, effect = "Packingstation - Blueberrycrate" },
	{ projectileName = "orange", fullAmount = 10, shape = obj_crates_orange, effect = "Packingstation - Orange" },
	{ projectileName = "pineapple", fullAmount = 10, shape = obj_crates_pineapple, effect = "Packingstation - Pineapplecrate" }
}
         */
    }
}
