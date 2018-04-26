using System;
using System.IO;

namespace MultiThreadingPractice
{
    public static class FileHandle
    {

        public static void FileCreate()
        {
            File.Delete("a.dat");
            var fileStream = new FileStream("a.dat", FileMode.OpenOrCreate);

            var data = BitConverter.GetBytes(8989809);
            fileStream.Write(data, 0, data.Length);

            StreamWriter writer = new StreamWriter(fileStream);
            writer.Write("aaaaa");
            writer.Close();

            fileStream.Close();
        }
    }
}
