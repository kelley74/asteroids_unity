using System;
using Game.Core.Life;
using Game.Core.Movement;

namespace Game.Gameplay.Player
{
    public class BulletLifeModel : ILifeModel
    {
        public Action<ILiveable> OnDied { get; private set; }
        public IMovable Movable;

        public BulletLifeModel(Action<ILiveable> onDied, IMovable movable)
        {
            OnDied = onDied;
            Movable = movable;
        }
    }
}