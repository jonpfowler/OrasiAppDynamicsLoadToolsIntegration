using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrasiPerformanceCounterUtility;

namespace PerformanceCounterUtilityUnitTests
{
    [TestClass]
    public class OrasiPerformanceCounterUtilityUnitTests
    {
        [TestMethod]
        public void DeleteCounterCategory_NoInstance()
        {
            Counter.DeleteCounterCategory(@"HP LoadRunner Performance");
        }

        [TestMethod]
        public void DeleteCounterCategory_Prefix()
        {
            Counter.DeleteCounterCategory("\\HP LoadRunner Performance");
        }

        [TestMethod]
        public void ResetCounter1()
        {
            Counter.ResetCounter(@"HP LoadRunner Performance(MyWebService)", 0);
        }

        [TestMethod]
        public void ResetCounterContainsCounterName()
        {
            Counter.ResetCounter(@"HP LoadRunner Performance(MyWebService)Transactions/Second", 0);
        }

        [TestMethod]
        public void IncrementCounter1()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(MyWebService)", 1);
        }
        [TestMethod]
        public void IncrementCounterInstance1()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(MyWebService 1)", 1);
        }
        [TestMethod]
        public void IncrementCounterInstance2()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(MyWebService 2)", 1);
        }
        [TestMethod]
        public void IncrementCounterInstance3()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(MyWebService 3)", 1);
        }
        [TestMethod]
        public void IncrementCounterInstance4()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(MyWebService 4)", 1);
        }

        [TestMethod]
        public void DeleteCounterCategory2()
        {
            Counter.DeleteCounterCategory(@"HP LoadRunner Performance(MyWebService)");
        }

        [TestMethod]
        public void ResetCounter_WithMachineName()
        {
            Counter.ResetCounter(@"HP LoadRunner Performance(<computername> MyWebService)", 0);
        }

        [TestMethod]
        public void IncrementCounter_WithMachineName1()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(<computername> MyWebService)", 1);
        }
        [TestMethod]
        public void IncrementCounter_WithMachineName2()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(<computername> MyWebService)", 1);
        }
        [TestMethod]
        public void IncrementCounter_WithMachineName3()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(<computername> MyWebService)", 1);
        }

    }
}
