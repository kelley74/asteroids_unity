using System;
using Game.Core.Life;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public class BulletLifeController : ILiveable
    {
        private Vector3 _boundaries;
        private BulletLifeModel _model;
        private bool _isAlive;
        
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

        public void Release(bool self)
        {
            _isAlive = false;
            _model.OnDied.Invoke(this);
        }

        public void Init(ILifeModel model)
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
            _model = (BulletLifeModel)model;
        }
    }
}