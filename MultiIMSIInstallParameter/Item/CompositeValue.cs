using System;
using System.Collections.Generic;
using System.Text;

namespace MultiIMSIInstallParameter.Item
{
    public class CompositeItem
    {
        public int location { get; set; }
        public string name { get; set; }
        public bool isChecked { get; set; }

    }
    public class CompositeValue
    {
        private List<CompositeItem> Items{ get; set; }
        public CompositeValue()
        {
            Items = new List<CompositeItem>();
        }
        public List<CompositeItem> getItems()
        {
            return Items;
        }
        public void setItem(CompositeItem CI)
        {
            Items.Add(CI);
        }
    }
}
