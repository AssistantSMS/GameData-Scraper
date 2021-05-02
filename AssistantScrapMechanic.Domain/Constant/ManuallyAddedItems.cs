using System.Collections.Generic;

namespace AssistantScrapMechanic.Domain.Constant
{
    public static class ManuallyAddedItems
    {
        public static List<NotFoundItem> Tools = new List<NotFoundItem>
		{
            new NotFoundItem
            {
                ItemId = "bb641a4f-e391-441c-bc6d-0ae21a069476", // Hammer
			},
			new NotFoundItem
			{
				ItemId = "8f190ce2-3a59-423e-8483-a7aa67bd5bc0", // Lift
			},
            new NotFoundItem
            {
                ItemId = "3384010e-bc1c-42bb-83ef-dbc78a1f9636", // Handbook
            },
        };
    }
}
