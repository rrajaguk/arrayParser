using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLibrary.ItemFactory;
using ParserLibrary.ItemObject;
using ParserLibrary.ItemRep;
using ParserLibrary.TranslateFactory;

namespace LibraryTester
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class ItemTranslatorTest
    {

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Translator_Import_Normal_Test()
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
        public void Translator_Export_Normal_Test()
        {
            List<string> val = new List<string> { "First Value,    1  ,03", "Second Value, 1,04" };

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
        public void Translator_Import_AffextNext_Test()
        {
            
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Translator_Export_AffextNext_Test()
        {

        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Translator_Import_OtherNotNull_Test()
        {

        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Translator_Export_OtherNotNull_Test()
        {

        }


    }
}
