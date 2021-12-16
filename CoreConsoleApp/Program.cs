using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace CoreConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddCommandLine(args, new Dictionary<string, string>()
            {
                ["-name"] = "Name"
            });

            var config = builder.Build();

            Console.WriteLine($"Hello {config["Name"]}");
        }
    }
}
