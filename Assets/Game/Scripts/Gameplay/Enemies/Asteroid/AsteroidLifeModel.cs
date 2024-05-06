using System;
using Game.Core.Life;
using Game.Core.Movement;
using UnityEngine;

namespace Game.Gameplay.Enemies.Asteroid
{
    public class AsteroidLifeModel : ILifeModel
    {
        public IMovable Movable { get; private set; }
        public Action<ILiveable,bool,int,Vector3> OnDied { get; private set; }
        public int Generation { get; private set; }
        
        public AsteroidLifeModel(IMovable movable, Action<ILiveable,bool,int,Vector3> onDied, int generation)
        {
            Movable = movable;
            OnDied = onDied;
            Generation = generation;
        }
    }
}