using System;
using System.Collections.Generic;
using System.Text;
using ParserLibrary.Decorator;

namespace ParserLibrary.ItemObject
{
    public abstract class Item
    {
        public string Name { get; set; }
        public int Length { 
            get { return getLength(); } 
            set { setLength(value); } }

        public string Value { 
            get { return getValue();}
            set { setValue(value); }
        }

        private DisplayDecorator displayDecorator;

        private int length;
        protected virtual int getLength()
        {
            return length;
        }
        protected virtual void setLength(int val)
        {
            length = val;
        }

        protected  abstract string getValue();
        protected abstract void setValue(string val);
        

        public void setDisplayerDecorator( DisplayDecorator disp)
        {
            displayDecorator = disp;
        }

        [Obsolete("will be obseleted soon")]
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

        public virtual int fecthValue(string val, int startingPosition)
        {
            Value = val.Substring(startingPosition, Length * 2);
            return startingPosition + Length * 2;
        }


    }
}
