using Game.BindingContainer;
using Game.Gameplay.Master;
using Game.Gameplay.Floaters;
using UnityEngine;

namespace Game.Gameplay.GameInstaller
{
    public class GameMasterInstaller : BindingInstaller
    {
        [SerializeField] private GameMaster _gameMaster;
        [SerializeField] private FloaterSpawner _floaterSpawner;
        
        public override void Bind()
        {
            AddBinding(_gameMaster);
            AddBinding(_floaterSpawner);
        }
    }
}
