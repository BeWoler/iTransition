using System;
using System.Security.Cryptography;

namespace rockPaperScissors
{
  class Program
  {
    static void Main(string[] args)
    {
      secureRandom(16);
      
      if (args.Length >= 3 & args.Length % 2 == 1)
      {
        int turns = args.Length;
        Console.WriteLine("Available moves: ");
        int availableMoves = 0;

        foreach (var i in args)
        {
          Console.WriteLine($"{++availableMoves}: {i}");
        };
        Console.WriteLine("0: Exit");
        Console.Write("Enter your move: ");
        string choise = Console.ReadLine();
        if (choise == "0")
        {
          return;
        }
        string yourMove = args[Convert.ToInt32(choise) - 1];
        Console.WriteLine($"Your move: {yourMove}");

        Random computerChoise = new Random();
        int value = computerChoise.Next(0, ++availableMoves);
        string computerMove = args[value - 1];
        Console.WriteLine($"Computer move: {computerMove}");
      }
      else
      {
        Console.WriteLine("Invalid number of strings");
      }
    }

    public static string secureRandom(int size)
    {
      RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
      byte[] buff = new byte[size];
      rng.GetBytes(buff);
      Console.WriteLine(Convert.ToBase64String(buff));
      return Convert.ToBase64String(buff);
    }
  }
}
