using System.Threading.Tasks;

namespace Grimoire.Botting
{
    public interface IBotCommand
    {
        #region Methods

        Task Execute(IBotEngine instance);

        #endregion
    }
}