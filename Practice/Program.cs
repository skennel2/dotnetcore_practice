﻿using System.Data;
﻿using System;
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
