using MultiThreading.Threading;
namespace MultiThreading
{
    class Program
    {

        static void Main(string[] args)
        {
            // Imagine we have 5 games to play
            for (int i = 1; i <= 1000; i++)
            {
                int gameId = i;

                // Give the game to the ThreadPool (bench players)
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Console.WriteLine($"🎲 Game {gameId} is played by Player {Thread.CurrentThread.ManagedThreadId}");
                });
            }

            Console.WriteLine("All games given to the arcade bench!");
            Console.ReadLine();
        }
    }
}
