using System.Collections.Generic;
using System.Xml;
using AssistantScrapMechanic.Domain.GameFiles;

namespace AssistantScrapMechanic.Logic.Mapper.XmlMapper
{
    public static class IconMapMapper
    {
        public static List<IconMapCoordinates> MapImageCoordinates(this XmlNodeList list)
        {
            List<IconMapCoordinates> coords = new List<IconMapCoordinates>();

            foreach (XmlNode item in list)
            {
                string name = item.GetName();
                if (string.IsNullOrEmpty(name)) continue;

                string pointsString = item.ChildNodes[0].GetPoint();
                string[] points = pointsString.Split(" ");
                if (points.Length != 2) continue;

                coords.Add(new IconMapCoordinates
                {
                    ItemId = name,
                    X = int.Parse(points[0]),
                    Y = int.Parse(points[1]),
                });
            }

            return coords;
        }
    }
}
