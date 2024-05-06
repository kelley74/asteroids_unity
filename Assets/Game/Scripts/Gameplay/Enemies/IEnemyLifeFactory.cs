using Game.Core.Life;
using Game.Core.Movement;

namespace Game.Gameplay.Enemies
{
    public interface IEnemyLifeFactory
    {
        ILiveable GetEnemyLifeController(IMovable enemyMoveController, EnemyConfig.EnemyType type, EnemyConfig config);
    }
}