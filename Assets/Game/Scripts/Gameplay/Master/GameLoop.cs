using System.Threading;
using Cysharp.Threading.Tasks;
using Game.BindingContainer;
using Game.Core.Colliding;
using Game.Core.Life;
using Game.Core.Movement;
using Game.Gameplay.Enemies;

namespace Game.Gameplay.Master
{
    public class GameLoop
    {
        private CancellationTokenSource _cancelToken;

        private MovementSystem _movementSystem;
        private LifeSystem _lifeSystem;
        private CollideSystem _collideSystem;
        private EnemySpawnSystem _enemySpawnSystem;
        
        public void StartLoop()
        {
            _movementSystem = DiContainer.Resolve<MovementSystem>();
            _lifeSystem = DiContainer.Resolve<LifeSystem>();
            _collideSystem = DiContainer.Resolve<CollideSystem>();
            _enemySpawnSystem = DiContainer.Resolve<EnemySpawnSystem>();
            _cancelToken = new CancellationTokenSource();
            UpdateLoop(_cancelToken.Token).Forget();
        }

        public void StopLoop()
        {
            _cancelToken.Cancel();
            _cancelToken.Dispose();
        }
        
        async UniTask UpdateLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                _movementSystem.Update();
                _lifeSystem.Update();
                _collideSystem.Update();
                _enemySpawnSystem.Update();
                await UniTask.Yield(token);
            }
        }
    }
}