using Grimoire.Botting;
using Grimoire.Tools;
using Grimoire.Utils;
using System;
using System.Threading.Tasks;

namespace Grimoire.Networking.Handlers
{
    public class HandlerSkills : IJsonMessageHandler
    {
        #region Properties

        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "sAct"
        };

        #endregion

        #region Methods

        public async void Handle(JsonMessage message)
        {
            try
            {
                await Task.Delay(10);
                Flash.Call("getRange", new string[0]);
                OptionsManager.SetInfiniteRange(OptionsManager.InfiniteRange);
            }
            catch (Exception ex)
            {
                ErrorReporter.Log("running HandlerSkills.Handle", ex);
            }
        }

        #endregion
    }
}
