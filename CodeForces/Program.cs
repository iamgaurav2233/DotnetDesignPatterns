using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;

public class Program
{
    public static void Main(string[] args)
    {
        int n = 1;
        while (n != 0)
        {
            n--;
            Solve();
        }
    }

    public static void Solve()
    {
        int[] nm = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int cnt = nm[0];
        while (cnt != 0)
        {
            cnt--;
            int op;
            char ch;
            object[] arr = Console.ReadLine().Split().Select().ToArray();
            op = arr[0];
            ch = (char)arr[1];
            Console.WriteLine(op + " " + ch);
        }
    }


    public static int[] ReadIntArray()
    {
        return Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
    }

    public static long[] ReadLongArray()
    {
        return Console.ReadLine().Split(' ').Select(long.Parse).ToArray();
    }
}