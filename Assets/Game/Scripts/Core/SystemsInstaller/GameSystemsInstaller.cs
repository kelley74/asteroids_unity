using Game.BindingContainer;
using Game.Core.Colliding;
using Game.Core.Life;
using Game.Core.Movement;
using Game.Gameplay.Enemies;

namespace Game.Core.SystemsInstaller
{
    public class GameSystemsInstaller : BindingInstaller
    {
        public override void Bind()
        {
            AddBinding(new MovementSystem());
            AddBinding(new LifeSystem());
            AddBinding(new CollideSystem());
            AddBinding(new EnemySpawnSystem());
        }
    }
}
