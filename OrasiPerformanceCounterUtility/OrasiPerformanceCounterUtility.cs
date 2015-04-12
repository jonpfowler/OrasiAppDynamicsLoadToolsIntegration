using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace OrasiPerformanceCounterUtility
{
    public static class Counter
    {

        public static void IncrementCounter(string performanceCounter, double value)
        {
            var perfCounter = ParseCounterPath(performanceCounter);

            CreatePerformanceCounterCategory(perfCounter);

            IncrementPerformanceCounter(perfCounter, value);
        }


        public static void ResetCounter(string performanceCounter, int value)
        {
            var perfCounter = ParseCounterPath(performanceCounter);
            CreatePerformanceCounterCategory(perfCounter);

            ResetPerformanceCounter(perfCounter, value);
        }

        public static void DeleteCounterCategory(string performanceCounter)
        {
            var perfCounter = ParseCounterPath(performanceCounter);
            DeletePerformanceCounterCategory(perfCounter);
        }

        private static void IncrementPerformanceCounter(PerfCounter perfCounter, double value)
        {
            PerformanceCounter myCounter;
            //var CounterNames = GetCounterNames();
            //foreach (CounterNameClass counterName in CounterNames)
            //{
            //    myCounter = new PerformanceCounter(categoryName, counterName.CounterName, instanceName, false);
            //    myCounter.IncrementBy(value);
            //}
            myCounter = new PerformanceCounter(perfCounter.CategoryName, perfCounter.CounterName, perfCounter.CounterInstanceName, false);
            //myCounter.IncrementBy(value);
            //myCounter.
        }

        private static void ResetPerformanceCounter(PerfCounter perfCounter, int value)
        {
            PerformanceCounter myCounter = 
                new PerformanceCounter(
                    perfCounter.CategoryName,
                    perfCounter.CounterName,
                    perfCounter.CounterInstanceName,
                    false);
            myCounter.RawValue = value;
        }

        private static void CreatePerformanceCounterCategory(PerfCounter perfCounter)
        {
            //Logger logger = new Logger(@"OrasiPerformanceCounterUtility.log");
            //logger.WriteEntry("CreatePerformanceCounterCategory begin.", "Info", "OrasiPerformanceCounterUtility");

            if (!PerformanceCounterCategory.Exists(perfCounter.CategoryName))
            {
                CounterCreationDataCollection counterData = new CounterCreationDataCollection();
                CounterCreationData counter;

                counter = new CounterCreationData();
                counter.CounterName = perfCounter.CounterName;
                counter.CounterType = perfCounter.CounterType;
                counter.CounterHelp = perfCounter.CounterHelp;
                counterData.Add(counter);

                // Create the category and pass the collection to it.
                PerformanceCounterCategory.Create(perfCounter.CategoryName, perfCounter.CategoryHelp, perfCounter.CategoryType, counterData);

            }

            //logger.WriteEntry("CreatePerformanceCounterCategory end.", "Info", "OrasiPerformanceCounterUtility");
            //logger.LoggerClose();
        }
        
        private static void DeletePerformanceCounterCategory(PerfCounter perfCounter)
        {
            if (PerformanceCounterCategory.Exists(perfCounter.CategoryName))
            {
                PerformanceCounterCategory.Delete(perfCounter.CategoryName);
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

            if (perfCounterParts[0].Contains("(") && perfCounterParts[0].Contains(")"))
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

                if (perfCounterParts.Length == 2)
                {
                    perfCounter.CounterName = perfCounterParts[1];
                    switch (perfCounter.CounterName.ToLower())
                    {
                        case "numberofitems":
                        case "count":
                            perfCounter.CounterType = PerformanceCounterType.NumberOfItems32;
                            break;
                        case "rateofcountspersecond":
                        case "rate/sec":
                            perfCounter.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                            break;
                        case "averagetimer":
                        case "average":
                            perfCounter.CounterType = PerformanceCounterType.AverageTimer32;
                            break;
                        default:
                            throw new Exception(string.Format("Counter name : \"{0}\" not understood. Please use \"Count\", \"Rate/Sec\", or \"Average\".", perfCounter.CounterName));
                    }
                }
                else
                {
                    throw new Exception(string.Format("Counter instance \"{0}\" provided but name not specified.", perfCounter.CounterInstanceName));
                }
            }

            //Both of these are hard coded
            perfCounter.CounterHelp = "OrasiPerformanceCounterUtility Counter help.";
            perfCounter.CategoryHelp = "OrasiPerformanceCounterUtility Category help.";
            perfCounter.CategoryType = PerformanceCounterCategoryType.MultiInstance;

            return perfCounter;
        }

        //private static List<CounterNameClass> GetCounterNames()
        //{
        //    return new List<CounterNameClass> {
        //        new CounterNameClass("Count", "OrasiPerformanceCounterUtility Count.", PerformanceCounterType.NumberOfItems32) ,
        //        new CounterNameClass("Rate/Sec", "OrasiPerformanceCounterUtility Rate/Sec.", PerformanceCounterType.RateOfCountsPerSecond32), 
        //        new CounterNameClass("Average", "OrasiPerformanceCounterUtility Average Timer.", PerformanceCounterType.AverageTimer32)
        //    };
        //}

    }//class

    public class PerfCounter
    {
        public string CategoryName { get; set; }
        public string CategoryHelp { get; set; }
        public PerformanceCounterCategoryType CategoryType { get; set; }
        public string CounterName { get; set; }
        public PerformanceCounterType CounterType { get; set; }
        public string CounterHelp { get; set; }

        public string CounterInstanceName { get; set; }

        public PerfCounter() { }
        public PerfCounter(string categoryName, string categoryHelp, PerformanceCounterCategoryType categoryType, string counterName, string counterInstanceName)
        {
            CategoryName = categoryName;
            CategoryHelp = categoryHelp;
            CategoryType = categoryType;
            CounterName = counterName;
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