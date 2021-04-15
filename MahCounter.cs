using System;

namespace InisBot
{
    internal class MahCounter
    {

        public event Action<MahCounter> OnCounterIncremented;
        public int Count { private set; get; } = 0;


        public MahCounter(int initialCount)
        {
            this.Count = initialCount;
        }

        public void Increment(int by = 1)
        {
            this.Count += by;
            this.OnCounterIncremented?.Invoke(this);
        }

    }
}
