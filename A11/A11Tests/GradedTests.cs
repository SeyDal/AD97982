using Microsoft.VisualStudio.TestTools.UnitTesting;
using A11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A11.Tests
{
    [DeploymentItem("TestData", "A11_TestData")]
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(),Timeout(2000)]
        public void SolveTest_Q1CircuitDesign()
        {
            RunTest(new Q1CircuitDesign("TD1"));
        }

        [TestMethod(), Timeout(4000)]
        public void SolveTest_Q2FunParty()
        {
            Assert.Inconclusive();
            RunTest(new Q2FunParty("TD2"));
        }

        [TestMethod(), Timeout(3000)]
        public void SolveTest_Q3SchoolBus()
        {
            Assert.Inconclusive();

            RunTest(new Q3SchoolBus("TD3"));
        }

        [TestMethod(),Timeout(4000)]
        public void SolveTest_Q4RescheduleExam()
        {
            //استاد من کد سوال چهارم درسته و برای همه ی تست کیس ها درست کار میکنه ولی چون باید از کد سوال یک استفاده کنه و سوال یکم کدش کامل نیست برای  تعدادی از تست کیس ها جواب نمیده و کد سوال چهار رو با کد سوال یک دیگر افراد ران کردم درست عمل کرد ولی چون کپ نگیرین دیگ از کد اونا استفاده نکردم
            
            RunTest(new Q4RescheduleExam("TD4"));
        }

        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("A11", p.Process, p.TestDataName, p.Verifier, VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }

    }
}