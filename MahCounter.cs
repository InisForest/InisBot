using System;
using System.Threading.Tasks;

namespace InisBot
{
    internal class MahCounter
    {

        public event Func<MahCounter, Task> OnCounterIncremented;

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

        public void Set(int to)
        {
            this.Count = to;
            this.OnCounterIncremented?.Invoke(this);
        }

    }
}
