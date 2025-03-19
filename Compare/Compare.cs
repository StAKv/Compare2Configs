using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Compare
{
    internal class Compare : Program
    {
       
       public static void ConfigsCompare()
        {
          
            string outputFilePath = null;
            string filePath1 = Args[0];
            string filePath2 = Args[1];
            if (File.Exists(filePath1))
            {
                Console.WriteLine("Файл 1 существует.");
            }
            else
            {
                Console.WriteLine("Файл 1 не существует.");
            }
            if (File.Exists(filePath2))
            {
                Console.WriteLine("Файл 2 существует.");
            }
            else
            {
                Console.WriteLine("Файл 2 не существует.");
            }
            try
            {
                if (Args[2] != null)
                {
                    outputFilePath = Args[2];
                };

            }
            catch { };

            // Чтение файлов
            byte[] file1Bytes = File.ReadAllBytes(filePath1);
            byte[] file2Bytes = File.ReadAllBytes(filePath2);

            // Сравнение файлов
            if (AreFilesEqual(file1Bytes, file2Bytes))
            {
                Console.WriteLine("Файлы идентичны.");
            }
            else
            {
                Console.WriteLine("Файлы различаются.");
                CompareAndWriteDifferences(file1Bytes, file2Bytes, outputFilePath );
            }
        }

        static bool AreFilesEqual(byte[] file1, byte[] file2)
        {
            if (file1.Length != file2.Length)
                return false;

            for (int i = 0; i < file1.Length; i++)
            {
                if (file1[i] != file2[i])
                    return false;
            }

            return true;
        }

        static void CompareAndWriteDifferences(byte[] file1, byte[] file2, string? outputFilePath)
        {
            if (outputFilePath != null)
            {
                using (var writer = new StreamWriter(outputFilePath))
                {
                    int minLength = Math.Min(file1.Length, file2.Length);

                    for (int i = 0; i < minLength; i++)
                    {
                        if (file1[i] != file2[i])
                        {
                            writer.WriteLine($"Различие в позиции {i}:");
                            writer.WriteLine($"  Файл 1: {file1[i]:X2}");
                            writer.WriteLine($"  Файл 2: {file2[i]:X2}");
                        }
                    }

                    if (file1.Length != file2.Length)
                    {
                        writer.WriteLine($"Файлы имеют разную длину:");
                        writer.WriteLine($"  Файл 1: {file1.Length} байт");
                        writer.WriteLine($"  Файл 2: {file2.Length} байт");
                    }
                }

                Console.WriteLine($"Различия записаны в файл: {outputFilePath}");
            }
            else if(outputFilePath == null )
            {

                int minLength = Math.Min(file1.Length, file2.Length);

                for (int i = 0; i < minLength; i++)
                {
                    if (file1[i] != file2[i])
                    {
                        Console.WriteLine($"Различие в позиции {i}:");
                        Console.WriteLine($"  Файл 1: {file1[i]:X2}");
                        Console.WriteLine($"  Файл 2: {file2[i]:X2}");
                    }
                }

                if (file1.Length != file2.Length)
                {
                    Console.WriteLine($"Файлы имеют разную длину:");
                    Console.WriteLine($"  Файл 1: {file1.Length} байт");
                    Console.WriteLine($"  Файл 2: {file2.Length} байт");
                }

                Console.WriteLine("Различия выведены в консоль.");


            }
        }
    }
}
