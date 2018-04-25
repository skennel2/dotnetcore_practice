using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadingPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskStaticWay();
            TaskInstanceWay();
            TaskWithReturn();
            CounterSyncExample();
        }

        static void TaskStaticWay()
        {
            Console.WriteLine("TaskStaticWay");

            int i = 0;
            Task.Factory.StartNew(() => { i++; });
            Task.Factory.StartNew(() => { i++; });
            Task.Factory.StartNew(() => { i++; });

            i++;

            Console.WriteLine(i);

            Thread.Sleep(50);

            Console.WriteLine(i);
        }

        static void TaskInstanceWay()
        {
            Console.WriteLine("TaskInstanceWay");

            int i = 0;
            Task task1 = new Task(() => 
            {
                i++;
                Console.WriteLine("task1 done");
            });
            Task task2 = new Task(() => { 
                i++; 
                Console.WriteLine("task2 done");
            });

            task1.Start();
            task2.Start();

            Thread.Sleep(50);
            Console.WriteLine(i);

            task1.Wait();
            task2.Wait();

            Console.WriteLine(i);
        }

        static void TaskWithReturn()
        {
            Console.WriteLine("TaskWithReturn");

            int i = 1;

            Task<int> t1 = new Task<int>(() => 3 + i);

            t1.Start(); 

            Console.WriteLine(t1.Result); // 4

            t1.Wait();

            Console.WriteLine(t1.Result); // 4 ?
        }

        static void CounterSyncExample()
        {
            Console.WriteLine("CounterSyncExample");

            Counter counter = new Counter();

            // 하나의 인스턴스에 여러 스레드가 붙는다.
            // 각각의 스레드는 인스턴스의 필드값을 변화시킨다. 
            Task t1 = new Task(() => counter.Increase("t1"));
            Task t2 = new Task(() => counter.Decrease("t2"));

            t1.Start();
            t2.Start();

            t1.Wait();
            t2.Wait();

            Console.WriteLine(counter.Count);
        }

    }

    public class Counter
    {
        const int LOOP_COUNT = 10;

        private int count;

        private readonly object lockObject;

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
            int loopCount = LOOP_COUNT;
            while(loopCount--  > 0)
            {
                lock(lockObject)
                {
                    count++;
                }
                Console.WriteLine(tName + ":" + count);
                Thread.Sleep(1);

            }
        }

        public void Decrease(String tName)
        {
            int loopCount = LOOP_COUNT;
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
    }
}
