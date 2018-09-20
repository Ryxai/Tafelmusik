using System;
using Tafelmusik;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TafelmusikTests
{
    [TestClass]
    public class LambdaTests
    {
        [TestMethod]
        public void NameTest()
        {
            var lambda = Lambda<object, bool>.Create(() => true);
            Assert.AreEqual(string.Empty, lambda.Name);
        }

        [TestMethod]
        public void RunTest()
        {
            var lambda1 = Lambda<object, bool>.Create(() => true);
            var lambda2 = Lambda<object, bool>.Create(() => false);
            Assert.IsTrue(lambda1.Run(new object()));
            Assert.IsFalse(lambda2.Run(new object()));
        }

    }
}
