using System;
using System.Collections.Generic;
using System.Text;

namespace ParserLibrary.ItemObject
{
    public class BitItem
    {
        public int location { get; set; }
        public string name { get; set; }
        public bool isChecked { get; set; }

    }
    public class ItemComposite : Item
    {
        private List<BitItem> Items { get; set; }

        public ItemComposite()
        {
            Items = new List<BitItem>();
        }
        

        public void addBitItem(BitItem val)
        {
            Items.Add(val);
        }

        public List<BitItem> getItems()
        {
            return Items;
        }

        protected override string getValue()
        {
            int val = 0;
            foreach (var item in Items)
            {
                if (item.isChecked)
                {
                    val += (int)Math.Pow(2, item.location - 1);
                }
            }
            return val.ToString("X2");
        }

        protected override void setValue(string val)
        {
            short sb = short.Parse(val, System.Globalization.NumberStyles.HexNumber);
            for (int i = 0; i < Items.Count; i++)
            {
                BitItem currentItem = Items[i];
                short interestedBit = (short)(1 << (currentItem.location - 1));
                currentItem.isChecked = ((sb & interestedBit) == interestedBit);
                
            }
        }

    }
}
