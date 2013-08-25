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
        [TestMethod]
        public void Test_Display_otherItemNotNull_FindItemZero()
        {
            List<Item> listOfItems = new List<Item>();
            RegularItem reg = new RegularItem();
            reg.Name = "affect";
            reg.Value = "00";
            listOfItems.Add(reg);

            RegularItem regDepend = new RegularItem();
            regDepend.Name = "depend";
            regDepend.Value = "0000";

            OtherItemNotNull_Decorator dec = new OtherItemNotNull_Decorator(regDepend, reg);
            listOfItems.Add(dec);

            // verify that the value empty since the affect value is 00
            Assert.AreEqual(string.Empty, dec.Value);
        }
        [TestMethod]
        public void Test_Display_otherItemNotNull_FindItemValue()
        {
            List<Item> listOfItems = new List<Item>();
            RegularItem reg = new RegularItem();
            reg.Name = "affect";
            reg.Value = "01";
            listOfItems.Add(reg);

            RegularItem regDepend = new RegularItem();
            regDepend.Name = "depend";
            regDepend.Value = "0000";


            OtherItemNotNull_Decorator dec = new OtherItemNotNull_Decorator(regDepend, reg);
            listOfItems.Add(dec);

            // verify that the value empty since the affect value is 00
            Assert.AreEqual("0000", dec.Value);


        }
    }
}
