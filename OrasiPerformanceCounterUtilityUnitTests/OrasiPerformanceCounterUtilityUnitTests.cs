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
            try
            {
                Counter.DeleteCounterCategory(@"HP LoadRunner Performance");
            }
            catch (System.UnauthorizedAccessException)
            {
            }
        }

        [TestMethod]
        public void DeleteCounterCategory_Prefix()
        {
            //Counter.DeleteCounterCategory("\\HP LoadRunner Performance");
        }

        [TestMethod]
        public void ResetCounter1()
        {
            try
            {
                Counter.ResetCounter(@"HP LoadRunner Performance(MyWebService)", 0);
            }
            catch(Exception e)
            {
                string strExpectedMessage = "Counter instance \"MyWebService\" provided but name not specified.";
                Assert.AreNotSame(e.Message, strExpectedMessage, "Expected error not found.");
            }
        }

        [TestMethod]
        public void ResetCounterContainsCounterName()
        {
            try
            {
                Counter.ResetCounter(@"HP LoadRunner Performance(MyWebService)Transactions/Second", 0);
            }
            catch (Exception e)
            {
                string strExpectedMessage = "Counter name : \"Transactions/Second\" not understood. Please use \"Count\", \"Rate/Sec\", or \"Average\".";
                Assert.AreNotSame(e.Message, strExpectedMessage, "Expected error not found.");
            }
        }

        [TestMethod]
        public void IncrementCounter1()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(MyWebService)\Count", 1);
        }
        [TestMethod]
        public void IncrementCounterInstance1()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(MyWebService 1)\Count", 1);
        }
        [TestMethod]
        public void IncrementCounterInstance2()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(MyWebService 2)\Count", 1);
        }
        [TestMethod]
        public void IncrementCounterInstance3()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(MyWebService 3)\Count", 1);
        }
        [TestMethod]
        public void IncrementCounterInstance4()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(MyWebService 4)\Count", 1);
        }

        [TestMethod]
        public void DeleteCounterCategory2()
        {
            try
            {
                Counter.DeleteCounterCategory(@"HP LoadRunner Performance(MyWebService)\Count");
            }
            catch (System.UnauthorizedAccessException)
            {
            }

        }

        [TestMethod]
        public void ResetCounter_WithMachineName()
        {
            Counter.ResetCounter(@"HP LoadRunner Performance(<computername> MyWebService)\Count", 0);
        }

        [TestMethod]
        public void IncrementCounter_WithMachineName1()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(<computername> MyWebService)\Count", 1);
        }
        [TestMethod]
        public void IncrementCounter_WithMachineName2()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(<computername> MyWebService)\Count", 1);
        }
        [TestMethod]
        public void IncrementCounter_WithMachineName3()
        {
            Counter.IncrementCounter(@"HP LoadRunner Performance(<computername> MyWebService)\Count", 1);
        }

    }
}
