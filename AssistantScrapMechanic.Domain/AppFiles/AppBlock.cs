using AssistantScrapMechanic.Domain.GameFiles;

namespace AssistantScrapMechanic.Domain.AppFiles
{
    public class AppBlock
    {
        public string AppId { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public string PhysicsMaterial { get; set; }
        public Ratings Ratings { get; set; }
        public bool Flammable { get; set; }
        public float Density { get; set; }
        public int QualityLevel { get; set; }
    }
}
