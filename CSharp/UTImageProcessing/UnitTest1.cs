using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UTImageProcessing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Byte nExValue;
            for (double dValue = -256; dValue < 256;)
            {
                if (dValue > 255.0)
                {
                    nExValue = 255;
                }
                else if (dValue < 0.0)
                {
                    nExValue = 0;
                }
                else
                {
                    nExValue = (byte)dValue;
                }
                byte nAns = ComFunc.DoubleToByte(dValue);
                Assert.AreEqual(nExValue, nAns);
                dValue += 0.1;
            }
        }
    }
}
