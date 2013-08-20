using System;
using System.Collections.Generic;
using System.Text;

namespace ParserLibrary.ItemObject
{
    public class ItemValueAffectedNextItemLength : Item
    {
        private RegularItem countedItem;
        
        public void setAffectedItem(RegularItem RI){
            countedItem = RI;
        }
        protected override string getValue()
        {
            if (countedItem == null)
            {
                return "00";
            }
            if (countedItem.Value == null)
            {
                return "00";
            }
            return (countedItem.Value.Length / 2).ToString("X2");
        }

        protected override void setValue(string val)
        {
            // didn't do anything
        }

        public override bool canBeDisplayed()
        {
            return false;
        }
    }
}
