using System;
namespace DesignPatterns
{
    class Program
    {
        enum Application
        {
            Carwale = 1,
            Bikewale = 2,
            Cartrade = 3,
            Autobiz = 4,
            MobilityOutlook = 5,
            OLX = 6
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Application.Carwale);
        }
    }
}

