using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLibrary.Utility;

namespace LibraryTester
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class Utility_Test
    {

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void LengthChanger_withBracket_1Level_Test()
        {
            string expected = "03225533";
            string result = LengthChanger.removeLength("#(225533)");
            Assert.AreEqual(expected, result);
        }
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void LengthChanger_withBracket_2Level_Test()
        {
            string expected = "03220133";
            string result = LengthChanger.removeLength("#(22#(33))");
            Assert.AreEqual(expected, result);

        }
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void LengthChanger_noBracket_Test()
        {
            string expected = "03220133";
            string result = LengthChanger.removeLength("03220133");
            Assert.AreEqual(expected, result);

        }
    }
}
