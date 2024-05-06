using Game.Core.Movement;

namespace Game.Gameplay.Enemies
{
    public interface IMoveTargetHolder
    {
        void SetTarget(IMovable target);
    }
}