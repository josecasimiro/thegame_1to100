using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame.Domain.Entities
{
  public class PlayerScore
  {
    public PlayerScore(string nickname, int score, long timeTakenMiliseconds)
    {
      this.Nickname = nickname;
      this.Score = score;
      this.TimeTakenMiliseconds = timeTakenMiliseconds;
    }
    public string Nickname { get; set; }

    public int Score { get; set; }

    public long TimeTakenMiliseconds { get; set; }
  }
}
