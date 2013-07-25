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
        public enum ValueType
        {
            normal = 1,
            composite = 2
        }
        public ValueType valueType { get; set; }
        public string ItemName  { get; set; }
        public int ItemLength { get; set; }
        public LengthType lengthType { get; set; }
        public string ItemValue { get; set; }
        public CompositeValue compositeValues { get; set; }
        public ItemRepresentation()
        {
            ItemValue = "";
            valueType = ValueType.normal;
            compositeValues = null;
        }
        
        public ItemRepresentation(string itemVal, int itemLengthVal, LengthType LT)
        {
            valueType = ValueType.normal;
            ItemValue = "";
            ItemName = itemVal;
            ItemLength = itemLengthVal;
            lengthType = LT;
            compositeValues = null;
        }
    }
}
