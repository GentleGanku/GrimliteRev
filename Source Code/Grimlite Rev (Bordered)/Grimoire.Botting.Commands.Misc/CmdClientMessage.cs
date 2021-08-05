using Grimoire.Game;
using Grimoire.Networking;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdClientMessage : IBotCommand
    {
        public string Messages
        {
            get;
            set;
        }

        public bool IsWarning
        {
            get;
            set;
        } = false;

        public async Task Execute(IBotEngine instance)
        {
            if (!IsWarning)
            {
                await Proxy.Instance.SendToClient($"%xt%server%-1%{Messages}%");
            }
            else
            {
                await Proxy.Instance.SendToClient($"%xt%warning%-1%{Messages}%");
            }
        }

        public override string ToString()
        {
            return "Send " + (IsWarning ? "warning" : "info") + " message: " + Messages;
        }
    }
}
