using System;
using Serilog;
using Microsoft.Extensions.DependencyInjection;

namespace Company.ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int exitCode;
            try
            {
                Console.Title = "APPLICATION-NAME";
                var serviceProvider = Startup.CreateServiceProvider();                
                exitCode = serviceProvider.GetService<Application>().Run(args);
            }
            finally
            {
                Log.CloseAndFlush();
            }
            Environment.Exit(exitCode);
        }
    }
}
