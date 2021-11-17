using System;
using System.Threading;

namespace ThreadSynchronization1
{
    class Program
    {

        private static object lockHandle = new object();
        public static int result = 0;

        public static EventWaitHandle readyForResult = new AutoResetEvent(false);
        public static EventWaitHandle setResult = new AutoResetEvent(false);



        static void Main(string[] args)
        {
            Thread t = new Thread(DoWork);
            t.Start();

            for(int i = 0; i < 100; i++)
            {
                readyForResult.Set();

                setResult.WaitOne();

                lock (lockHandle)
                {
                    Console.WriteLine(result);
                }
                Thread.Sleep(10);
            }
        }

        public static void DoWork()
        {
            while (true)
            {
                int i = result;
                Thread.Sleep(1);

                readyForResult.WaitOne();
                lock (lockHandle)
                {
                    result = i + 1;
                }
                setResult.Set();
                
                
            }
        }
    }
}
