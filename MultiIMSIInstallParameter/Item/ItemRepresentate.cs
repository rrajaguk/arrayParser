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
        public string ItemName  { get; set; }
        public int ItemLength { get; set; }
        public LengthType lengthType { get; set; }
        public string ItemValue { get; set; }
        public ItemRepresentation()
        {
            ItemValue = "";
            
        }
        public ItemRepresentation(string itemVal, int itemLengthVal, LengthType LT)
        {
            ItemValue = "";
            ItemName = itemVal;
            ItemLength = itemLengthVal;
            lengthType = LT;
        }
    }
}
