using System;
using System.Collections.Generic;
using System.Text;
using MultiIMSIInstallParameter.Item;

namespace MultiIMSIInstallParameter.Parsers
{
    public class BufferInstallParam : Parser
    {
        public BufferInstallParam()
          {
              ItemParam = new List<ItemRepresentation>();
              ItemParam.Add(new ItemRepresentation("Number of IMSIs", 1, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("Number of Language", 1, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("Maximum Length of Proactive command APDU ", 1, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("Installation Flag  ", 1, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("Size of EF FPLMN ", 2, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("Size of the Applet’s PLMN List ", 2, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("EF SMSP Record Length", 1, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("EF SMSP Record Number", 1, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("Maximum String Length", 1, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("Default IMSI List Size", 1, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("Number of KEY Records", 1, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("Maximum Size of each KEY Record ", 1, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("EF KEY Update Mechanism", 1, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("File ID of EF KEY ", 2, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("File ID of EF SQN", 2, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("EF SQN Record Number ", 1, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("Length of ADF USIM AID ", 1, ItemRepresentation.LengthType.affectNext));
              ItemParam.Add(new ItemRepresentation("ADF USIM AID ", 1, ItemRepresentation.LengthType.independent));
              ItemParam.Add(new ItemRepresentation("Applicative MSL", 1, ItemRepresentation.LengthType.independent));

          }
    }
}
