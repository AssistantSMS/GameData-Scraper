using AssistantScrapMechanic.Domain.GameFiles;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.Domain.AppFiles
{
    public class AppBlock
    {
        [JsonProperty("Id")]
        public string AppId { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public string PhysicsMaterial { get; set; }
        public Ratings Ratings { get; set; }
        public bool Flammable { get; set; }
        public float Density { get; set; }
        public int QualityLevel { get; set; }

        public AppBlockBase ToBase(string icon)
        {
            AppBlockBase baseObj = new AppBlockBase
            {
                AppId = AppId,
                Icon = icon,
                Color = Color,
                Flammable = Flammable,
                PhysicsMaterial = PhysicsMaterial,
                QualityLevel = QualityLevel,
                Density = Density,
                Ratings = Ratings
            };
            return baseObj;
        }

        public AppBlockLang ToLang()
        {
            AppBlockLang baseObj = new AppBlockLang
            {
                AppId = AppId,
                Title = Title,
            };
            return baseObj;
        }
    }

    public class AppBlockBase
    {
        [JsonProperty("Id")]
        public string AppId { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public string PhysicsMaterial { get; set; }
        public Ratings Ratings { get; set; }
        public bool Flammable { get; set; }
        public float Density { get; set; }
        public int QualityLevel { get; set; }
    }

    public class AppBlockLang
    {
        [JsonProperty("Id")]
        public string AppId { get; set; }
        public string Title { get; set; }
    }
}
