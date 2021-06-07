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

        int winnerHalf = uniqueArgs.Length / 2;

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
          if(winnerHalf >= Array.IndexOf(uniqueArgs, computerMove) & Convert.ToInt32(value) != Convert.ToInt32(choise)) {
            Console.WriteLine("You win!");
          }
          else if(Convert.ToInt32(value) == Convert.ToInt32(choise)) {
            Console.WriteLine("Tie!");
          }
          else {
            Console.WriteLine("You lose!");
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
