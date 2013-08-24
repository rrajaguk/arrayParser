using System;
using System.Collections.Generic;
using System.Text;
using ParserLibrary.ItemRep;

namespace ParserLibrary.TranslateFactory
{
    public class StringTranslator  : TranslateFactory
    {
       
        private string operatedValue= string.Empty;
        public void setValue(string val){
            operatedValue = val;
        }
        public string getValue()
        {
            string result = operatedValue;
            operatedValue = string.Empty;
            return result;
        }
        public override void Export()
        {
            StringBuilder SB = new StringBuilder();
            foreach (var item in parser.Items)
            {
                SB.Append(item.Value);

            }
            operatedValue=  SB.ToString();
        }

        public override void Import()
        {
            int counter = 0;
            foreach (var item in parser.Items)
            {
                counter = item.fecthValue(operatedValue, counter);
            }
        }
    }
}
