using System;
using System.Runtime.Serialization;
using System.Threading;

namespace MultiThreadingPractice
{
    [Serializable]
    public class Counter
    {
        const int LOOP_COUNT = 10;

        int count;

        readonly object lockObject;

        public Counter()
        {
            count = 0;
            lockObject = new object();
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public void Increase(String tName)
        {
            var loopCount = LOOP_COUNT;
            while (loopCount-- > 0)
            {
                lock (lockObject)
                {
                    count++;
                }
                Console.WriteLine(tName + ":" + count);
                Thread.Sleep(1);

            }
        }

        public void Decrease(String tName)
        {
            var loopCount = LOOP_COUNT;
            while (loopCount-- > 0)
            {
                lock (lockObject)
                {
                    count--;
                }
                Thread.Sleep(1);
                Console.WriteLine(tName + ":" + count);
            }
        }

		public override string ToString()
		{
            return "Hello";
		}
	}
}
