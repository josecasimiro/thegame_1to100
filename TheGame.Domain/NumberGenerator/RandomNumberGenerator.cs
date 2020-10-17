using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame.Domain.NumberGenerator
{
  public class RandomNumberGenerator : INumberGenerator
  {
    private readonly Random _random = new Random(DateTime.Now.Millisecond);
    private readonly IEnumerable<int> _offers = null;

    public RandomNumberGenerator()
      : this(0, 100)
    {
    }

    public RandomNumberGenerator(int end)
      : this(0, end)
    {
    }

    public RandomNumberGenerator(int start, int end)
    {
      if (start > end)
      {
        this._offers = new List<int>();
        return;
      }

      this._offers = Enumerable.Range(start, end - start).OrderBy(i => _random.Next());
    }

    public IEnumerator<int> GetEnumerator()
    {
      foreach (var offer in _offers)
      {
        yield return offer;
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
