using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadingPractice
{
    class MultiThread
    {
        public static void TaskStaticWay()
        {
            Console.WriteLine("TaskStaticWay");

            var i = 0;
            Task.Factory.StartNew(() => { i++; });
            Task.Factory.StartNew(() => { i++; });
            Task.Factory.StartNew(() => { i++; });

            i++;

            Console.WriteLine(i);

            Thread.Sleep(50);

            Console.WriteLine(i);
        }

        public static void TaskInstanceWay()
        {
            Console.WriteLine("TaskInstanceWay");

            var i = 0;
            var task1 = new Task(() =>
            {
                i++;
                Console.WriteLine("task1 done");
            });
            var task2 = new Task(() =>
            {
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

        public static void TaskWithReturn()
        {
            Console.WriteLine("TaskWithReturn");

            var i = 1;

            var t1 = new Task<int>(() => 3 + i);

            t1.Start();

            Console.WriteLine(t1.Result); // 4

            t1.Wait();

            Console.WriteLine(t1.Result); // 4 ?
        }

        public static void CounterSyncExample()
        {
            Console.WriteLine("CounterSyncExample");

            Counter counter = new Counter();

            // 하나의 인스턴스에 여러 스레드가 붙는다.
            // 각각의 스레드는 인스턴스의 필드값을 변화시킨다. 
            var t1 = new Task(() => counter.Increase("t1"));
            var t2 = new Task(() => counter.Decrease("t2"));

            t1.Start();
            t2.Start();

            t1.Wait();
            t2.Wait();

            Console.WriteLine(counter.Count);
        }

        public static void TaskFactory()
        {
            Console.WriteLine("TaskFactory");

            var tasks = new Task[2];
            String[] files = null;
            String[] dirs = null;
            var docsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            tasks[0] = Task.Factory.StartNew(() => files = Directory.GetFiles(docsDirectory));
            tasks[1] = Task.Factory.StartNew(() => dirs = Directory.GetDirectories(docsDirectory));

            Task.Factory.ContinueWhenAll(tasks, completedTasks =>
            {
                Console.WriteLine("{0} contains: ", docsDirectory);
                Console.WriteLine("   {0} subdirectories", dirs.Length);
                Console.WriteLine("   {0} files", files.Length);
            });
        }
    }
}
