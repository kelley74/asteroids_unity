using Game.UI.Base;

namespace Game.Scripts.GameData
{
    public class GameRoundData : IUiModel
    {
        public int EnemiesKilled { get; private set; }

        public void AddKill()
        {
            EnemiesKilled++;
        }
    }
}
