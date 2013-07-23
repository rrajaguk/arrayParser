using System;
using System.Collections.Generic;
using System.Text;

namespace MultiIMSIInstallParameter.Item
{
    public class ItemRepresentation
    {
        public enum LengthType
        {
            independent = 1,
            affectNext = 2
        } ;
        public string ItemName;
        public int ItemLength;
        public LengthType lengthType;
        public ItemRepresentation()
        {
            
        }
        public ItemRepresentation(string itemVal, int itemLengthVal, LengthType LT)
        {
            ItemName = itemVal;
            ItemLength = itemLengthVal;
            lengthType = LT;
        }
    }
}
