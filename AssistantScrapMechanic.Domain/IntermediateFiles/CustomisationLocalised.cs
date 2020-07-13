using System.Collections.Generic;

namespace AssistantScrapMechanic.Domain.IntermediateFiles
{
    public class CustomisationLocalised
    {
        public string Category { get; set; }
        public List<CustomisationItemLocalised> Items { get; set; }
    }

    public class CustomisationItemLocalised: ILocalised
    {
        public string ItemId { get; set; }
        public string AppId { get; set; }
        public string Name { get; set; }
    }
}
