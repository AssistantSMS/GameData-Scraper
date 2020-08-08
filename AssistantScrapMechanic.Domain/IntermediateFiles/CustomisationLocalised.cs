using System.Collections.Generic;
using AssistantScrapMechanic.Domain.Enum;

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
        public CustomisationSourceType Tier { get; set; }
    }
}
