using Grimoire.Networking;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Grimoire.Tools
{
    public class Spammer
    {
        private List<string> _packets;

        private int _delay;

        private CancellationTokenSource _cancellationTokenSource;

        public static Spammer Instance
        {
            get;
        } = new Spammer();

        public event Action<int> IndexChanged;

        private Spammer()
        {
        }

        public void Start(List<string> packets, int delay)
        {
            _packets = packets;
            _delay = delay;
            _cancellationTokenSource = new CancellationTokenSource();
            Task.Run(Spam);
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel(throwOnFirstException: false);
        }

        private async Task Spam()
        {
            int index = 0;
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                if (index >= _packets.Count)
                {
                    index = 0;
                }
                this.IndexChanged?.Invoke(index);
                await Proxy.Instance.SendToServer(_packets[index++]);
                await Task.Delay(_delay);
            }
        }
    }
}