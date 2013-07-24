using System;
using System.Collections.Generic;
using System.Text;

namespace MultiIMSIInstallParameter.GuiHelper
{
    public class GuiHelper
    {
        public static string chopToLines(string val, out int numOfLines)
        {
            
            int myLimit = 50;
            string sentence = val; 
            string[] words = sentence.Split(' ');

            StringBuilder newSentence = new StringBuilder();
            numOfLines = 0;

            string line = "";
            foreach (string word in words)
            {
                if ((line + word).Length > myLimit)
                {
                    newSentence.AppendLine(line);
                    line = "";
                    numOfLines++;
                }

                line += string.Format("{0} ", word);
            }

            if (line.Length > 0)
            {
                newSentence.AppendLine(line);
                numOfLines++;
            }
            return newSentence.ToString();
        }
    }
}
