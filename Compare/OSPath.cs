using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Compare
{
    internal class OSPath : Program 
    {
        
        public static string OSCheck()
        {
            string OSName = null;
           
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                OSName = "Linux";
            }
            else
            {
                OSName = "Other";
            }
          
            return OSName;
        }
        public static string[] ChangerPath(string[] arg )
        {
            if (OSCheck() == "Linux" && DetectPathOS(arg[0]) =="Windows")
            {
                arg[0]=  ConvertWindowsPathToLinux(arg[0]);
                arg[1]= ConvertWindowsPathToLinux(arg[1]);
                try
                {
                    if (arg[2] != null)
                    {
                        arg[2] =ConvertWindowsPathToLinux(arg[2]);
                    }
                }
                catch { }
            }
            else if (OSCheck() != "Linux" && DetectPathOS(arg[1]) != "Windows")
            {
                Console.WriteLine("Укажите Диск:");
                string disk = Console.ReadLine().ToString();
                arg[0] = ConvertLinuxPathToWindows(arg[0], disk);
                arg[1] = ConvertLinuxPathToWindows(arg[1], disk);
                try
                {
                    if (arg[2] != null)
                    {
                        arg[2] = ConvertLinuxPathToWindows(arg[2], disk);
                    }
                }
                catch
                {

                };


            }
            return arg;
        }
        static string ConvertLinuxPathToWindows(string linuxPath, string disk)
        {
            // Заменяем прямые слэши на обратные
            string windowsPath = linuxPath.Replace('/', '\\');

            // Добавляем букву диска 
            windowsPath = $"{disk}:" + windowsPath;

            return windowsPath;
        }
        static string ConvertWindowsPathToLinux(string windowsPath)
        {
            // Удаляем букву диска и двоеточие (например, "C:")
            if (windowsPath.Length > 2 && windowsPath[1] == ':')
            {
                windowsPath = windowsPath.Substring(2);
            }

            // Заменяем обратные слэши на прямые
            return windowsPath.Replace('\\', '/');
        }
        static string DetectPathOS(string path)
        {
            // Проверяем, содержит ли путь обратные слэши
            if (path.Contains('\\'))
            {
                return "Windows";
            }

            // Проверяем, начинается ли путь с буквы диска (например, "C:")
            if (path.Length > 1 && path[1] == ':')
            {
                return "Windows";
            }

            // Проверяем, начинается ли путь с корневого каталога Linux
            if (path.StartsWith("/"))
            {
                return "Linux/macOS";
            }

          
            return "Неизвестно";
        }
    }
}

  
