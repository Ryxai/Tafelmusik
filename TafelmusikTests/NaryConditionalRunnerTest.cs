using System;
using System.Runtime.InteropServices;
using Tafelmusik;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TafelmusikTests
{
    [TestClass]
    public class NaryConditionalRunnerTest
    {
        [TestMethod]
        public void BasicTest()
        {
            var runner = NaryConditionalRunner<int, bool>.Create("");
            var app1 = Lambda<int, bool>.Create(() => true);
            var app2 = Lambda<int, bool>.Create(() => false);
            runner.Add(0, app1).Add(1, app2);
            Assert.IsTrue(runner.Run(0));
            Assert.IsFalse(runner.Run(1));
        }

        [TestMethod]
        public void ComplexTest()
        {
            var runner = NaryConditionalRunner<int, bool>.Create("");
            var app1 = Lambda<int, bool>.Create(() => true);
            var app2 = Lambda<int, bool>.Create(() => false);
            runner.Add(0, app1).Add(1, app2).Add(2, app1).Add(3, app2);
            Assert.IsTrue(runner.Run(0));
            Assert.IsFalse(runner.Run(1));
            Assert.IsTrue(runner.Run(2));
            Assert.IsFalse(runner.Run(3));
        }

    }
}
