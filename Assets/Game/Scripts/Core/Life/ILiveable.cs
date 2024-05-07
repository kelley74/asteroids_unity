using Game.Core.GameSystems;

namespace Game.Core.Life
{
    public interface ILiveable : IGameEntity
    {
        bool IsAlive();
        void CheckLive();

        void Release(DeadReason reason);

        void Init(ILifeModel model);
    }
}