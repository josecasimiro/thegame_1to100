using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGame.Domain.Entities;
using TheGame.Domain.Leaderboard;

namespace TheGame.Infrastructure.FileSystem
{
  public class FileSystemLeaderBoardRepository : ILeaderboardRepository
  {
    private const char PropertySeparator = ';';
    private readonly List<PlayerScore> _scores = null;
    private readonly string _filePath = null;

    public FileSystemLeaderBoardRepository(string filePath)
    {
      this._filePath = filePath;
      this._scores = this.ReadScoresFromFile();
    }

    public List<PlayerScore> GetTopScores(int take)
    {
      return this._scores.OrderByDescending(s => s.Score).ThenBy(s => s.TimeTakenMiliseconds).Take(take).ToList();
    }

    public void SaveScore(string nickname, int score, long timeTakenMiliseconds)
    {
      var playerScore = new PlayerScore(nickname, score, timeTakenMiliseconds);
      _scores.Add(playerScore);

      this.SaveScoreToFile(playerScore);
    }

    private void SaveScoreToFile(PlayerScore playerScore)
    {
      try
      {
        var formattedPlayerScore = this.FormatPlayerScore(playerScore);

        if (!File.Exists(this._filePath))
        {
          File.WriteAllText(this._filePath, formattedPlayerScore);
        }
        else
        {
          File.AppendAllText(this._filePath, formattedPlayerScore);
        }
      }
      catch (Exception)
      {
        // TODO: Handle error display to user
        throw;
      }
    }

    private List<PlayerScore> ReadScoresFromFile()
    {
      var leaderboard = new List<PlayerScore>();

      if (!File.Exists(this._filePath))
      {
        return leaderboard;
      }

      try
      {
        using (var stream = new StreamReader(this._filePath))
        {
          while (!stream.EndOfStream)
          {
            var line = stream.ReadLine();
            var playerScore = this.ParsePlayerScore(line);
            if (playerScore != null)
            {
              leaderboard.Add(playerScore);
            }
          }

          return leaderboard;
        }
      }
      catch (Exception)
      {
        // TODO: Handle error display to user.
        throw;
      }
    }

    private string FormatPlayerScore(PlayerScore playerScore)
    {
      return $"{Environment.NewLine}{playerScore.Nickname}{PropertySeparator}{playerScore.Score}{PropertySeparator}{playerScore.TimeTakenMiliseconds}";
    }

    private PlayerScore ParsePlayerScore(string fileLine)
    {
      var values = fileLine.Split(PropertySeparator);

      var nickname = values[0];
      if (string.IsNullOrEmpty(nickname))
      {
        return null;
      }

      if (!int.TryParse(values[1], out var score))
      {
        return null;
      }

      if (!long.TryParse(values[2], out var timeTaken))
      {
        return null;
      }

      return new PlayerScore(nickname, score, timeTaken);
    }
  }
}
