using System;
using System.Collections.Generic;
using System.Text;
using MultiIMSIInstallParameter.Item;

namespace ParserLibrary.Displayer
{
    public class RTFDisplayer :Displayer
    {
        public override string display(List<MultiIMSIInstallParameter.Item.ItemRepresentation> items)
        {
            // setting of color pallet
            string colorPalet = @"{\rtf1\ansi\deff0{\colortbl;\red0\green0\blue0;\red255\green0\blue0;}";
            StringBuilder SBData = new StringBuilder();
            StringBuilder SBHeader = new StringBuilder();
            int counter = 0;

            List<ItemRepresentation> listOfPass = items;
            for (int i = 0; i < listOfPass.Count; i++)
            {
                var item = listOfPass[i];

                SBHeader.Append(((counter % 2 == 0) ? @"\cf1" : @"\cf2") + " " + item.ItemValue);
                counter++;

                if (item.lengthType == ItemRepresentation.LengthType.affectNext)
                {
                    SBData.Append(((i % 2 == 0) ? @"\cf1" : @"\cf2") + " ");
                    SBData.Append((listOfPass[i + 1].ItemValue.Length / 2).ToString("X2") + @"\tab ");
                    SBData.Append(@"\cf1 = ");
                    SBData.Append(item.ItemName + @"\line ");
                    continue;
                }
                if (item.valueType == ItemRepresentation.ValueType.normal)
                {
                    SBData.Append(((i % 2 == 0) ? @"\cf1" : @"\cf2") + " ");
                    SBData.Append(item.ItemValue + @"\tab ");
                    SBData.Append(@"\cf1 = ");
                    SBData.Append(item.ItemName + @"\line ");
                    continue;
                }
                if (item.valueType == ItemRepresentation.ValueType.composite)
                {
                    SBData.Append(((i % 2 == 0) ? @"\cf1" : @"\cf2") + " ");
                    SBData.Append(item.compositeValues.ToString() + @"\tab ");
                    SBData.Append(@"\cf1 = ");
                    SBData.Append(item.ItemName + @"\line");
                    foreach (var compositeItem in item.compositeValues.getItems())
                    {
                        SBData.Append(@"\tab " + compositeItem.name + @"\tab = " + (compositeItem.isChecked ? "activated" : "deactivated") + @"\line ");
                    }
                }
            }
            SBHeader.Append(@"\line ");
            return  (colorPalet + SBHeader.ToString() + SBData.ToString() + "}");
        }
    }
}
