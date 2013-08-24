using System;
using System.Collections.Generic;
using System.Text;

namespace ParserLibrary.ItemObject
{
    public class OtherItemNotNull_Decorator : Item
    {
        private Item decoratedItem;
        private Item DependItem;
        public OtherItemNotNull_Decorator(Item basicItem,Item DependItem)
        {
            this.decoratedItem = basicItem;
            this.DependItem = DependItem;
            // copy default value
            this.Name = basicItem.Name;
            this.Length = basicItem.Length;
        }
        protected override string getValue()
        {
            // if the item value equal to 00 the hide the result;
            if (DependItem.Value == "00")
            {
                return string.Empty;
            }
            return decoratedItem.Value;
        }

        protected override void setValue(string val)
        {
            decoratedItem.Value = val;
        }
    }
}
