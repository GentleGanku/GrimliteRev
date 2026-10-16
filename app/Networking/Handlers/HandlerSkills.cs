using Grimoire.Botting;
using Grimoire.Tools;
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
            await Task.Delay(10);
            Flash.Call("getRange", new string[0]);
            OptionsManager.SetInfiniteRange(OptionsManager.InfiniteRange);
        }

        #endregion
    }
}
