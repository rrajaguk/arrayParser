using System;
using System.Collections.Generic;
using System.Text;
using ParserLibrary.ItemObject;
using ParserLibrary.ItemObject.Decorator;
using ParserLibrary.ItemRep;

namespace ParserLibrary.TranslateFactory
{
    public class RTFTranslator : Translater
    {
        private string operatedValue = string.Empty;
        public string getValue()
        {
            string result = operatedValue;
            operatedValue = string.Empty;
            return result;
        }
        public override void Export()
        {

            // setting of color pallet
            string colorPalet = @"{\rtf1\ansi\deff0{\colortbl;\red0\green0\blue0;\red255\green0\blue0;}";
            StringBuilder SBData = new StringBuilder();
            StringBuilder SBHeader = new StringBuilder();
            int counter = 0;

            List<Item> listOfPass = parser.Items;
            for (int i = 0; i < listOfPass.Count; i++)
            {
                var item = listOfPass[i];

                SBHeader.Append(((counter % 2 == 0) ? @"\cf1" : @"\cf2") + " " + item.Value);
                counter++;

                ParserLibrary.ItemObject.Item basicForm = item;
                if (basicForm is ItemDecorator)
                {
                    basicForm = (item as ItemDecorator).getBaseClass();
                }

                if (basicForm is RegularItem)
                {
                    SBData.Append(((i % 2 == 0) ? @"\cf1" : @"\cf2") + " ");
                    SBData.Append(item.Value + @"\tab ");
                    SBData.Append(@"\cf1 = ");
                    SBData.Append(item.Name+ @"\line ");
                    continue;
                }
                if (basicForm is ItemComposite)
                {
                    ItemComposite composite = (basicForm as ItemComposite);
                    SBData.Append(((i % 2 == 0) ? @"\cf1" : @"\cf2") + " ");
                    SBData.Append(composite.Value.ToString() + @"\tab ");
                    SBData.Append(@"\cf1 = ");
                    SBData.Append(basicForm.Name + @"\line");
                    foreach (var compositeItem in composite.getItems())
                    {
                        SBData.Append(@"\tab " + compositeItem.name + @"\tab = " + (compositeItem.isChecked ? "activated" : "deactivated") + @"\line ");
                    }
                }
            }
            SBHeader.Append(@"\line ");
            operatedValue= (colorPalet + SBHeader.ToString() + SBData.ToString() + "}");
        }

        public override void Import()
        {
            throw new NotImplementedException();
        }
    }
}
