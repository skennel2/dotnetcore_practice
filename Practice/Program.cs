using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadingPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task 클래스 Api
            // https://docs.microsoft.com/ko-kr/dotnet/api/system.threading.tasks.task?view=netcore-2.0
            // public class Task : IAsyncResult, IDisposable
            // Task 는 값을 반환하지 않는 비동기적 작업을 표현한다. 
            // Task<TResult> 는 값을 반환하는 비동기적 작업을 표현한다. 
            TaskStaticWay();
            TaskInstanceWay();
            TaskWithParameter();
            TaskWithReturn();
            CounterTaskExample();
            TaskWaitAny();
        }

        static void TaskStaticWay()
        {
            Console.WriteLine("TaskStaticWay");

            int i = 0;
            Task t1 = Task.Factory.StartNew(() => { i++; }); // 따로 Thread를 Start시키는 메소드의 호출이 필요없다. 
            Task t2 = Task.Factory.StartNew(() => { i++; });
            Task t3 = Task.Factory.StartNew(() => { i++; });
            Task t4 = Task.Run(() => { i++; }); // 따로 Thread를 Start시키는 메소드의 호출이 필요없다. 
            //t4.Start();

            i++;
            //t4.Wait();
            Console.WriteLine(i); // 1

            Thread.Sleep(50);

            Console.WriteLine(i); // 5
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

        // 파라미터를 가지는 Task
        static void TaskWithParameter()
        {
            Console.WriteLine("TaskWithParameter");
            Action<object> action = (o) => 
            {
                Console.WriteLine(o.ToString());
            };

            Task t1 = new Task(action, 123123123);
            t1.Start();
        }

        // 제네릭으로 반환값을 가질수 있는 Task
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

        // Task, lock 키워드
        static void CounterTaskExample()
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

            // 0 출력 
            Console.WriteLine(counter.Count);
        }

        //     Status of all tasks:
        //        Task #3: Running
        //        Task #1: RanToCompletion
        //        Task #4: Running
        static void TaskWaitAny()
        {
            var tasks = new Task[3];
            var rnd = new Random();
            for (int ctr = 0; ctr <= 2; ctr++)
            {
                tasks[ctr] = Task.Run( () => Thread.Sleep(rnd.Next(500, 3000)));
            }

            try 
            {
                int index = Task.WaitAny(tasks);
                Console.WriteLine("Task #{0} completed first.\n", tasks[index].Id);
                Console.WriteLine("Status of all tasks:");
                foreach (var t in tasks)
                {
                    Console.WriteLine("   Task #{0}: {1}", t.Id, t.Status);
                }
            }
            catch (AggregateException)
            {
                Console.WriteLine("An exception occurred.");
            }
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
                // 인스턴스의 내부 상태값을 변화 시킨다. 
                // 여러 스레드에서 경쟁상태가 될수 있기 때문에 lock 으로 묶었다. 
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
                // 인스턴스의 내부 상태값을 변화 시킨다. 
                // 여러 스레드에서 경쟁상태가 될수 있기 때문에 lock 으로 묶었다.                 
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
