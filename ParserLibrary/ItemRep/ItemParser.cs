using System;
using System.Collections.Generic;
using System.Text;
using ParserLibrary.ItemObject;

namespace ParserLibrary.ItemRep
{
    public class ItemParser
    {
        public string Name { get; set; }
        public ItemFactory.ItemFactory Factory { get; set; }
        public List<Item> Items{get;set;}
        public void setFactory(ItemFactory.ItemFactory factory)
        {
            this.Factory = factory;
            Items = Factory.GetItems();
        }
    }
}
