using AssistantScrapMechanic.Domain.GameFiles;

namespace AssistantScrapMechanic.Domain.IntermediateFiles
{
    public class GameItemLocalised: ILocalised
    {
        public string ItemId { get; set; }
        public string GameName { get; set; }
        public string AppId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string PhysicsMaterial { get; set; }
        public Ratings Ratings { get; set; }
        public Box Box { get; set; }
        public Cylinder Cylinder { get; set; }
        public bool Flammable { get; set; }
    }
}
