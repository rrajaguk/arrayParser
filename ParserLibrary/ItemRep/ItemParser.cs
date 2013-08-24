using System;
using System.Collections.Generic;
using System.Text;
using MultiIMSIInstallParameter.Item;
using ParserLibrary.ItemObject;

namespace ParserLibrary.ItemRep
{
    public class ItemParser
    {
        public string Name { get; set; }
        public ItemFactory.ItemFactory Factory { get; set; }
        public TranslateFactory.TranslateFactory Translator { get; private set; }
        public List<Item> Items{get;set;}
        public void setFactory(ItemFactory.ItemFactory factory)
        {
            this.Factory = factory;
            Items = Factory.GetItems();
        }
        public void setTranslator(TranslateFactory.TranslateFactory factory)
        {
            factory.setParser(this);
            this.Translator = factory;
        }
        public void Parse(string value)
        {
            int stringPosition = 0;
            for (int a = 0; a < Items.Count; a++)
            {
                Item currentItemRepresentate = Items[a];
                //currentItem.Description = currentItemRepresentate.ItemName;

                if (stringPosition + currentItemRepresentate.Length* 2 <= value.Length)
                {
                    Items[a].Value= value.Substring(stringPosition, currentItemRepresentate.Length* 2);
                    if (Items[a].Value.Length < 1)
                    {
                        Items[a].Value = ItemTranslation.NOT_DEFINED;
                    }
                }

                if (currentItemRepresentate is ItemValueAffectedNextItemLength)
                {
                    Items[a + 1].Length= Convert.ToInt32(Items[a].Value, 16);
                }
                stringPosition += currentItemRepresentate.Length* 2;

            }
            //return result;
        }
    }
}
