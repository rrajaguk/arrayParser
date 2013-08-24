using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLibrary.ItemObject;
using ParserLibrary.ItemObject.Decorator;

namespace LibraryTester
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class ItemDecoratorTest
    {

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Test_ItemDecorator_GetBase1Level()
        {
            RegularItem RI = new RegularItem();
            RegularItem RI2 = new RegularItem();
            OtherItemNotNull_Decorator decWithRegularItem = new OtherItemNotNull_Decorator(RI, RI2);

            Assert.IsInstanceOfType(decWithRegularItem.getBaseClass(), typeof(RegularItem));

            ItemComposite itemComposite = new ItemComposite();
            OtherItemNotNull_Decorator decWithComposite = new OtherItemNotNull_Decorator(itemComposite, RI2);
            Assert.IsInstanceOfType(decWithComposite.getBaseClass(), typeof(ItemComposite));

        }
    }
}
