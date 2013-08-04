using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiIMSIInstallParameter.Item;
using MultiIMSIInstallParameter.Parsers;

namespace LibraryTester
{
    [TestClass]
    public class DataDefinitionParserTest
    {
        [TestMethod]
        public void VerifyRegular()
        {

            List<ItemRepresentation> expected = new List<ItemRepresentation>(){
                new ItemRepresentation(){ItemName = "two bytes parameter",ItemLength = 2,lengthType = ItemRepresentation.LengthType.independent},
                new ItemRepresentation(){ItemName = "three bytes parameter",ItemLength = 3,lengthType = ItemRepresentation.LengthType.independent}
            };
            List<string> val = new List<string>{ "two bytes parameter,2", "three bytes parameter ,    3" };

            StreamReader testBed = prepareTestDouble(val);
            Parser p = new StreamDataDefinitionParser(testBed);
            List<ItemRepresentation> res = p.getItems();

            // do the checking
            Compare(expected, res);

        }

        [TestMethod]
        public void VerifyAffectNext()
        {
            List<ItemRepresentation> expected = new List<ItemRepresentation>(){
                new ItemRepresentation(){
                    ItemName = "two bytes parameter",
                    ItemLength = 2,
                    lengthType = ItemRepresentation.LengthType.independent},
                new ItemRepresentation(){
                    ItemName = "three bytes parameter",
                    ItemLength = 3,
                    lengthType = ItemRepresentation.LengthType.affectNext}
            };
            List<string> val = new List<string> { "two bytes parameter,2", "three bytes parameter ,    3,N" };

            StreamReader testBed = prepareTestDouble(val);
            Parser p = new StreamDataDefinitionParser(testBed);
            List<ItemRepresentation> res = p.getItems();

            // do the checking
            Compare(expected, res);            
        }

        [TestMethod]
        public void VerifyNullName()
        {
            List<ItemRepresentation> expected = new List<ItemRepresentation>(){
                new ItemRepresentation(){
                    ItemName = "",
                    ItemLength = 2,
                    lengthType = ItemRepresentation.LengthType.independent},
                new ItemRepresentation(){
                    ItemName = "three bytes parameter",
                    ItemLength = 3,
                    lengthType = ItemRepresentation.LengthType.affectNext}
            };
            List<string> val = new List<string> { ",2", "three bytes parameter ,    3,N" };

            StreamReader testBed = prepareTestDouble(val);
            Parser p = new StreamDataDefinitionParser(testBed);
            List<ItemRepresentation> res = p.getItems();

            // do the checking
            Compare(expected, res);
        }

        [TestMethod]
        public void VerifyTypeParsing()
        {
            List<ItemRepresentation> expected = new List<ItemRepresentation>(){
                new ItemRepresentation(){
                    ItemName = "normal val with length 2",
                    ItemLength = 2,
                    lengthType = ItemRepresentation.LengthType.independent,
                    valueType= ItemRepresentation.ValueType.normal},
                new ItemRepresentation(){
                    ItemName = "composite val",
                    ItemLength = 3,
                    lengthType = ItemRepresentation.LengthType.independent,
                    valueType = ItemRepresentation.ValueType.composite, 
                    compositeValues= new CompositeValue()}
            };
            List<string> val = new List<string> { "normal val with length 2,2", "composite val,    3,C" };

            StreamReader testBed = prepareTestDouble(val);
            Parser p = new StreamDataDefinitionParser(testBed);
            List<ItemRepresentation> res = p.getItems();

            // do the checking
            Compare(expected, res);
        }

        [TestMethod]
        public void VerifyCompositeValue()
        {
            CompositeValue CV = new CompositeValue();
            CV.setItem(new CompositeItem() { name = "byte 1", location = 2, isChecked = false });
            CV.setItem(new CompositeItem() { name = "byte 3", location = 3, isChecked = false });
            List<ItemRepresentation> expected = new List<ItemRepresentation>(){
                new ItemRepresentation(){
                    ItemName = "composite val",
                    ItemLength = 3,
                    lengthType = ItemRepresentation.LengthType.independent,
                    valueType = ItemRepresentation.ValueType.composite,
                    compositeValues = CV 
                }
            };
            List<string> val = new List<string> { "composite val,    3,C",">byte 1, 2",">byte 3, 3" };

            StreamReader testBed = prepareTestDouble(val);
            Parser p = new StreamDataDefinitionParser(testBed);
            List<ItemRepresentation> res = p.getItems();

            // do the checking
            Compare(expected, res);
        }

        /// <summary>
        /// preparing the stream reader based on input of list of  string
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private StreamReader prepareTestDouble(List<string> val)
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            foreach (var str in val)
            {
                sw.WriteLine(str);
            }
            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(ms);
            return sr;
        }
        /// <summary>
        /// comparing two list of itemrepresentations
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="result"></param>
        private void Compare(List<ItemRepresentation> expected, List<ItemRepresentation> result)
        {
            Assert.AreEqual(expected.Count,result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                ItemRepresentation currentExpectedItem = expected[i];
                ItemRepresentation currentResultItem = result[i];
                Assert.AreEqual(currentExpectedItem.ItemName, currentResultItem.ItemName);
                Assert.AreEqual(currentExpectedItem.ItemLength, currentResultItem.ItemLength);
                Assert.AreEqual(currentExpectedItem.lengthType, currentResultItem.lengthType);
                Assert.AreEqual(currentExpectedItem.valueType, currentResultItem.valueType);

                if (currentExpectedItem.compositeValues != null)
                {
                    List<CompositeItem> expectedCI =currentExpectedItem.compositeValues.getItems();
                    List<CompositeItem> resCI = currentResultItem.compositeValues.getItems();
                    Assert.AreEqual(expectedCI.Count, resCI.Count);
                    for (int j = 0; j < expectedCI.Count; j++)
                    {
                        Assert.AreEqual(expectedCI[i].isChecked, resCI[i].isChecked);
                        Assert.AreEqual(expectedCI[i].location, resCI[i].location);
                        Assert.AreEqual(expectedCI[i].name, resCI[i].name);
                    }
                }
            }
        }
    }
}
