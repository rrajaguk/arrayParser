using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MultiIMSIInstallParameter.Item;

namespace MultiIMSIInstallParameter.Parsers
{
    public class PhysicalFileParser : Parser
    {
        public string ParserName;
        public PhysicalFileParser(string fileLocation)
        {
            //set file name
            ParserName = Path.GetFileNameWithoutExtension(fileLocation);

            // parse the content
            ItemParam = new List<ItemRepresentation>();
            ItemRepresentation prev = null;
            using (StreamReader sr = new StreamReader(fileLocation))
            {
                string line = " ";
                while (line != null)
                {
                    // do the checking of string
                    line = sr.ReadLine();
                    if (line == null)
                        continue;
                    line = line.Trim();
                    if (line.Length <= 0 || line[0] == '#')
                    {
                        continue;
                    }
                    if ((line[0] == '>') && (prev != null))
                    {
                        CompositeItem CI = new CompositeItem();
                        int index = line.IndexOf(',');
                        CI.name = line.Substring(1, index - 1);
                        CI.location = int.Parse(line.Substring(index + 1, line.Length - index - 1));
                        prev.compositeValues.setItem(CI);
                        continue;
                    }
                    prev = null;
                    ItemRepresentation current = new ItemRepresentation();
                    // process the configuration name
                    int SeparatorIndex = line.IndexOf(',');
                    current.ItemName = line.Substring(0, SeparatorIndex);
                    line = line.Substring(SeparatorIndex + 1, line.Length - SeparatorIndex - 1);

                    // process the configuration length
                    SeparatorIndex = line.IndexOf(',');
                    string tempLengthValueHolder;
                    if (SeparatorIndex < 0)
                    {
                        tempLengthValueHolder = line;
                        current.lengthType = ItemRepresentation.LengthType.independent;
                    }
                    else
                    {
                        tempLengthValueHolder = line.Substring(0, SeparatorIndex);

                        string typeOfEntry = line.Substring(SeparatorIndex + 1, line.Length - SeparatorIndex - 1).Trim();
                        switch (typeOfEntry)
                        {
                            case "N":
                                current.valueType = ItemRepresentation.ValueType.normal;
                                current.lengthType = ItemRepresentation.LengthType.affectNext;
                                break;
                            case "C":
                                current.valueType = ItemRepresentation.ValueType.composite;
                                current.compositeValues = new CompositeValue();
                                prev = current;
                                break;
                        }
                    }
                    int lengthValue = Int32.Parse(tempLengthValueHolder);
                    current.ItemLength = lengthValue;
                    // process the configuration next

                    ItemParam.Add((current));
                }
            }
        }
    }
}
