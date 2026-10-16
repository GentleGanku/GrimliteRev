using Grimoire.Networking;
using System.Threading.Tasks;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdClientMessage : IBotCommand
    {
        public string Messages { get; set; }

        public bool IsWarning { get; set; }

        public async Task Execute(IBotEngine instance)
        {
            await Proxy.Instance.SendToClient(IsWarning ? $"%xt%warning%-1%{Messages}%" : $"%xt%server%-1%{Messages}%");
        }

        public override string ToString()
        {
            return "Send " + (IsWarning ? "warning" : "info") + " message: " + Messages;
        }
    }
}
