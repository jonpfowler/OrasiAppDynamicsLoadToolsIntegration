using System;
using System.Diagnostics;

namespace PerfCounterExample
{
    class Program
    {
        static void Main(string[] args)
        {
            AddMultipleInstanceCounter();
            return;

            string performanceCounterCategoryName = "PerformanceCounterExampleCategory";
            string performanceCounterCategoryHelp = "Example category help.";

            string performanceCounterName = "Counter1";
            string performanceCounterHelp = "Counter1 help.";

            if (!PerformanceCounterCategory.Exists(performanceCounterCategoryName))
            {

                CounterCreationDataCollection counterData = new CounterCreationDataCollection();
                CounterCreationData counter = new CounterCreationData();
                counter.CounterName = performanceCounterName;
                counter.CounterType = PerformanceCounterType.NumberOfItems32;
                counter.CounterHelp = performanceCounterHelp;
                counterData.Add(counter);
                PerformanceCounterCategory.Create(performanceCounterCategoryName, performanceCounterCategoryHelp, PerformanceCounterCategoryType.SingleInstance, counterData);
                PerformanceCounter myCounter = new PerformanceCounter(performanceCounterCategoryName, performanceCounterName, false);
                myCounter.RawValue = 0;
            }

            PerformanceCounter myCounterInstance = new PerformanceCounter(performanceCounterCategoryName, performanceCounterName, false);
            Console.WriteLine("Press Ctrl+C to exit.");
            while (true)
            {
                myCounterInstance.IncrementBy(1);
                Console.WriteLine("{0}:\t{1}", performanceCounterName, myCounterInstance.RawValue);
                System.Threading.Thread.Sleep(5000);
            }
        }

        private static void AddMultipleInstanceCounter()
        {
            string performanceCounterCategoryName = "PerformanceCounterExampleCategoryMultiInstance";
            string performanceCounterCategoryHelp = "Example category help.";

            string performanceCounterName = "Counter1";
            string performanceCounterInst = "Instance1";
            string performanceCounterHelp = "Counter1 help.";

            if (!PerformanceCounterCategory.Exists(performanceCounterCategoryName))
            {

                CounterCreationDataCollection counterData = new CounterCreationDataCollection();
                CounterCreationData counter = new CounterCreationData();
                counter.CounterName = performanceCounterName;
                counter.CounterType = PerformanceCounterType.NumberOfItems32;
                counter.CounterHelp = performanceCounterHelp;
                counterData.Add(counter);
                PerformanceCounterCategory.Create(performanceCounterCategoryName, performanceCounterCategoryHelp, PerformanceCounterCategoryType.MultiInstance, counterData);
                PerformanceCounter myCounter = new PerformanceCounter(performanceCounterCategoryName, performanceCounterName, performanceCounterInst, false);
                myCounter.RawValue = 0;
            }

            PerformanceCounter myCounterInstance = new PerformanceCounter(performanceCounterCategoryName, performanceCounterName, performanceCounterInst, false);
            myCounterInstance.InstanceName = performanceCounterInst;
            Console.WriteLine("Press Ctrl+C to exit.");
            while (true)
            {
                myCounterInstance.IncrementBy(1);
                Console.WriteLine("{0}:\t{1}", performanceCounterName, myCounterInstance.RawValue);
                System.Threading.Thread.Sleep(5000);
            }
            
        }
    }
}
