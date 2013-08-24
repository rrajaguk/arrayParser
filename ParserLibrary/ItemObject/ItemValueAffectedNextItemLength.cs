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
        protected override int getLength()
        {
            return 1;
        }
        protected override void setLength(int val)
        {
            // do nothing
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
            //countedItem.Length = countedItem.Value.Length;
            return (countedItem.Length ).ToString("X2");
        }

        protected override void setValue(string val)
        {
            if (countedItem != null)
            {
                // set the affected item length, but keep it;s own length
                countedItem.Length = Convert.ToInt32(val, 16);
            }
        }

    }
}
