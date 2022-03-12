using System.Collections.Generic;

namespace AssistantScrapMechanic.Domain.DataFiles
{
    public class AppDevDetailFile
    {
        public string AppId { get; set; }

        public List<AppDevDetailItem> Details { get; set; }
    }

    public class AppDevDetailItem
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
