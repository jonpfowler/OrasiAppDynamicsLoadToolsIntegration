using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ClrTestApp
{
    public static class Class1
    {
        public static void ClrStr1Param(string clrString1)
        {
            Console.WriteLine("ClrTestApp.Class1.ClrStr1Param string1: {0}", clrString1);
        }
        public static void ClrStr2Params(string clrString1, string clrString2)
        {
            Console.WriteLine("ClrTestApp.Class1.ClrStr2Params clrString1: {0}", clrString1);
            Console.WriteLine("ClrTestApp.Class1.ClrStr2Params clrString2: {0}", clrString2);
            File.WriteAllText("ClrTestApp.log", String.Format("ClrStr2Params, clrString1: {0}, clrString2: {1}", clrString1, clrString2));

        }
    }
}
