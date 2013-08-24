using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ParserLibrary.Decorator;
using ParserLibrary.ItemObject;

namespace ParserLibrary.ItemFactory
{
	public class NormalItemFactory : ItemFactory
	{
        public StreamReader dataSource;
        public NormalItemFactory(StreamReader sr)
        {
            this.dataSource = sr;
        }
		public override List<ItemObject.Item> GetItems()
		{
            List<Item> result = new List<Item>();
			Item prev = null;
            Item prevTemp = null;

			string line = " ";
            int defaultValuePosition;
			while (line != null)
			{
                // setting of default value
                defaultValuePosition = 3;
                Item currentItem = null;
                prevTemp = null;
                
				// do the checking of string
				line = dataSource.ReadLine();
				if (line == null)
					continue;
				line = line.Trim();
				
                // check for comment or empty line
				if (line.Length <= 0 || line[0] == '#')
				{
					continue;
				}

                // chop the string into several string separated by ',' (comma)
				string[] lines = line.Split(',');

                // remove the ending and trailing space
				for(int i = 0;i< lines.Length;i++){
					lines[i] = lines[i].Trim();
				}

				// if there is a composite item detected at the previous attempt then start adding it's member
				if ((line[0] == '>') && (prev != null) && (prev is ItemComposite))
				{
					BitItem BI= new BitItem();
					BI.name = lines[0].Substring(1);
					BI.location = int.Parse(lines[1]);
					(prev as ItemComposite).addBitItem(BI);
					continue;
				}

                // check the item type
                if (lines.Length <= 2)
                {
                    currentItem = new RegularItem();
                //    if (prev != null && (prev is ItemValueAffectedNextItemLength))
                //    {
                //        (prev as ItemValueAffectedNextItemLength).setAffectedItem((RegularItem)currentItem);
                //    }
                //    prev = null;
                }
                else
                {
                    switch (lines[2].ToUpper())
                    {
                        case "N":
                            currentItem = new ItemValueAffectedNextItemLength();
                            prevTemp = currentItem;
                            break;
                        case "C":
                            currentItem = new ItemComposite();
                            prevTemp= (ItemComposite)currentItem;
                            break;
                        case "P":
                            currentItem = new RegularItem();
                            currentItem.setDisplayerDecorator(new Display_OtherItemNotNull(result, lines[3]));
                            defaultValuePosition = 4;
                            break;
                        default :
                            currentItem = new RegularItem();
                            defaultValuePosition = 2;
                            break;

                    }
                }
                // get the default value (in case it exist)
                if (lines.Length >= defaultValuePosition + 1)
                {
                    currentItem.Value = lines[defaultValuePosition];
                }

                // set the item name and length
                currentItem.Name = lines[0];
                currentItem.Length = int.Parse(lines[1]);
                result.Add(currentItem);


                // set the relationship
                if (prev != null)
                {
                    if (prev is ItemValueAffectedNextItemLength)
                    {
                        (prev as ItemValueAffectedNextItemLength).setAffectedItem((RegularItem)currentItem);
                    }
                    prevTemp = null;
                }
                prev = prevTemp;

			}
			return result;
		}
	}
}
