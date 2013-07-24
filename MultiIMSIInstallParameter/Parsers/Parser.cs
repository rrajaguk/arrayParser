using System;
using System.Collections.Generic;
using System.Text;
using MultiIMSIInstallParameter.Item;

namespace MultiIMSIInstallParameter.Parsers
{
    public abstract class Parser
    {
        protected List<ItemRepresentation> ItemParam;
        public List<ItemRepresentation> getItems()
        {
            return ItemParam;
        }
        public void Parse(string value)
        {
            List<ItemTranslation> result = new List<ItemTranslation>();
            int stringPosition = 0;
            for (int a = 0; a < ItemParam.Count; a++)
            {
                ItemRepresentation currentItemRepresentate = ItemParam[a];
                //currentItem.Description = currentItemRepresentate.ItemName;

                if (stringPosition + currentItemRepresentate.ItemLength * 2 <= value.Length)
                {
                    ItemParam[a].ItemValue= value.Substring(stringPosition, currentItemRepresentate.ItemLength * 2);
                    if (ItemParam[a].ItemValue.Length < 1)
                    {
                        ItemParam[a].ItemValue = ItemTranslation.NOT_DEFINED;
                    }
                }

                if (currentItemRepresentate.lengthType == ItemRepresentation.LengthType.affectNext)
                {
                    ItemParam[a + 1].ItemLength = Convert.ToInt32(ItemParam[a].ItemValue, 16);
                }
                stringPosition += currentItemRepresentate.ItemLength * 2;

            }
            //return result;
        }

    }
}
