using System;

namespace DesignPatterns
{
    public sealed class SingletonDesign
    {
        // Proper Lazy initialization
        private static readonly Lazy<SingletonDesign> instance =
            new Lazy<SingletonDesign>(() => new SingletonDesign());

        // Private constructor
        private SingletonDesign()
        {
            Console.WriteLine("This is implemented");
        }

        // Public property to access instance
        public static SingletonDesign getInstance
        {
            get
            {
                return instance.Value;
            }
        }
    }
}
