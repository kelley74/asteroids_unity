using Game.Core.GameSystems;

namespace Game.Core.Life
{
    public interface ILiveable : IGameEntity
    {
        bool IsAlive();
        void CheckLive();

        void Release(bool self = true);

        void Init(ILifeModel model);
    }

    public interface ILifeModel
    {
        
    }
}