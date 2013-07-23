using System;
using System.Collections.Generic;
using System.Text;
using MultiIMSIInstallParameter.Item;

namespace MultiIMSIInstallParameter.Parsers
{
    public class ApplicationParameter :Parser
    {
        public ApplicationParameter()
        {
            ItemParam = new List<ItemRepresentation>();
            ItemParam.Add(new ItemRepresentation("Extended Function A ", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Status Counter", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Poll Interval ", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Timeout Coverage Counter", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Applet State", 2, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Extended Function B ", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("RFU  ", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Maximum Number of Proactive Command Retries", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Applet Global Activation", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Current Language", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Event Location Download counter", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Activation/Deactivation of Play Tone Proactive Command", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Activation/Deactivation of Display Text Proactive Command", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Default IMSI at ATR (Device Reboot)", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Default IMSI for Automatic Swapping Mechanism", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Maximum Number of Refresh Retry", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Default IMSI for 3GPP2 ", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Applicative MSL", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("RFU ", 1, ItemRepresentation.LengthType.independent));
      
        }
    }
}
