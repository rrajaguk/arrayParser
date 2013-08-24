using System;
using System.Collections.Generic;
using System.Text;

namespace ParserLibrary.ItemObject.Decorator
{
    public class OtherItemNotNull_Decorator : ItemDecorator
    {
        private Item DependItem;
        public OtherItemNotNull_Decorator(Item basicItem,Item DependItem): base(basicItem)
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
        
        public override int fecthValue(string val, int startingPosition)
        {
            if (DependItem.Value != "00")
            {
                return decoratedItem.fecthValue(val, startingPosition);
            }
            return startingPosition;
        }
    }
}
