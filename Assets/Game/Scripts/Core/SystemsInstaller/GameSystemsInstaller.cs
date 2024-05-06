using Game.BindingContainer;
using Game.Core.Life;
using Game.Core.Movement;
using Game.Scripts.Core.Colliding;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Core.SystemsInstaller
{
    public class GameSystemsInstaller : BindingInstaller
    {
        [SerializeField] private MovementSystem _movementSystem;
        [SerializeField] private LifeSystem _lifeSystem;
        [SerializeField] private CollideSystem _collideSystem;
        
        public override void Bind()
        {
            AddBinding(_movementSystem);
            AddBinding(_lifeSystem);
            AddBinding(_collideSystem);
        }
    }
}
