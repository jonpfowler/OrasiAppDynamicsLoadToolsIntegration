using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClrTestApp
{
    public static class Class1
    {
        public static void ClrStr1Param(string string1)
        {
            Console.WriteLine("ClrTestApp.Class1.ClrStr1Param string1: {0}", string1);
        }
        public static void ClrStr2Params(string string1, string string2)
        {
            Console.WriteLine("ClrTestApp.Class1.ClrStr2Params string1: {0}", string1);
            Console.WriteLine("ClrTestApp.Class1.ClrStr2Params string2: {0}", string2);
        }
    }
}
