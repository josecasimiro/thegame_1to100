using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame.UI.Console
{
  public sealed class GameConfiguration
  {
    public static string SaveFilePath => ConfigurationManager.AppSettings["filePath"];

    public static int MinimumVictoryScore 
    {
      get
      {
        int.TryParse(ConfigurationManager.AppSettings["minimumVictoryScore"], out var minimumVictoryScore);
        return minimumVictoryScore;
      }
    }

    public static int MinimumGeneratedNumber
    {
      get
      {
        int.TryParse(ConfigurationManager.AppSettings["minimumGeneratedNumber"], out var minimumGeneratedNumber);
        return minimumGeneratedNumber;
      }
    }
    public static int MaximumGeneratedNumber
    {
      get
      {
        int.TryParse(ConfigurationManager.AppSettings["maximumGeneratedNumber"], out var maximumGeneratedNumber);
        return maximumGeneratedNumber;
      }
    }

    public static int ScorePerCorrectGuess {
      get
      {
        int.TryParse(ConfigurationManager.AppSettings["scorePerCorrectGuess"], out var scorePerCorrectGuess);
        return scorePerCorrectGuess;
      }
    }

    public static int MaximumNicknameLength
    {
      get
      {
        int.TryParse(ConfigurationManager.AppSettings["maximumNicknameLength"], out var maximumNicknameLength);
        return maximumNicknameLength;
      }
    }

    public static int MaximumTopScoresToShow
    {
      get
      {
        int.TryParse(ConfigurationManager.AppSettings["maximumTopScoresToShow"], out var maximumTopScoresToShow);
        return maximumTopScoresToShow;
      }
    }
  }
}
