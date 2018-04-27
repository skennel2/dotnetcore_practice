using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace MultiThreadingPractice
{
    public static class StreamHandle
    {
        public static void WriteTextToFileUsingStreamWriter()
        {
            var fileName = @".\data\a.dat";
            
            if(File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using(var fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            using(var streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.WriteLine("Hello ");
                streamWriter.Write("World");
            }
        }

        public static void WriteBinaryToFileUsingBinaryWriter()
        {
            var fileName = @".\data\b.dat";
            
            if(File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using(var fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            using(var bWriter = new BinaryWriter(fileStream))
            {                
                bWriter.Write("Hello World");
                bWriter.Write(300);
                bWriter.Write(true);

                using(var memoryStream = new MemoryStream())
                {
                    var counter = new Counter();
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(memoryStream, counter);
                    bWriter.Write(memoryStream.ToArray());
                }
            }
        }

        public static void ReadBinaryFromFileUsingBinaryReader()
        {
            var fileName = @".\data\b.dat";

            using(var stream = new FileStream(fileName, FileMode.Open))
            using(var reader = new BinaryReader(stream))
            {
                Console.WriteLine(reader.ReadString());
                Console.WriteLine(reader.ReadInt32());
                Console.WriteLine(reader.ReadBoolean());

                var bytes = reader.ReadBytes(CalculateMemorySize(new Counter()));

                using(var memoryStream = new MemoryStream())
                {
                    memoryStream.Write(bytes, 0, bytes.Length);
                    memoryStream.Position = 0;

                    var obj = new BinaryFormatter().Deserialize(memoryStream) as Counter;    

                    Console.WriteLine(obj.ToString());
                }
            }
        }

        public static int CalculateMemorySize(object obj)
        {
            var size = 0;
            using (Stream s = new MemoryStream()) {
                var formatter = new BinaryFormatter();
                formatter.Serialize(s, obj);
                size = (int)s.Length;
            }

            return size;
        }
    }
}
