﻿using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

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

    public class ByteHandle
    {
        public static void PrimitiveTypeToByte()
        {
            Console.WriteLine("PrimitiveTypeToByte");
            int value = 100;

            byte[] bytes = BitConverter.GetBytes(value);

            foreach(byte b in bytes)
            {
                Console.WriteLine(b);
            }
        }

        public static void StringTypeToByte()
        {
            Console.WriteLine("StringTypeToByte");

            String stringValue = "gggg bbbb";

            byte[] bytesToString = Encoding.Unicode.GetBytes(stringValue);

            foreach (byte b in bytesToString)
            {
                Console.WriteLine(b);
            }
        }

        public static void ObjectToByte()
        {
            Console.WriteLine("ObjectToByte");

            Counter counter = new Counter();

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, counter);

            byte[] bytes = memoryStream.ToArray();

            foreach (byte b in bytes)
            {
                Console.WriteLine(b);
            }

            ByteToObject(bytes);
        }

        public static void ByteToObject(byte[] bytes)
        {
            Console.WriteLine("ByteToObject");
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(bytes, 0, bytes.Length);
            memoryStream.Seek(0, SeekOrigin.Begin); // 뭔지모르겠당 ....
            object obj = binaryFormatter.Deserialize(memoryStream) as object;

            Console.WriteLine(obj.ToString());
        }
    }
}