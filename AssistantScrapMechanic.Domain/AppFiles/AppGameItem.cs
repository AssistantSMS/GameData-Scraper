using System.Collections.Generic;
using AssistantScrapMechanic.Domain.Constant;
using AssistantScrapMechanic.Domain.GameFiles;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.Domain.AppFiles
{
    public class AppGameItem
    {
        [JsonProperty("Id")]
        public string AppId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string PhysicsMaterial { get; set; }
        public Ratings Ratings { get; set; }
        public Box Box { get; set; }
        public Cylinder Cylinder { get; set; }
        public bool Flammable { get; set; }
        public decimal Density { get; set; }
        public int QualityLevel { get; set; }

        public Upgrade Upgrades { get; set; }
        public List<Feature> Features { get; set; }

        public AppGameItemBase ToBase(string icon)
        {
            AppGameItemBase baseObj = new AppGameItemBase
            {
                AppId = AppId,
                Icon = icon,
                Color = Color,
                Flammable = Flammable,
                PhysicsMaterial = PhysicsMaterial,
                QualityLevel = QualityLevel,
                Density = Density,
                Ratings = Ratings,
                Box = Box,
                Cylinder = Cylinder,
                Upgrades = Upgrades,
                Features = Features,
            };
            return baseObj;
        }

        public AppGameItemLang ToLang()
        {
            AppGameItemLang baseObj = new AppGameItemLang
            {
                AppId = AppId,
                Title = Title,
                Description = Description,
            };
            return baseObj;
        }
    }

    public class AppGameItemBase
    {
        [JsonProperty("Id")]
        public string AppId { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public string PhysicsMaterial { get; set; }
        public Ratings Ratings { get; set; }
        public Box Box { get; set; }
        public Cylinder Cylinder { get; set; }
        public bool Flammable { get; set; }
        public decimal Density { get; set; }
        public int QualityLevel { get; set; }
        public Upgrade Upgrades { get; set; }
        public List<Feature> Features { get; set; }
    }

    public class AppGameItemLang
    {
        [JsonProperty("Id")]
        public string AppId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
