using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLibrary.ItemFactory;
using ParserLibrary.ItemObject;
using ParserLibrary.ItemObject.Decorator;
using ParserLibrary.ItemRep;
using ParserLibrary.TranslateFactory;

namespace LibraryTester
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class ItemTranslatorTest
    {

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Translator_Normal_Import_Test()
        {
            List<string> val = new List<string> { "First Value,    1  ", "Second Value, 1" };

            StreamReader testBed = TestHelper.prepareTestDouble(val);
            NormalItemFactory normalItemFactory = new NormalItemFactory(testBed);
            ItemParser itemParser = new ItemParser();
            itemParser.setFactory(normalItemFactory);


            List<Item> expected = new List<Item>();
            expected.Add(new RegularItem() { Name = "First Value", Length = 1, Value = "03" });
            expected.Add(new RegularItem() { Name = "Second Value", Length = 1, Value = "04" });


            // compare the itemTranslator
            StringTranslator ST = new StringTranslator();
            itemParser.setTranslator(ST);
            ST.setValue("0304");
            ST.Import();
            Assert.AreEqual("0304", ST.getValue());
            Assert.AreEqual(string.Empty, ST.getValue());

            TestHelper.Compare(expected, itemParser.Items);
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Translator_Normal_Export_Test()
        {
            List<string> val = new List<string> { "First Value,    1  ,R,03", "Second Value, 1,R,04" };

            StreamReader testBed = TestHelper.prepareTestDouble(val);
            NormalItemFactory normalItemFactory = new NormalItemFactory(testBed);
            ItemParser itemParser = new ItemParser();
            itemParser.setFactory(normalItemFactory);


            List<Item> expected = new List<Item>();
            expected.Add(new RegularItem() { Name = "First Value", Length = 1, Value = "03" });
            expected.Add(new RegularItem() { Name = "Second Value", Length = 1, Value = "04" });

            TestHelper.Compare(expected, itemParser.Items);

            // compare the itemTranslator
            StringTranslator ST = new StringTranslator();
            itemParser.setTranslator(ST);
            ST.Export();
            Assert.AreEqual("0304", ST.getValue());
            Assert.AreEqual(string.Empty, ST.getValue());
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Translator_AffectNext_Import_Test()
        {

            // SETUP the SUT
            List<string> val = new List<string> { "First Value,    1  ,N", "Second Value, 16","Third Item, 1" };
            StreamReader testBed = TestHelper.prepareTestDouble(val);
            NormalItemFactory normalItemFactory = new NormalItemFactory(testBed);
            ItemParser itemParser = new ItemParser();
            itemParser.setFactory(normalItemFactory);

            List<Item> expected = new List<Item>();
            var affectorItem = new ItemValueAffectedNextItemLength() { Name = "First Value", Length = 1, Value = "01" };
            var affectedItem = new RegularItem() { Name = "Second Value", Length = 2, Value = "0403" };
            affectorItem.setAffectedItem(affectedItem);
            expected.Add(affectorItem);
            expected.Add(affectedItem);
            expected.Add(new RegularItem() { Name = "Third Item", Length = 1, Value = "02" });

            // Exercise
            StringTranslator ST = new StringTranslator();
            itemParser.setTranslator(ST);
            ST.setValue("02040302");
            ST.Import();

            // Verify
            TestHelper.Compare(expected, itemParser.Items);
            
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Translator_AffectNext_Export_Test()
        {
            List<string> val = new List<string> { "First Value,    1  ,N,02", "Second Value, 2,R,0403", "Third Item, 1,R, 02" };

            StreamReader testBed = TestHelper.prepareTestDouble(val);
            NormalItemFactory normalItemFactory = new NormalItemFactory(testBed);
            ItemParser itemParser = new ItemParser();
            itemParser.setFactory(normalItemFactory);


            List<Item> expected = new List<Item>();
            var affectorItem = new ItemValueAffectedNextItemLength() { Name = "First Value", Length = 1, Value = "02" };
            var affectedItem = new RegularItem() { Name = "Second Value", Length = 2, Value = "0403" };
            affectorItem.setAffectedItem(affectedItem);
            expected.Add(affectorItem);
            expected.Add(affectedItem);
            expected.Add(new RegularItem() { Name = "Third Item", Length = 1, Value = "02" });

            StringTranslator ST = new StringTranslator();
            itemParser.setTranslator(ST);
            ST.Export();
            Assert.AreEqual("02040302", ST.getValue());
            Assert.AreEqual(string.Empty, ST.getValue());
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Translator_OtherNotNull_Import_Test()
        {

            // SETUP the SUT
            List<string> val = new List<string> { 
                "First Value,    1 , R,00", 
                "Second Value, 2", 
                "Third Item, 1 , RP ,First Value" };
            StreamReader testBed = TestHelper.prepareTestDouble(val);
            NormalItemFactory normalItemFactory = new NormalItemFactory(testBed);
            ItemParser itemParser = new ItemParser();
            itemParser.setFactory(normalItemFactory);

            List<Item> expected = new List<Item>();

            var item1 = new RegularItem() { Name = "First Value", Length = 1, Value = "02" };
            var item2 = new RegularItem() { Name = "Second Value", Length = 2,Value = "0403" };
            var item3 = new OtherItemNotNull_Decorator(
                new RegularItem() { Name = "Third Item", Length = 1,Value= "02" },
                item1
                );
            expected.Add(item1);
            expected.Add(item2);
            expected.Add(item3);

            // Exercise
            StringTranslator ST = new StringTranslator();
            itemParser.setTranslator(ST);
            ST.setValue("02040302");
            ST.Import();

            // Verify
            TestHelper.Compare(expected, itemParser.Items);
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Translator_OtherNotNull_Export_Test()
        {
            List<string> val = new List<string> { "First Value,    1  ,R,00", "Second Value, 2,R,0403", "Third Item, 1,RP,First Value,02" };
            StreamReader testBed = TestHelper.prepareTestDouble(val);
            NormalItemFactory normalItemFactory = new NormalItemFactory(testBed);
            ItemParser itemParser = new ItemParser();
            itemParser.setFactory(normalItemFactory);


            List<Item> expected = new List<Item>();
            var item1 = new RegularItem() { Name = "First Value", Length = 1, Value = "00" };
            var item2 = new RegularItem() { Name = "Second Value", Length = 2, Value = "0403" };
            var item3 = new OtherItemNotNull_Decorator(
                new RegularItem() { Name = "Third Item", Length = 1, Value = "02" },
                item1
                );
            expected.Add(item1);
            expected.Add(item2);
            expected.Add(item3);

            StringTranslator ST = new StringTranslator();
            itemParser.setTranslator(ST);
            ST.Export();
            
            // verify the 00 first value
            Assert.AreEqual("000403", ST.getValue());
            Assert.AreEqual(string.Empty, ST.getValue());


            // verify the 02 first value
            itemParser.Items[0].Value = "02";
            ST.Export();
            Assert.AreEqual("02040302", ST.getValue());
            Assert.AreEqual(string.Empty, ST.getValue());


        }


    }
}
