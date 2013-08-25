using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLibrary.ItemFactory;
using System.IO;
using ParserLibrary.ItemObject;
using ParserLibrary.ItemObject.Decorator;

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
            List<string> val = new List<string> { "composite val,    1  ,C", ">byte 1, 2", ">byte 3, 3" ,
            "composite val 2,    1  ,C", ">byte 1, 2", ">byte 3, 3" };

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

            var IC2 = new ItemComposite();
            IC2.Length = 1;
            IC2.Name = "composite val 2";
            BitItem BI2_1 = new BitItem() { isChecked = false, location = 2, name = "byte 1" };
            BitItem BI2_2 = new BitItem() { isChecked = false, location = 2, name = "byte 1" };
            IC.addBitItem(BI2_1);
            IC.addBitItem(BI2_2);
            expectedItems.Add(IC2);

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
                "two bytes parameter,1", 
                "item affected by prev, 3, RP,two bytes parameter  "};

            StreamReader testBed = TestHelper.prepareTestDouble(val);
            NormalItemFactory normalItemFactory = new NormalItemFactory(testBed);
            List<Item> res = normalItemFactory.GetItems();

            List<Item> expected = new List<Item>();
            RegularItem ri = new RegularItem();
            ri.Name = "two bytes parameter";
            ri.Length = 1;
            ri.Value = "00";

            RegularItem ri2= new RegularItem();
            ri2.Name = "item affected by prev";
            ri2.Length = 3;
            ri2.Value = "002233";
            OtherItemNotNull_Decorator decorator = new OtherItemNotNull_Decorator(ri2, ri);

            expected.Add(ri);
            expected.Add(decorator);

           // test the value of decorator is null
            Assert.AreEqual(string.Empty, decorator.Value);

            // test the value of decorator is not null since there is a value on RI
            ri.Value = "01";
            Assert.AreEqual("002233",decorator.Value);
        }

       
    }
}
