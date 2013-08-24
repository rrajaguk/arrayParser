﻿using System;
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
    }
}
