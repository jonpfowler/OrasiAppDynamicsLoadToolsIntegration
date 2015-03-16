using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace OrasiPerformanceCounterUtility
{
    public static class Counter
    {
        public static void IncrementCounter(string performanceCounter, int value)
        {
            var perfCounter = ParseCounterPath(performanceCounter);

            CreatePerformanceCounterCategory(perfCounter.CategoryName, perfCounter.CategoryHelp, perfCounter.CategoryType);

            IncrementPerformanceCounter(perfCounter.CategoryName, perfCounter.CounterInstanceName, value);
        }

        public static void ResetCounter(string performanceCounter, int value)
        {
            var perfCounter = ParseCounterPath(performanceCounter);
            CreatePerformanceCounterCategory(perfCounter.CategoryName, perfCounter.CategoryHelp, perfCounter.CategoryType);

            ResetPerformanceCounter(perfCounter.CategoryName, perfCounter.CounterInstanceName, value);
        }

        public static void DeleteCounterCategory(string performanceCounter)
        {
            var perfCounter = ParseCounterPath(performanceCounter);
            DeletePerformanceCounterCategory(perfCounter.CategoryName);
        }

        private static void IncrementPerformanceCounter(string categoryName, string instanceName, int value)
        {
            PerformanceCounter myCounter;
            var CounterNames = GetCounterNames();
            foreach (CounterNameClass counterName in CounterNames)
            {
                myCounter = new PerformanceCounter(categoryName, counterName.CounterName, instanceName, false);
                myCounter.IncrementBy(value);
            }
        }

        private static void ResetPerformanceCounter(string categoryName, string instanceName, int value)
        {
            PerformanceCounter myCounter;
            var CounterNames = GetCounterNames();
            foreach (CounterNameClass counterName in CounterNames)
            {
                myCounter = new PerformanceCounter(categoryName, counterName.CounterName, instanceName, false);
                myCounter.RawValue = value;
            }
        }

        private static void CreatePerformanceCounterCategory(string categoryName, string categoryHelp, PerformanceCounterCategoryType performanceCounterCategoryType)
        {
            if (!PerformanceCounterCategory.Exists(categoryName))
            {
                CounterCreationDataCollection counterData = new CounterCreationDataCollection();
                CounterCreationData counter;

                var CounterNames = GetCounterNames();
                foreach(CounterNameClass counterName in CounterNames)
                {
                    // Create a collection of type CounterCreationDataCollection.
                    counter = new CounterCreationData();
                    counter.CounterName = counterName.CounterName;
                    counter.CounterType = counterName.CounterType;
                    counter.CounterHelp = counterName.CounterHelp;
                    counterData.Add(counter);
                }

                // Create the category and pass the collection to it.
                PerformanceCounterCategory.Create(categoryName, categoryHelp, performanceCounterCategoryType, counterData);
            }
        }
        
        private static void DeletePerformanceCounterCategory(string categoryName)
        {
            if (PerformanceCounterCategory.Exists(categoryName))
            {
                PerformanceCounterCategory.Delete(categoryName);
            }
        }

        /// <summary>
        /// Accepts Category name and instance name
        /// Note, we dont need the counter name because they are pre-defined
        /// User passes in a path in this format
        /// "HP LoadRunner Performance"
        /// "\\HP LoadRunner Performance"
        /// "\\HP LoadRunner Performance(IMH IPMS Web Service)"
        /// "HP LoadRunner Performance(IMH IPMS Web Service)"
        /// "HP LoadRunner Performance(<computername> IMH IPMS Web Service)"
        /// </summary>
        private static PerfCounter ParseCounterPath(string performanceCounter)
        {
            var perfCounter = new PerfCounter();

            string[] perfCounterParts = performanceCounter
                .Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

            //CategoryName
            perfCounter.CategoryName = perfCounterParts[0]
                .Split(new char[] { '(' }, StringSplitOptions.RemoveEmptyEntries)[0];

            if (perfCounterParts[0].Contains("(") || perfCounterParts[0].Contains(")"))
            {
                //InstanceName
                perfCounter.CounterInstanceName = perfCounterParts[0]
                    .Split(new char[] { '(' }, StringSplitOptions.RemoveEmptyEntries)[1]
                    .Split(new char[] { ')' }, StringSplitOptions.RemoveEmptyEntries)[0];

                //If user wants the computername added to the instance name they can just add the following:
                //  <computername> myinstancename
                var index = perfCounter.CounterInstanceName.IndexOf("<computername>");
                if (index >= 0)
                {
                    perfCounter.CounterInstanceName = perfCounter.CounterInstanceName.Replace("<computername>", System.Net.Dns.GetHostName());
                }
            }
            //Both of these are hard coded
            perfCounter.CategoryHelp = "OrasiPerformanceCounterUtility Category.";
            perfCounter.CategoryType = PerformanceCounterCategoryType.MultiInstance;

            return perfCounter;
        }

        private static List<CounterNameClass> GetCounterNames()
        {
            return new List<CounterNameClass> {
                new CounterNameClass("Count", "OrasiPerformanceCounterUtility Count.", PerformanceCounterType.NumberOfItems32) ,
                new CounterNameClass("Rate/Sec", "OrasiPerformanceCounterUtility Rate/Sec.", PerformanceCounterType.RateOfCountsPerSecond32)
            };
        }

    }//class

    public class PerfCounter
    {
        public string CategoryName { get; set; }
        public string CategoryHelp { get; set; }
        public PerformanceCounterCategoryType CategoryType { get; set; }
        public string CounterInstanceName { get; set; }

        public PerfCounter() { }
        public PerfCounter(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, string counterInstanceName)
        {
            CategoryName = categoryName;
            CategoryHelp = categoryHelp;
            CategoryType = categoryType;
            CounterInstanceName = counterInstanceName;
        }
    }//class

    public class CounterNameClass
    {
        public string CounterName { get; set; }
        public string CounterHelp { get; set; }
        public PerformanceCounterType CounterType { get; set; }

        public CounterNameClass(string counterName, string counterHelp, PerformanceCounterType counterType)
        {
            CounterName = counterName;
            CounterHelp = counterHelp;
            CounterType = counterType;
        }
    }//class

}//namespace