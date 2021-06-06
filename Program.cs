using System;
using System.Text;
using System.Linq;
using System.Security.Cryptography;

namespace rockPaperScissors
{
  class Program
  {
    static void Main(string[] args)
    {
      string[] soft = args;
      var newArgs = soft.Concat(args).Distinct();
      string[] uniqueArgs = newArgs.ToArray();

      if (uniqueArgs.Length >= 3 & uniqueArgs.Length % 2 == 1)
      {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] keyBuffer = new byte[16];
        rng.GetBytes(keyBuffer);

        static string BufferToString(byte[] buffer)
        {
          return BitConverter.ToString(buffer).Replace("-", string.Empty);
        }

        int availableMoves = 0;

        foreach (var i in uniqueArgs)
        {
          ++availableMoves;
        };

        Random computerChoise = new Random();
        int value = computerChoise.Next(0, ++availableMoves);
        string computerMove = uniqueArgs[value - 1];

        var instance = new HMACSHA256(keyBuffer);

        byte[] initialBuffer = Encoding.ASCII.GetBytes(computerMove);
        var hash = instance.ComputeHash(initialBuffer);

        Console.WriteLine($"HMAC: {BufferToString(hash)}");

        Console.WriteLine("Available moves: ");

        availableMoves = 0;
        foreach (var i in uniqueArgs)
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
        string yourMove = uniqueArgs[Convert.ToInt32(choise) - 1];
        Console.WriteLine($"Your move: {yourMove}");
        Console.WriteLine($"Computer move: {computerMove}");

        int turns = uniqueArgs.Length;
        int winnerHalf = turns / 2;

        string tmp;
        int nextInd = 0;

        for (int i = 0; i < uniqueArgs.Length - 1; ++i)
        {
          nextInd += Convert.ToInt32(uniqueArgs.Length - Convert.ToInt32(choise) + 1);
          nextInd %= uniqueArgs.Length;

          tmp = uniqueArgs[nextInd];
          uniqueArgs[nextInd] = uniqueArgs[0];
          uniqueArgs[0] = Convert.ToString(tmp);
        }
        Console.WriteLine(string.Join(" ", uniqueArgs));
        for (var i = 1; i <= winnerHalf; i++)
        {
          Console.WriteLine(i);
          if (Convert.ToInt32(value) == i & Convert.ToInt32(choise) != Convert.ToInt32(value)) //TODO
          {
            Console.WriteLine("You win!");
            break;
          }
          else if (Convert.ToInt32(choise) == Convert.ToInt32(value))
          {
            Console.WriteLine("Tie!");
            break;
          }
          else
          {
            Console.WriteLine("You lose!");
            break;
          }
        }

        Console.WriteLine($"HMAC key: {BufferToString(keyBuffer)}");
      }
      else
      {
        Console.WriteLine("Invalid number of strings");
      }
    }
  }
}
