using System;
using System.IO;
using Newtonsoft.Json;

namespace MultiThreadingPractice
{
    public static class JsonSerializeHandle
    {
        public static void JsonSerializeAndDeserializeUsingJsonNet()
        {
            const String filePath = @".\data\j.dat";

            // 직렬화
            var value = JsonConvert.SerializeObject(
                new Student()
                {
                    Name="na", 
                    Age = 20, 
                    Department = new Department(){ Name="ERP" }
                });

            File.Delete(filePath);
            using(FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            using(StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.WriteLine(value);
            }

            // 역직렬화
            using(FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            using(var streamReader = new StreamReader(fileStream))
            {
                var str = streamReader.ReadLine();

                Student student = JsonConvert.DeserializeObject<Student>(str);

                Console.WriteLine(student.Name);
                Console.WriteLine(student.Age);
            }
        }
    }
}