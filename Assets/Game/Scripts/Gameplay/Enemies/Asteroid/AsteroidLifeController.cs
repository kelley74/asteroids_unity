using System;
using Game.Core.Life;
using UnityEngine;

namespace Game.Gameplay.Enemies.Asteroid
{
    public class AsteroidLifeController : ILiveable
    {
        private Vector3 _boundaries;
        private AsteroidLifeModel _model;
        private bool _isAlive;

        void ILiveable.Release(bool self)
        {
            _model.OnDied.Invoke(this, self, _model.Generation, _model.Movable.GetPosition());
        }

        void ILiveable.Init(ILifeModel model)
        {
            var camera = Camera.main;
            var ratio = (float)Screen.width / Screen.height;
            if (camera == null)
            {
                throw new Exception("Camera not found");
            }

            var size = camera.orthographicSize;
            _boundaries = new Vector3(ratio * size, size);
            _isAlive = true;
            _model = (AsteroidLifeModel)model;
        }

        bool ILiveable.IsAlive()
        {
            return _isAlive;
        }

        void ILiveable.CheckLive()
        {
            var position = _model.Movable.GetPosition();
            if (position.x > _boundaries.x ||
                position.x < -_boundaries.x ||
                position.y > _boundaries.y ||
                position.y < -_boundaries.y)
            {
                _isAlive = false;
            }
        }
    }
}