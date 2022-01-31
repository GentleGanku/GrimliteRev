using System.Threading.Tasks;

namespace Grimoire.Botting
{
    public interface IBotCommand
    {
        Task Execute(IBotEngine instance);
    }
}