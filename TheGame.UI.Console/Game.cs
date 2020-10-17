using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGame.Domain.Entities;
using TheGame.Domain.NumberGenerator;
using TheGame.Infrastructure.FileSystem;

namespace TheGame.UI.Console
{
  class Game
  {
    static void Main(string[] args)
    {
      System.Console.WriteLine("Welcome to Over or Under!");
      System.Console.WriteLine($"The rules are simple. We give you a number between {GameConfiguration.MinimumGeneratedNumber} and {GameConfiguration.MaximumGeneratedNumber}.");
      System.Console.WriteLine("Do you believe the next number will be over or under?");
      System.Console.WriteLine($"Each correct guess awards you {GameConfiguration.ScorePerCorrectGuess} point(s). You need {GameConfiguration.MinimumVictoryScore} to win!");
      System.Console.WriteLine($"Guess wrong, and you lose.");
      System.Console.WriteLine($"You will be timed! Good luck! Press any key when ready.");
      System.Console.ReadLine();

      do
      {
        System.Console.Clear();
        var leaderboardRepository = new FileSystemLeaderBoardRepository(GameConfiguration.SaveFilePath);
        var numberGenerator = new RandomNumberGenerator(1, 100);
        var currentNumber = numberGenerator.First();
        var score = 0;
        var stopwatch = Stopwatch.StartNew();

        foreach (var generatedNumber in numberGenerator.Skip(1))
        {
          System.Console.WriteLine($"Current Score: {score}");
          System.Console.WriteLine($"Your Number is: {Environment.NewLine}{currentNumber}");
          System.Console.WriteLine();

          var userInput = ReadUserGuess();
          if (userInput == UserInputs.END)
          {
            break;
          }

          System.Console.WriteLine($"The next number generated was: {generatedNumber}!");

          if (IsCorrectGuess(userInput, currentNumber, generatedNumber))
          {
            score += GameConfiguration.ScorePerCorrectGuess;
          }
          else
          {
            System.Console.WriteLine($"Game Over! You were wrong! Your high score was: {score}");
            System.Console.WriteLine("Tip: The same number can't appear twice. Judge the odds!");
            System.Console.WriteLine();
            break;
          }

          if (score >= GameConfiguration.MinimumVictoryScore)
          {
            System.Console.WriteLine($"Wow! Congratulations you won!");
            break;
          }

          currentNumber = generatedNumber;
          System.Console.Clear();
        }

        if (score > 0)
        {
          var nickname = ReadUserNickname();
          leaderboardRepository.SaveScore(nickname, score, stopwatch.ElapsedMilliseconds);
          WriteLeaderboard(leaderboardRepository.GetTopScores(GameConfiguration.MaximumTopScoresToShow));
        }

      } while (ReadUserPlayAgain());
    }

    private static void WriteLeaderboard(List<PlayerScore> topScores)
    {
      System.Console.WriteLine();
      System.Console.WriteLine("Current leaderboard:");

      foreach (var playerScore in topScores)
      {
        System.Console.WriteLine($"{playerScore.Nickname} - {playerScore.Score}");
      }
     
      System.Console.WriteLine();
    }

    private static string ReadUserNickname()
    {
      System.Console.WriteLine($"Please tell us you name, so we can track your score! (maximum {GameConfiguration.MaximumNicknameLength} characters)");
      System.Console.WriteLine();

      var userInput = System.Console.ReadLine();
      if(string.IsNullOrEmpty(userInput))
      {
        userInput = "ANONYMOUS";
      }

      if (userInput.Length > GameConfiguration.MaximumNicknameLength)
      {
        userInput = userInput.Substring(0, GameConfiguration.MaximumNicknameLength);
      }

      return userInput.ToUpper();
    }

    private static bool ReadUserPlayAgain()
    {
      System.Console.WriteLine($"Do you wish to play again? Press Y to continue, press any other key to end.");
      System.Console.WriteLine();

      var userInput = System.Console.ReadLine();
      if (userInput.StartsWith("y", true, CultureInfo.InvariantCulture))
      {
        return true;
      }

      return false;
    }

    private static UserInputs ReadUserGuess()
    {
      string userInput = null;
      do
      {
        System.Console.WriteLine($"Will the next one be over or under? Choose now:");
        System.Console.WriteLine($"O) Over");
        System.Console.WriteLine($"U) Under");
        System.Console.WriteLine($"X) Surrender (ends the game)");
        System.Console.WriteLine();

        userInput = System.Console.ReadLine();
        System.Console.WriteLine();
      } while (!IsValidUserGuess(userInput));


      if (IsOver(userInput))
      {
        return UserInputs.OVER;
      }

      if (IsUnder(userInput))
      {
        return UserInputs.UNDER;
      }

      if (IsSurrender(userInput))
      {
        return UserInputs.END;
      }

      throw new ArgumentOutOfRangeException("userInput", "The given user selection is not supported.");
    }

    private static bool IsCorrectGuess(UserInputs userInput, int currentNumber, int generatedNumber)
    {
      if (userInput == UserInputs.OVER && currentNumber < generatedNumber)
      {
        return true;
      }

      if (userInput == UserInputs.UNDER && currentNumber > generatedNumber)
      {
        return true;
      }

      return false;
    }

    static bool IsValidUserGuess(string userInput)
    {
      if (string.IsNullOrEmpty(userInput))
      {
        return false;
      }

      return IsOver(userInput) || IsUnder(userInput) || IsSurrender(userInput);
    }

    static bool IsOver(string userInput)
    {
      if (string.IsNullOrEmpty(userInput))
      {
        return false;
      }

      return userInput.StartsWith("o", true, CultureInfo.InvariantCulture);
    }
    static bool IsUnder(string userInput)
    {
      if (string.IsNullOrEmpty(userInput))
      {
        return false;
      }

      return userInput.StartsWith("u", true, CultureInfo.InvariantCulture);
    }

    static bool IsSurrender(string userInput)
    {
      if (string.IsNullOrEmpty(userInput))
      {
        return false;
      }

      return userInput.StartsWith("x", true, CultureInfo.InvariantCulture);
    }
  }
}
