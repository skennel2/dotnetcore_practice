using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MultiThreadingPractice
{
    public static class ByteHandle
    {
        public static void PrimitiveTypeToByte()
        {
            Console.WriteLine("PrimitiveTypeToByte");
            int value = 100;

            var bytes = BitConverter.GetBytes(value);

            foreach (byte b in bytes)
            {
                Console.WriteLine(b);
            }
        }

        public static void StringTypeToByte()
        {
            Console.WriteLine("StringTypeToByte");

            var stringValue = "gggg bbbb";

            var bytesToString = Encoding.Unicode.GetBytes(stringValue);

            foreach (byte b in bytesToString)
            {
                Console.WriteLine(b);
            }
        }

        public static void ObjectToByte()
        {
            Console.WriteLine("ObjectToByte");

            Counter counter = new Counter();

            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, counter);

            var bytes = memoryStream.ToArray();

            foreach (byte b in bytes)
            {
                Console.WriteLine(b);
            }

            ByteToObject(bytes);
        }

        public static void ByteToObject(byte[] bytes)
        {
            Console.WriteLine("ByteToObject");
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream();
            memoryStream.Write(bytes, 0, bytes.Length);
            memoryStream.Seek(0, SeekOrigin.Begin); // 뭔지모르겠당 ....
            var obj = binaryFormatter.Deserialize(memoryStream) as object;

            Console.WriteLine(obj);
        }
    }
}
