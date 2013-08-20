using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLibrary.ItemObject;
using ParserLibrary.Decorator;

namespace LibraryTester
{
    /// <summary>
    /// Summary description for ItemRelation
    /// </summary>
    [TestClass]
    public class ItemIndividualTest
    {
        [TestMethod]
        public void Test_AffectNextItem_Normal()
        {
            RegularItem Reg = new RegularItem();
            Reg.Name = "test";
            Reg.Value = "002222";
            ItemValueAffectedNextItemLength affectNext = new ItemValueAffectedNextItemLength();
            affectNext.setAffectedItem(Reg);
            affectNext.Name = "Length";

            Assert.AreEqual("03", affectNext.Value );
            Assert.AreEqual(false, affectNext.canBeDisplayed());
            Assert.AreEqual(true, affectNext.includedInResult());
        }

        [TestMethod]
        public void Test_CompositeItem_Normal()
        {
            ItemComposite CI = new ItemComposite();
            CI.addBitItem(new BitItem() { isChecked = false, location = 2, name = "bit 2" });
            CI.addBitItem(new BitItem() { isChecked = true, location = 3, name = "bit 3" });
            CI.addBitItem(new BitItem() { isChecked = true, location = 4, name = "bit 4" });

            Assert.AreEqual("0C", CI.Value);
            Assert.AreEqual(true, CI.canBeDisplayed());
            Assert.AreEqual(true, CI.includedInResult());
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
            regDepend.setDisplayerDecorator(new Display_OtherItemNotNull(listOfItems, "affect"));
            listOfItems.Add(regDepend);

            Assert.AreEqual(false, regDepend.includedInResult());
            Assert.AreEqual(true, regDepend.canBeDisplayed());
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
            regDepend.setDisplayerDecorator(new Display_OtherItemNotNull(listOfItems, "affect"));
            listOfItems.Add(regDepend);

            Assert.AreEqual(true, regDepend.includedInResult());
            Assert.AreEqual(true, regDepend.canBeDisplayed());
        }
    }
}
