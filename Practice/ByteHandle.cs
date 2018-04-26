using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MultiThreadingPractice
{
    public static class ByteHandle
    {
        /// <summary>
        /// BitConverter 클래스로 기본타입을 바이트로 변환한다.
        /// </summary>
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

        /// <summary>
        /// String type을 문자열 인코딩을 통해 바이트로 변환한다.
        /// </summary>
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

        /// <summary>
        /// 오브젝트를 BinaryFormatter의 직렬화기능으로 바이트변환한다. 
        /// 변환하려는 클래스 타입에 [Serializable]를 선언해주어야한다.
        /// </summary>
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

        /// <summary>
        /// 직렬화된 바이트를 BinaryFormatter클래스를 이용하여 오브젝트로 변환한다.
        /// </summary>
        /// <param name="bytes">Bytes.</param>
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
