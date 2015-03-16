using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrasiPerformanceCounterUtility;

namespace PerformanceCounterUtilityConsole
{
    class OrasiPerformanceCounterUtilityConsole
    {
        static void Main(string[] args)
        {
            string myCategory = "Prudent Counters";
            string myInstance = "Instance1";
            Counter.DeleteCounterCategory(myCategory);
            Counter.ResetCounter(string.Format("{0}({1})", myCategory, myInstance), 0);
            Counter.IncrementCounter(string.Format("{0}({1})", myCategory, myInstance), 1);
            Counter.IncrementCounter(string.Format("{0}({1})", myCategory, myInstance), 1);
            Counter.IncrementCounter(string.Format("{0}({1})", myCategory, myInstance), 1);
        }
    }
}
