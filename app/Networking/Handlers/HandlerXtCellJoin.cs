using Grimoire.Botting;
using Grimoire.Game;

namespace Grimoire.Networking.Handlers
{
    public class HandlerXtCellJoin : IXtMessageHandler
    {
        #region Properties

        public string[] HandledCommands
        {
            get;
        } = new string[1]
        {
            "moveToCell"
        };

        #endregion

        #region Methods

        public void Handle(XtMessage message)
        {
            World.SetSpawnPoint();
            if (OptionsManager.HideYulgar && Player.Map.ToLower() == "yulgar")
                OptionsManager.DestroyPlayers(OptionsManager.HideYulgar);
            if (OptionsManager.HidePlayers)
                OptionsManager.DestroyPlayers(true);
        }

        #endregion
    }
}
