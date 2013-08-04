using System;
using System.Collections.Generic;
using System.Text;
using MultiIMSIInstallParameter.Item;

namespace ParserLibrary.Displayer
{
    public class StringDisplayer : Displayer
    {
        public override string display(List<MultiIMSIInstallParameter.Item.ItemRepresentation> items)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].lengthType == ItemRepresentation.LengthType.affectNext)
                {
                    i++;
                    sb.Append((items[i].ItemValue.Length / 2).ToString("X2"));
                }
                if (items[i].valueType == ItemRepresentation.ValueType.normal)
                {
                    sb.Append(items[i].ItemValue);
                    continue;
                }
                if (items[i].valueType == ItemRepresentation.ValueType.composite)
                {
                    sb.Append(items[i].compositeValues.ToString());
                }
            }

            return sb.ToString();
        }
    }
}
