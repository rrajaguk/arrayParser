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
    public class DisplayerString
    {
        [TestMethod]
        public void DisplayWithAffectNext()
        {
            List<ItemRepresentation> expected = new List<ItemRepresentation>(){
                new ItemRepresentation(){
                    ItemName = "two bytes parameter",
                    ItemLength = 2,
                    lengthType = ItemRepresentation.LengthType.independent,
                    ItemValue = "AABB"},
                new ItemRepresentation(){
                    ItemName = "this affect next",
                    ItemLength = 3,
                    lengthType = ItemRepresentation.LengthType.affectNext,
                    ItemValue="00"},
                new ItemRepresentation(){
                    ItemName = "three bytes parameter",
                    ItemLength = 0,
                    lengthType = ItemRepresentation.LengthType.affectNext,
                    ItemValue="AABBCC"}
            };

            Displayer DS = new StringDisplayer();
            string resultString= DS.display(expected);
            string expectedString = "AABB03AABBCC";
            Assert.AreEqual(expectedString, resultString);
        }

        [TestMethod]
        public void DisplayAllIndependent()
        {
            
            List<ItemRepresentation> expected = new List<ItemRepresentation>(){
                new ItemRepresentation(){
                    ItemName = "two bytes parameter",
                    ItemLength = 2,
                    lengthType = ItemRepresentation.LengthType.independent,
                    ItemValue = "AABB"},
                new ItemRepresentation(){
                    ItemName = "three bytes parameter",
                    ItemLength = 3,
                    lengthType = ItemRepresentation.LengthType.independent,
                    ItemValue="AABBCC"}
            };

            Displayer DS = new StringDisplayer();
            string resultString= DS.display(expected);
            string expectedString = "AABBAABBCC";
            Assert.AreEqual(expectedString, resultString);
        }

        [TestMethod]
        public void DisplayWithComposite()
        {

            CompositeValue CV = new CompositeValue();
            CV.setItem(new CompositeItem() { name = "byte 1", location = 1, isChecked = true });
            CV.setItem(new CompositeItem() { name = "byte 2", location = 2, isChecked = false});
            CV.setItem(new CompositeItem() { name = "byte 4", location = 4, isChecked = true });
            CV.setItem(new CompositeItem() { name = "byte 8", location = 8, isChecked = true });
            List<ItemRepresentation> expected = new List<ItemRepresentation>(){
                new ItemRepresentation(){
                    ItemName = "two bytes parameter",
                    ItemLength = 2,
                    lengthType = ItemRepresentation.LengthType.independent,
                    compositeValues = CV,
                    valueType = ItemRepresentation.ValueType.composite ,
                    ItemValue = "aacc"}
            };

            Displayer DS = new StringDisplayer();
            string resultString= DS.display(expected);
            string expectedString = "89";
            Assert.AreEqual(expectedString, resultString);
        }
    }
}
