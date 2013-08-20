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

			string line = " ";
			while (line != null)
			{
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

				string[] lines = line.Split(',');
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

                Item currentItem = null;
                // check the item type
                if (lines.Length <= 2)
                {
                    currentItem = new RegularItem();
                    if (prev != null && (prev is ItemValueAffectedNextItemLength))
                    {
                        (prev as ItemValueAffectedNextItemLength).setAffectedItem((RegularItem)currentItem);
                    }
                    prev = null;
                }
                else
                {
                    switch (lines[2].ToUpper())
                    {
                        case "N":
                            currentItem = new ItemValueAffectedNextItemLength();
                            prev = currentItem;
                            break;
                        case "C":
                            currentItem = new ItemComposite();
                            prev = (ItemComposite)currentItem;
                            break;
                        case "P":
                            currentItem = new RegularItem();
                            currentItem.setDisplayerDecorator(new Display_OtherItemNotNull(result, lines[3]));
                            break;

                    }
                }

                // set the item value
                currentItem.Name = lines[0];
                currentItem.Length = int.Parse(lines[1]);
                result.Add(currentItem);

			}
			return result;
		}
	}
}
