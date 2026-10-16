using System;
using System.Threading.Tasks;

namespace Grimoire.Game.Data
{
    public class Aura
    {
        public int entityID
        { 
            get;
            set;
        }

        public string Name { get; set; }

        public int Value { get; set; } = 0;

        public string realValue { get; set; }

        public string disableType { get; set; }

        public int Duration
        {
            get
            {
                return durationSpan - time().Seconds;
            }
            set { }
        }

        public DateTime timeNow = new DateTime();

        public int durationSpan;

        public TimeSpan time()
        {
            return DateTime.Now - timeNow;
        }

        public async void countdown(Aura aura)
        {
            await Task.Delay(durationSpan + 000);
            World.PlayerAuras.Remove(aura);
            World.EnemyAuras.Remove(aura);
            World.auraCountdown -= aura.countdown;
        }

    }
}