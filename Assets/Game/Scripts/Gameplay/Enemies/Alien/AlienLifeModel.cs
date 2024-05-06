using System;
using Game.Core.Life;

namespace Game.Gameplay.Enemies.Alien
{
    public class AlienLifeModel : ILifeModel
    {
        public Action<ILiveable> OnDied { get; private set; }

        public AlienLifeModel(Action<ILiveable> onDied)
        {
            OnDied = onDied;
        }
    }
}
