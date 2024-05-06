using Game.BindingContainer;
using Game.Core.Input;
using Game.Services.Pools;
using UnityEngine;

namespace Game.Services.ServiceInstaller
{
    public class GameServiceInstaller : BindingInstaller
    {
        [SerializeField] private InputController _inputController;
        [SerializeField] private GoPool _pool;
        
        public override void Bind()
        {
            AddBinding(_inputController);
            AddBinding(_pool);
        }
    }
}
