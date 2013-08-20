using System;
using System.Collections.Generic;
using System.Text;
using ParserLibrary.ItemObject;

namespace ParserLibrary.Decorator
{
    public class Display_OtherItemNotNull : DisplayDecorator
    {

        private List<Item> listOfItems;
        private string DependentItemName;
        public Display_OtherItemNotNull(List<Item> listOfItems, string itemName)
        {
            this.listOfItems = listOfItems;
            DependentItemName = itemName;
        }
        public override bool includedInResult()
        {
            Item foundItem = null;
            foreach (var item in listOfItems)
            {
                if (item.Name.Equals(DependentItemName)){
                    foundItem = item;
                    break;   
                }
            }
            // check the data, if null means the item couldn't be found, then display the string
            if (foundItem == null)
            {
                return true;
            }
            // if the item value equal to 00 the hide the result;
            return !(foundItem.Value == "00");
        }
    }
}
