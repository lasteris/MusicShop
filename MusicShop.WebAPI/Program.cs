﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using System.Diagnostics;
using System.IO;

namespace MusicShop.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // получаем путь к файлу 
            var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            // путь к каталогу проекта
            var pathToContentRoot = Path.GetDirectoryName(pathToExe);
            // создаем хост
            var host = WebHost.CreateDefaultBuilder(args)
                    .UseContentRoot(pathToContentRoot)
                    .UseStartup<Startup>()
                    .Build();
            // запускаем в виде службы
            host.RunAsService();
        }
    }
}
