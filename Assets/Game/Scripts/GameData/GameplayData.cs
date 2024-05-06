using System;
using Game.UI.Base;
using UnityEngine;

namespace Game.Scripts.GameData
{
    public class GameplayData : IUiModel
    {
        public Action OnChange;
        
        public Vector2 Coordinates { get; private set; }
        public float RotationAngle  { get; private set; }
        public float Velocity  { get; private set; }
        public int LaserAmmo  { get; private set; }
        public int LaserMaxAmmo  { get; private set; }
        public float LaserCooldownNormalTime  { get; private set; }

        private readonly float _laserCooldown;
        
        private float _lastShootTime;
        private bool _laserRestore;

        public GameplayData(float laserCooldown, int laserAmmo)
        {
            _laserCooldown = laserCooldown;
            LaserMaxAmmo = laserAmmo;
        }

        public void SetPlayerState(float velocity, float angle, Vector2 coordinates)
        {
            Coordinates = coordinates;
            RotationAngle = angle;
            Velocity = velocity;
        }

        public bool LaserShoot()
        {
            if (LaserAmmo > 0)
            {
                LaserAmmo--;
                return true;
            }

            return false;
        }

        public void UpdateState(float time)
        {
            if (LaserAmmo < LaserMaxAmmo && !_laserRestore)
            {
                _laserRestore = true;
                _lastShootTime = time;
            }

            if (_laserRestore)
            {
                if (time - _lastShootTime > _laserCooldown)
                {
                    LaserAmmo++;
                    _lastShootTime = time;
                }

                LaserCooldownNormalTime = (time - _lastShootTime) / _laserCooldown;
            }
            else
            {
                LaserCooldownNormalTime = 1f;
            }
            
            if (LaserAmmo == LaserMaxAmmo && _laserRestore)
            {
                _laserRestore = false;
            }
            
            OnChange?.Invoke();
        }
        
    }
}
