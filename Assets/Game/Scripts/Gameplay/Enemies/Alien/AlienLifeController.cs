using Game.Core.Life;

namespace Game.Gameplay.Enemies.Alien
{
    public class AlienLifeController : ILiveable
    {
        private AlienLifeModel _model;
        private bool _isAlive;
        
        public bool IsAlive()
        {
            return _isAlive;
        }

        public void CheckLive()
        {
            // There is no logic of self killing
        }

        public void Release(DeadReason reason)
        {
            _isAlive = false;
            _model.OnDied?.Invoke(this);
        }

        public void Init(ILifeModel model)
        {
            _isAlive = true;
            _model = (AlienLifeModel)model;
        }
    }
}
