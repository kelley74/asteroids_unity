using UnityEngine;

namespace Game.BindingContainer
{
    public abstract class BindingInstaller : MonoBehaviour
    {
        public abstract void Bind();

        protected void AddBinding<T>(T binding)
        {
            DiContainer.Bind(binding);
        }
    }
}
