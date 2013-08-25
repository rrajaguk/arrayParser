using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLibrary.ItemObject;
using ParserLibrary.ItemObject.Decorator;

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
            Reg.Length = 3;
            ItemValueAffectedNextItemLength affectNext = new ItemValueAffectedNextItemLength();
            affectNext.setAffectedItem(Reg);
            affectNext.Name = "Length";

            Assert.AreEqual("03", affectNext.Value );
        }

        [TestMethod]
        public void Test_CompositeItem_Normal()
        {
            ItemComposite CI = new ItemComposite();
            CI.addBitItem(new BitItem() { isChecked = false, location = 2, name = "bit 2" });
            CI.addBitItem(new BitItem() { isChecked = true, location = 3, name = "bit 3" });
            CI.addBitItem(new BitItem() { isChecked = true, location = 4, name = "bit 4" });

            Assert.AreEqual("0C", CI.Value);
        }

       
    }
}
