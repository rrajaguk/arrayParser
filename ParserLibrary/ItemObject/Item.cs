using System;
using System.Collections.Generic;
using System.Text;

namespace ParserLibrary.ItemObject
{
    public abstract class Item
    {
        private string _name;
        public string Name {
            get { return getName(); }
            set { setName(value); } 
        }

        protected virtual void setName(string value)
        {
            _name = value;
        }

        protected virtual string getName()
        {
            return _name;
        }
        public int Length { 
            get { return getLength(); } 
            set { setLength(value); } }

        public string Value { 
            get { return getValue();}
            set { setValue(value); }
        }

        
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


        public virtual bool canBeDisplayed()
        {
            return true;
        }        

        public virtual int fecthValue(string val, int startingPosition)
        {
            Value = val.Substring(startingPosition, Length * 2);
            return startingPosition + Length * 2;
        }


    }
}
