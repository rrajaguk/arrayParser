using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ParserLibrary.ItemObject;

namespace ParserLibrary.ItemFactory
{
    public abstract class ItemFactory
    {
        public abstract List<Item> GetItems();
    }
}
