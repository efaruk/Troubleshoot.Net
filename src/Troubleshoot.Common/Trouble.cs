using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Troubleshoot.Common
{
    public static class Trouble
    {
        public static List<byte[]> LeakingList = new List<byte[]>(10);

        public static void CrashStackoverflow(long k)
        {
            var i = k + k;
            // Recursive call without any break...
            CrashStackoverflow(i);
        }

        public static long MemoryLeak()
        {
            while (LeakingList.Count < 1000000000)
            {
                try
                {
                    LeakingList.Add(Encoding.UTF8.GetBytes("Troubleshoot .Net - Startup to Enterprise (.Net Unleashed)"));
                }
                catch (Exception)
                {
                    break;
                }
            }

            long l = 0;
            foreach (var bytez in LeakingList)
            {
                l += bytez.LongLength;
            }
            return l;
        }

        private static int _counter;
        private static readonly object CounterLock = new object();
        public static void CounterIncrement()
        {
            lock (CounterLock)
            {
                _counter++;
                Thread.Sleep(500);
            }
        }

        public static int GetRequestCount()
        {
            int count;
            lock (CounterLock)
            {
                count = _counter;
                Thread.Sleep(500);
            }
            return count;
        }

        public static void WrangCall()
        {
            var business = new Business();
            var ds = business.GetProductsByCategories();
        }

    }
}
