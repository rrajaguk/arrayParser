using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserLibrary.ItemObject;

namespace LibraryTester
{
    public class TestHelper
    {
        /// <summary>
        /// preparing the stream reader based on input of list of  string
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static StreamReader prepareTestDouble(List<string> val)
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
        public static void Compare(List<Item> expected, List<Item> result)
        {
            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Item currentExpectedItem = expected[i];
                Item currentResultItem = result[i];
                Assert.AreEqual(currentExpectedItem.Name, currentResultItem.Name);
                Assert.AreEqual(currentExpectedItem.Length, currentResultItem.Length);
                Assert.AreEqual(currentExpectedItem.Value, currentResultItem.Value);
                Assert.AreEqual(currentExpectedItem.includedInResult(), currentResultItem.includedInResult());

            }
        }
    }
}
