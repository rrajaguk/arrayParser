using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiIMSIInstallParameter.Item;
using ParserLibrary.Displayer;

namespace LibraryTester
{
    [TestClass]
    public class DisplayerRTFTest
    {
        [TestMethod]
        public void NormalDataIndependent()
        {
            RTFDisplayer display = new RTFDisplayer();

            List<ItemRepresentation> expected = new List<ItemRepresentation>(){
                new ItemRepresentation(){
                    ItemName = "two bytes parameter",
                    ItemLength = 2,
                    lengthType = ItemRepresentation.LengthType.independent,
                    ItemValue = "AABB"},
                new ItemRepresentation(){
                    ItemName = "three bytes parameter",
                    ItemLength = 0,
                    lengthType = ItemRepresentation.LengthType.independent,
                    ItemValue="AABBCC"}
            };

            string resultString = display.display(expected);
            string expectedString = @"{\rtf1\ansi\deff0{\colortbl;\red0\green0\blue0;\red255\green0\blue0;}" +
                                    @"\cf1 AABB\cf2 AABBCC\line "+
                                    @"\cf1 AABB\tab \cf1 = two bytes parameter\line " +
                                    @"\cf2 AABBCC\tab \cf1 = three bytes parameter\line "+
                                    @"}";

            Assert.AreEqual(resultString,expectedString);
           
        }

        [TestMethod]
        public void NormalDataAffectNext()
        {
            RTFDisplayer display = new RTFDisplayer();

            List<ItemRepresentation> expected = new List<ItemRepresentation>(){
                new ItemRepresentation(){
                    ItemName = "Val",
                    ItemLength = 2,
                    lengthType = ItemRepresentation.LengthType.affectNext,
                    ItemValue = "03"},
                new ItemRepresentation(){
                    ItemName = "three bytes parameter",
                    ItemLength = 0,
                    lengthType = ItemRepresentation.LengthType.independent,
                    ItemValue="AABBCC"}
            };

            string resultString = display.display(expected);
            string expectedString = @"{\rtf1\ansi\deff0{\colortbl;\red0\green0\blue0;\red255\green0\blue0;}" +
                                    @"\cf1 03\cf2 AABBCC\line " +
                                    @"\cf1 03\tab \cf1 = Val\line " +
                                    @"\cf2 AABBCC\tab \cf1 = three bytes parameter\line " +
                                    @"}";

            Assert.AreEqual(resultString, expectedString);
            
        }

        [TestMethod]
        public void CompositeDataIndependent()
        {
            RTFDisplayer display = new RTFDisplayer();

            CompositeValue CV = new CompositeValue();
            CV.setItem(new CompositeItem() { name = "byte 1", location = 1, isChecked = true });
            CV.setItem(new CompositeItem() { name = "byte 2", location = 2, isChecked = false });
            CV.setItem(new CompositeItem() { name = "byte 4", location = 4, isChecked = true });
            CV.setItem(new CompositeItem() { name = "byte 8", location = 8, isChecked = true });
            List<ItemRepresentation> expected = new List<ItemRepresentation>(){
                new ItemRepresentation(){
                    ItemName = "two bytes parameter",
                    ItemLength = 1,
                    lengthType = ItemRepresentation.LengthType.independent,
                    compositeValues = CV,
                    valueType = ItemRepresentation.ValueType.composite ,
                    ItemValue = "89"}
            };

            string resultString = display.display(expected);
            string expectedString = @"{\rtf1\ansi\deff0{\colortbl;\red0\green0\blue0;\red255\green0\blue0;}" +
                                    @"\cf1 89\line " +
                                    @"\cf1 89\tab \cf1 = two bytes parameter\line" +
                                    @"\tab byte 1\tab = activated\line " +
                                    @"\tab byte 2\tab = deactivated\line " +
                                    @"\tab byte 4\tab = activated\line " +
                                    @"\tab byte 8\tab = activated\line " +
                                    @"}";

            Assert.AreEqual(resultString, expectedString);
        }
    }
}
