using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Tafelmusik;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TafelmusikTests
{
    [TestClass]
    public class AppletTests
    {

        private string testExecutablePath =
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
            @"\source\repos\Tafelmusik\TestProcess\bin\debug";
        [TestMethod]
        public void TestName()
        {
            const string name1 = "app";
            const string name2 = "";
            var applet1 = Applet<object, bool>.Create(name1, (i, b) => true,
                s => true);
            var applet2 =
                Applet<object, bool>.Create(name2, (i, b) => true, s => true);
            Assert.IsTrue(name1.Equals(applet1.Name));
            Assert.IsTrue(name2.Equals(applet2.Name));
        }

        [TestMethod]
        public void TestProcessRunWithNoOutputEvaluator()
        {
            const string name = "TestProcess";
            var applet1 =
                Applet<object, bool>.Create(name, (i, b)  => i == 0, s => true,  testExecutablePath, "");
            var applet2 = Applet<object, bool>.Create(name, (i, b) => i == 0, s => true, testExecutablePath, "3");
            Assert.IsTrue(applet1.Run(new object()
                ));
            Assert.IsFalse(applet2.Run(new object()));
        }

        [TestMethod]
        public void TestProcessRunWithInputEvaluator()
        {
            const string name = "TestProcess";
            bool DefSuccessEval(int i, bool b) => b;
            bool DefOutputEval(string s) => s.Equals("0");
            var applet1 = Applet<object, bool>.Create(name, DefSuccessEval,
                DefOutputEval, testExecutablePath, "");
            var applet2 = Applet<object, bool>.Create(name, DefSuccessEval, DefOutputEval,
                testExecutablePath, "3");
            Assert.IsTrue(applet1.Run(new object()));
            Assert.IsFalse(applet2.Run(new object()));
        }

        [TestMethod]
        public void TestProcessExceptionsTest()
        {
            const string name = "TestProcess";
            bool DefSuccessEval(int i, bool b) => b;
            bool DefOutputEval(string s) => s.Equals("0");
            var app1 = Applet<object, bool>.Create(name, DefSuccessEval, DefOutputEval,  testExecutablePath, "win32");
            var app2 = Applet<object, bool>.Create(name, DefSuccessEval, DefOutputEval, testExecutablePath, "sys");
            var res1 = app1.Run(new object());
            var res2 = app2.Run(new object());
            Assert.IsFalse(res1);
            Assert.IsFalse(res2);
        }

        [TestMethod]
        public void FailToLocateProcessTest()
        {
            bool DefSuccessEval(int i, bool b) => b;
            bool DefOutputEval(string s) => s.Equals("0");
            var applet =
                Applet<object, bool>.Create("", DefSuccessEval, DefOutputEval);
            Assert.IsFalse(applet.Run(new object()));
        }
    }
}
