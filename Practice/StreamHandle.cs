using System;
using System.IO;

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
            }
        }
    }
}
