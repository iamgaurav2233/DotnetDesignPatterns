using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
namespace TicTacToe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ticTacToe = new TicTacToeGame();
            Console.WriteLine("Game Result :" + ticTacToe.StartGame());
        }
    }
}
