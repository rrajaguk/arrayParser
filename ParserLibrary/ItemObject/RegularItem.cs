using System;
using System.Collections.Generic;
using System.Text;

namespace ParserLibrary.ItemObject
{
    public class RegularItem : Item
    {
        private string value;
        protected override string getValue()
        {
            return value;
        }

        protected override void setValue(string val)
        {
            value = val;
        }

        public override bool canBeDisplayed()
        {
            return true;
        }
    }
}
