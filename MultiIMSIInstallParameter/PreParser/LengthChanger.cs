using System;
using System.Collections.Generic;
using System.Text;

namespace MultiIMSIInstallParameter.PreParser
{
    public class LengthChanger
    {
        public static string removeLength(string val)
        {
            val = val.Replace(" ", "");
            string result = val;
            int start= val.IndexOf("#(");
            if (start < 0)
            {
                return val;
            }
            int end = val.IndexOf(")", start);
            int length = ((end - start-2)/2);
            string totalLength = string.Format("{0:X2}", length);
            result = val.Replace("#(", totalLength);
            result = result.Replace(")", "");
            return result;
        }
    }
}
