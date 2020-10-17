using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using TheGame.Domain.Entities;

namespace TheGame.Domain.Leaderboard
{
  public interface ILeaderboardRepository
  {
    List<PlayerScore> GetTopScores(int take);
    void SaveScore(string nickname, int score, long timeTakenMiliseconds);
  }
}
