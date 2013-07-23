using System;
using System.Collections.Generic;
using System.Text;
using MultiIMSIInstallParameter.Item;

namespace MultiIMSIInstallParameter.Parsers
{
    public class EFInstallParam : Parser
    {
      
        public EFInstallParam()
        {
            ItemParam = new List<ItemRepresentation>();
            ItemParam.Add(new ItemRepresentation("Installation Flag", 1,ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Proprietary DF ID", 2, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Number of KEY Records", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Maximum Size of each KEY Record  ", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("EF KEY Update Mechanism", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("File ID of EF KEY  ", 2, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("File ID of EF SQN", 2, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("EF SQN Record Number", 1, ItemRepresentation.LengthType.independent));
            ItemParam.Add(new ItemRepresentation("Length of ADF USIM AID", 1, ItemRepresentation.LengthType.affectNext));
            ItemParam.Add(new ItemRepresentation("ADF USIM AID", 10, ItemRepresentation.LengthType.independent));
        }
    }
}
