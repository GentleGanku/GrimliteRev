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
        }

        #endregion
    }
}
