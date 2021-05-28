namespace AssistantScrapMechanic.Domain.GameFiles
{
    public partial class PackingStationItem
    {
        public string projectileName { get; set; }
        public int fullAmount { get; set; }
        public string shape { get; set; }
        public string effect { get; set; }
    }

    public partial class PackingStationItem
    {
        public string CrateGuid => shape;
    }
}
