using System;
using System.Dynamic;
using Tafelmusik;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TafelmusikTests
{
    [TestClass]
    public class LinearRunnerTest
    {
        [TestMethod]
        public void BasicTest()
        {
            var runner1 =
                LinearRunner<object, bool, bool>.Create("", b => b,
                    (b1, b2) => b1 && b2, () => true);
            var runner2 = LinearRunner<object, bool, bool>.Create("", b => b,
                (b1, b2) => b1 && b2, () => true);
            var app1 = Lambda<object, bool>.Create(() => true);
            var app2 = Lambda<object, bool>.Create(() => true);
            var app3 = Lambda<object, bool>.Create(() => false);
            runner1.Add(app1);
            runner1.Add(app2);
            runner2.Add(app1);
            runner2.Add(app3);
            runner2.Add(app3);
            Assert.IsTrue(runner1.Run(new object()));
            Assert.IsFalse(runner2.Run(new object()));
        }

        [TestMethod]
        public void CompletedAppsTest()
        {
            var runner1 = LinearRunner<object, bool, int>.Create("", b => true,
                (b, i) => (b ? 1 : 0) + i, () => 0);
            var runner2 = LinearRunner<object, bool, int>.Create("", b => true,
                (b, i) => (b ? 1 : 0) + i, () => 0);
            var app1 = Lambda<object, bool>.Create(() => true);
            var app2 = Lambda<object, bool>.Create(() => false);
            runner1.Add(app1);
            runner1.Add(app2);
            runner1.Add(app1).Add(app2).Add(app2);
            runner2.Add(app1).Add(app1);
            Assert.AreEqual(2, runner1.Run(new object()));
            Assert.AreEqual(2, runner2.Run(new object()));
        }

    }
}
