using Microsoft.Extensions.Configuration;
using MultitargetingClassLibrary;
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
                ["-name"] = "name"
            });

            var config = builder.Build();

            Console.WriteLine(GreetLib.Greet(config["name"]));
        }
    }
}
