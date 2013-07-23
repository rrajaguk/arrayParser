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
            using(StreamReader sr = new StreamReader(fileLocation))
            {
                string line = " ";
                while(line != null)
                {
                    // do the checking of string
                    line = sr.ReadLine();
                    if (line == null)
                        continue;
                    line = line.Trim();
                    if (line.Length <=0 || line[0] == '#')
                    {
                        continue;
                    }
                    ItemRepresentation current = new ItemRepresentation();
                    // process the configuration name
                    int SeparatorIndex = line.IndexOf(',');
                    current.ItemName = line.Substring(0, SeparatorIndex);
                    line = line.Substring(SeparatorIndex + 1, line.Length - SeparatorIndex-1);

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
                        current.lengthType = ItemRepresentation.LengthType.affectNext;
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
