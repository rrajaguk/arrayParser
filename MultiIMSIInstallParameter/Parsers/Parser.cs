using System;
using System.Collections.Generic;
using System.Text;
using MultiIMSIInstallParameter.Item;

namespace MultiIMSIInstallParameter.Parsers
{
    public abstract class Parser
    {
        protected List<ItemRepresentation> ItemParam;
        public List<MultiIMSIInstallParameter.Item.ItemTranslation> Parse(string value)
        {
            List<ItemTranslation> result = new List<ItemTranslation>();
            int stringPosition = 0;
            int nextStringPosition = 0;
            for (int a = 0; a < ItemParam.Count; a++)
            {
                ItemTranslation currentItem = new ItemTranslation();
                ItemRepresentation currentItemRepresentate = ItemParam[a];
                currentItem.Description = currentItemRepresentate.ItemName;

                if (stringPosition + currentItemRepresentate.ItemLength * 2 <= value.Length)
                {
                    currentItem.Value = value.Substring(stringPosition, currentItemRepresentate.ItemLength * 2);
                    if (currentItem.Value.Length <1 )
                    {
                        currentItem.Value = ItemTranslation.NOT_DEFINED;
                    }
                }

                if (currentItemRepresentate.lengthType == ItemRepresentation.LengthType.affectNext)
                {
                    ItemParam[a + 1].ItemLength = Convert.ToInt32(currentItem.Value, 16);
                }
                stringPosition += currentItemRepresentate.ItemLength * 2;

                result.Add(currentItem);
            }
            return result;
        }

    }
}
