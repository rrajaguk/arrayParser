using System;
using System.Collections.Generic;
using System.Text;
using MultiIMSIInstallParameter.Item;

namespace ParserLibrary.Displayer
{
    public abstract class Displayer
    {
        public abstract string display(List<ItemRepresentation> items);
        
    }
}
