using System;
using System.Collections.Generic;
using System.Text;

namespace ParserLibrary.ItemObject.Decorator
{
    public abstract class ItemDecorator : Item
    {

        protected Item decoratedItem;
        protected ItemDecorator(Item IT)
        {
            this.decoratedItem = IT;
        }
        public Item getBaseClass()
        {
            ItemDecorator item = decoratedItem as ItemDecorator;
            if (item == null){
                return decoratedItem;
            }
            return item.getBaseClass();
        }
        protected override void setLength(int val)
        {
            decoratedItem.Length = val;
        }
        protected override int getLength()
        {
            return decoratedItem.Length;
        }

        protected override void setValue(string val)
        {
            decoratedItem.Value = val;
        }

        protected override void setName(string value)
        {
            decoratedItem.Name = value;
        }
        protected override string getName()
        {
            return decoratedItem.Name;
        }
    }
}
