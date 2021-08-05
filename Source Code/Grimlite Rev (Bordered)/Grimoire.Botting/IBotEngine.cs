using System;
using System.Collections.Generic;

namespace Grimoire.Botting
{
    public interface IBotEngine
    {
        bool IsRunning
        {
            get;
            set;
        }

        int Index
        {
            get;
            set;
        }

        bool IsVar(string value);

        Configuration Configuration
        {
            get;
            set;
        }

        int CurrentConfiguration { get; set; }

        string GetVar(string value);

        event Action<bool> IsRunningChanged;

        event Action<int> IndexChanged;

        event Action<Configuration> ConfigurationChanged;

        void Start(Configuration config);

        void Stop();
    }
}