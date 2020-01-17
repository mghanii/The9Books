using System;

namespace Tasaneef
{
    public interface IRandom
    {
        int RandPositive(int max = int.MaxValue);
    }

    public class RandomGenerator : IRandom
    {
        private readonly Random _random = new Random();

        public int RandPositive(int max = int.MaxValue)
        {
            return _random.Next(1, max);
        }
    }
}