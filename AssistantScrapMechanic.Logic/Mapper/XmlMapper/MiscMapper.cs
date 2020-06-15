using System.Xml;
using AssistantScrapMechanic.Domain.Result;

namespace AssistantScrapMechanic.Logic.Mapper.XmlMapper
{
    public static class MiscMapper
    {
        public static ResultWithValue<XmlNodeList> GetSubProperty(this XmlNodeList list, string prop)
        {
            foreach (XmlNode property in list)
            {
                for (int attriIndex = 0; attriIndex < property.Attributes.Count; attriIndex++)
                {
                    XmlAttribute propertyAttribute = property.Attributes[attriIndex];
                    if (!IsCorrectAttribute(propertyAttribute, prop, attriIndex, property.Attributes.Count + 2)) continue;

                    return new ResultWithValue<XmlNodeList>(true, property.ChildNodes, string.Empty);
                }
            }

            return new ResultWithValue<XmlNodeList>(false, null, "Item not found");
        }

        public static ResultWithValue<XmlAttribute> GetValueAttribute(this XmlNodeList list, string prop)
        {
            foreach (XmlNode property in list)
            {
                for (int attriIndex = 0; attriIndex < property.Attributes.Count; attriIndex++)
                {
                    ResultWithValue<XmlAttribute> attr = property.GetAttribute(prop);
                    if (attr.HasFailed) continue;

                    XmlAttribute siblingPropertyAttribute = property.Attributes[attriIndex + 1];

                    return new ResultWithValue<XmlAttribute>(true, siblingPropertyAttribute, string.Empty);
                }
            }

            return new ResultWithValue<XmlAttribute>(false, null, "Attribute not found");
        }

        public static ResultWithValue<XmlAttribute> GetAttribute(this XmlNode item, string prop)
        {
            for (int attriIndex = 0; attriIndex < item.Attributes.Count; attriIndex++)
            {
                XmlAttribute propertyAttribute = item.Attributes[attriIndex];
                if (!IsCorrectAttribute(propertyAttribute, prop, attriIndex, item.Attributes.Count)) continue;

                return new ResultWithValue<XmlAttribute>(true, propertyAttribute, string.Empty);
            }

            return new ResultWithValue<XmlAttribute>(false, null, "Attribute not found");
        }

        public static bool GetBool(this XmlNodeList list, string prop)
        {
            ResultWithValue<XmlAttribute> attributeResult = list.GetValueAttribute(prop);
            if (attributeResult.HasFailed) return false;
            if (attributeResult.Value?.Value == null) return false;

            return attributeResult.Value.Value.Equals("True");
        }

        public static string GetString(this XmlNodeList list, string prop)
        {
            ResultWithValue<XmlAttribute> attributeResult = list.GetValueAttribute(prop);
            if (attributeResult.HasFailed) return string.Empty;
            if (attributeResult.Value?.Value == null) return string.Empty;

            return attributeResult.Value.Value;
        }

        public static string GetName(this XmlNode item)
        {
            for (int attriIndex = 0; attriIndex < item.Attributes.Count; attriIndex++)
            {
                XmlAttribute propertyAttribute = item.Attributes[attriIndex];
                if (!propertyAttribute.Name.Equals("name")) continue;
                if (propertyAttribute.Value == null) continue;

                return propertyAttribute.Value;
            }

            return string.Empty;
        }

        public static string GetPoint(this XmlNode item)
        {
            for (int attriIndex = 0; attriIndex < item.Attributes.Count; attriIndex++)
            {
                XmlAttribute propertyAttribute = item.Attributes[attriIndex];
                if (!propertyAttribute.Name.Equals("point")) continue;
                if (propertyAttribute.Value == null) continue;

                return propertyAttribute.Value;
            }

            return string.Empty;
        }

        public static string GetStringSubProperty(this XmlNodeList list, string prop, string subProp)
        {
            foreach (XmlNode property in list)
            {
                for (int attriIndex = 0; attriIndex < property.Attributes.Count; attriIndex++)
                {
                    XmlAttribute propertyAttribute = property.Attributes[attriIndex];
                    if (!IsCorrectAttribute(propertyAttribute, prop, attriIndex, property.Attributes.Count)) continue;

                    foreach (XmlNode subProperty in property.ChildNodes)
                    {
                        for (int subPropAttriIndex = 0; subPropAttriIndex < subProperty.Attributes.Count; subPropAttriIndex++)
                        {
                            XmlAttribute subPropertyAttribute = subProperty.Attributes[subPropAttriIndex];
                            if (IsCorrectAttribute(subPropertyAttribute, subProp, subPropAttriIndex, subProperty.Attributes.Count))
                            {
                                XmlAttribute siblingPropertyAttribute = subProperty.Attributes[attriIndex + 1];
                                return siblingPropertyAttribute.Value;
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }

        public static decimal GetDecimal(this XmlNodeList list, string prop)
        {
            foreach (XmlNode property in list)
            {
                for (int attriIndex = 0; attriIndex < property.Attributes.Count; attriIndex++)
                {
                    XmlAttribute propertyAttribute = property.Attributes[attriIndex];
                    if (!IsCorrectAttribute(propertyAttribute, prop, attriIndex, property.Attributes.Count)) continue;

                    XmlAttribute siblingPropertyAttribute = property.Attributes[attriIndex + 1];
                    decimal.TryParse(siblingPropertyAttribute.Value, out decimal parsedValue);
                    return parsedValue;
                }
            }

            return 0;
        }
        public static int GetInt(this XmlNodeList list, string prop)
        {
            foreach (XmlNode property in list)
            {
                for (int attriIndex = 0; attriIndex < property.Attributes.Count; attriIndex++)
                {
                    XmlAttribute propertyAttribute = property.Attributes[attriIndex];
                    if (!IsCorrectAttribute(propertyAttribute, prop, attriIndex, property.Attributes.Count)) continue;

                    XmlAttribute siblingPropertyAttribute = property.Attributes[attriIndex + 1];
                    int.TryParse(siblingPropertyAttribute.Value, out int parsedValue);
                    return parsedValue;
                }
            }

            return 0;
        }


        public static bool IsCorrectAttribute(XmlAttribute attribute, string prop, int currentIndex, int totalIndex)
        {
            if (!attribute.Value.Equals(prop))
            {
                return false;
            }

            if (currentIndex + 1 >= totalIndex)
            {
                return false;
            }
            return true;
        }
    }
}
