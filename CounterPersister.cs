using System.IO;
using System.Threading.Tasks;

namespace InisBot
{
    internal class CounterPersister
    {

        private static object lockObject = new object();
        private static MahCounter _mahCounter;
        private readonly string _filePath;

        public CounterPersister(string filePath) {
            this._filePath = filePath;
            if (!File.Exists(filePath))
                File.AppendAllText(filePath, "0");
        }

        public Task PersistAsync(MahCounter counter) =>
            File.WriteAllTextAsync(this._filePath, counter.Count.ToString());

        public async Task<MahCounter> GetCounterAsync()
        {
            if (_mahCounter != null)
            {
                return _mahCounter;
            }
            var persistedCount = await File.ReadAllTextAsync(this._filePath);
            lock (lockObject)
            {
                if (_mahCounter != null)
                {
                    return _mahCounter;
                }
                _mahCounter = new MahCounter(int.Parse(persistedCount));
                _mahCounter.OnCounterIncremented += this.PersistAsync;
                return _mahCounter;
            }
        }

    }
}
