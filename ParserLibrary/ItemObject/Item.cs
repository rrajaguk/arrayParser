using System;
using System.Collections.Generic;
using System.Text;
using ParserLibrary.Decorator;

namespace ParserLibrary.ItemObject
{
    public abstract class Item
    {
        public string Name { get; set; }
        public int Length { get; set; }

        public string Value { 
            get { return getValue();}
            set { setValue(value); }
        }

        private DisplayDecorator displayDecorator;

        protected  abstract string getValue();
        protected abstract void setValue(string val);
        

        public void setDisplayerDecorator( DisplayDecorator disp)
        {
            displayDecorator = disp;
        }

        public virtual bool canBeDisplayed()
        {
            return true;
        }
        public bool includedInResult()
        {

            if (displayDecorator == null)
            {
                return true;
            }

            return displayDecorator.includedInResult();
        }
    }
}
