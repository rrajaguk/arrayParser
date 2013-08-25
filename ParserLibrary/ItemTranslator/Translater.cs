using System;
using System.Collections.Generic;
using System.Text;
using ParserLibrary.ItemObject;
using ParserLibrary.ItemRep;

namespace ParserLibrary.TranslateFactory
{
    public abstract class Translater
    {
        protected ItemParser parser;
        public void setParser(ItemParser parser)
        {
            this.parser = parser;
        }
        public abstract void Export();
        public abstract void Import();

    }
}
