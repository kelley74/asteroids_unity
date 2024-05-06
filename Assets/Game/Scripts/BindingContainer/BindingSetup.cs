using UnityEngine;

namespace Game.BindingContainer
{
    public class BindingSetup : MonoBehaviour
    {
        [SerializeField] private BindingInstaller[] _installers;
        private void Awake()
        {
            DiContainer.Create();
            foreach (var installer in _installers)
            {
                installer.Bind();
            }
        }
    }
}
