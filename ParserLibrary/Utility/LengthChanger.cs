using System;
using System.Collections.Generic;
using System.Text;

namespace ParserLibrary.Utility
{
    public class LengthChanger
    {
        public class BracketLocation
        {
            public int location;
            public int length;
        }
        private static Stack<BracketLocation> BR = new Stack<BracketLocation>();
        public static string removeLength(string val)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < val.Length; i++)
            {
                string temp = null;
                if (i + 2 <= val.Length)
                {
                    temp = val.Substring(i, 2);
                }
                else
                {
                    temp = val[i].ToString();
                }

                if (val[i] == ')')
                {
                    // pop the bracket
                    BracketLocation bl = BR.Pop();
                    result.Remove(bl.location, 2);
                    result.Insert(bl.location, bl.length.ToString("X2"));
                    continue;
                }

                foreach (BracketLocation bracketLocation in BR)
                {
                    bracketLocation.length += 1;
                }
                if (temp == "#(")
                {
                    BracketLocation bracketLocation = new BracketLocation();
                    bracketLocation.length = 0;
                    bracketLocation.location = result.Length;
                    BR.Push(bracketLocation);

                }
                result.Append(temp);
                i++;

            }
            return result.ToString();
        }
    }
}
