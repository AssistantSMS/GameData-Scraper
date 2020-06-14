using AssistantScrapMechanic.Domain.GameFiles;

namespace AssistantScrapMechanic.Domain.IntermediateFiles
{
    public class BlockLocalised
    {
        public string ItemId { get; set; }
        public string AppId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string PhysicsMaterial { get; set; }
        public Ratings Ratings { get; set; }
        public bool Flammable { get; set; }
        public float Density { get; set; }
        public int QualityLevel { get; set; }
    }
}
