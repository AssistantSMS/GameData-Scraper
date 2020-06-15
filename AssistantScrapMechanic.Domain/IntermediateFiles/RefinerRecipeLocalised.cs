using System;
using System.Collections.Generic;
using System.Text;

namespace AssistantScrapMechanic.Domain.IntermediateFiles
{
    public class RefinerRecipeLocalised
    {
        public string AppId { get; set; }
        public string ItemId { get; set; }
        public string Title { get; set; }
        public List<IngredientListLocalised> IngredientListLocalised { get; set; }
    }
}
