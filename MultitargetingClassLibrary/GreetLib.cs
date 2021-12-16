using System;

namespace MultitargetingClassLibrary
{
    public class GreetLib
    {
        public static string Greet(string name)
        {
            return $"{DateTime.Now:HH:mm} Hello, {name}";
        }
    }
}
