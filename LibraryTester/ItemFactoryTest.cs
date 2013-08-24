using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLibrary.ItemFactory;
using System.IO;
using ParserLibrary.ItemObject;
using MultiIMSIInstallParameter.Item;
using ParserLibrary.Decorator;

namespace LibraryTester
{
    /// <summary>
    /// Summary description for ItemFactoryTest
    /// </summary>
    [TestClass]
    public class ItemFactoryTest
    {

       

        [TestMethod]
        public void Test_NormalItemFactory_CompositeData()
        {
            List<string> val = new List<string> { "composite val,    1  ,C", ">byte 1, 2", ">byte 3, 3" };

            StreamReader testBed = TestHelper.prepareTestDouble(val);
            NormalItemFactory normalItemFactory = new NormalItemFactory(testBed);
            List<Item> items =  normalItemFactory.GetItems();
            List<Item> expectedItems = new List<Item>();
            var IC = new ItemComposite();
            IC.Length = 1;
            IC.Name = "composite val";

            BitItem BI1 = new BitItem() { isChecked = false, location = 2, name = "byte 1" };
            BitItem BI2 = new BitItem() { isChecked = false, location = 2, name = "byte 1" };
            IC.addBitItem(BI1);
            IC.addBitItem(BI2);
            expectedItems.Add(IC);
            
            // start asserting;
            TestHelper.Compare(expectedItems, items);
        }

        [TestMethod]
        public void Test_NormalItemFactory_AffectedNext()
        {
            List<string> val = new List<string> { "two bytes parameter,1 , N", "three bytes parameter ,    3" };

            StreamReader testBed = TestHelper.prepareTestDouble(val);
            NormalItemFactory normalItemFactory = new NormalItemFactory(testBed);
            List<Item> res = normalItemFactory.GetItems();
            
            List<Item> expected = new List<Item>();
            RegularItem ri = new RegularItem();
            ri.Name = "three bytes parameter";
            ri.Length = 3;
            ItemValueAffectedNextItemLength prev = new ItemValueAffectedNextItemLength();
            prev.setAffectedItem(ri);
            prev.Name = "two bytes parameter";
            prev.Length = 1;
            expected.Add(prev);
            expected.Add(ri);


            // do the checking
            TestHelper.Compare(expected, res);            
        }
        [TestMethod]
        public void Test_NormalItemFactory_OtherItemNull()
        {
            List<string> val = new List<string> { 
                "two bytes parameter,1 , N", 
                "three bytes parameter ,    3" ,
                "item affected by prev, 3, P,two bytes parameter  "};

            StreamReader testBed = TestHelper.prepareTestDouble(val);
            NormalItemFactory normalItemFactory = new NormalItemFactory(testBed);
            List<Item> res = normalItemFactory.GetItems();

            List<Item> expected = new List<Item>();
            RegularItem ri = new RegularItem();
            ri.Name = "three bytes parameter";
            ri.Length = 3;
            ItemValueAffectedNextItemLength prev = new ItemValueAffectedNextItemLength();
            prev.setAffectedItem(ri);
            prev.Name = "two bytes parameter";
            prev.Length = 1;

            RegularItem ri2= new RegularItem();
            ri2.Name = "item affected by prev";
            ri2.Length = 3;
            ri2.setDisplayerDecorator(new Display_OtherItemNotNull(expected, prev.Name));

            expected.Add(prev);
            expected.Add(ri);
            expected.Add(ri2);

            TestHelper.Compare(expected, res);

            // test the last val
            RegularItem RI = (res[2] as RegularItem);
            Assert.IsFalse(RI.includedInResult());


            Item RI_controller = (res[1] as Item);
            RI_controller.Value = "02";
            Assert.IsTrue(RI.includedInResult());
        }

       
    }
}
