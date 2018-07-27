using Microsoft.AspNetCore.Hosting;
using System;
using static System.Console;

namespace FindingImmo.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                new WebHostBuilder()
                    .UseKestrel()
                    .UseStartup<Startup>()
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                WriteLine(ex);
                ReadKey();
            }
        }
    }
}
