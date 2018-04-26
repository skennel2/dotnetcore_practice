using System.Data;

namespace MultiThreadingPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            MultiThread.TaskStaticWay();
            MultiThread.TaskInstanceWay();
            MultiThread.TaskWithReturn();
            MultiThread.CounterSyncExample();
            MultiThread.TaskFactory();

            ByteHandle.PrimitiveTypeToByte();
            ByteHandle.StringTypeToByte();
            ByteHandle.ObjectToByte();
        }
    }
}
