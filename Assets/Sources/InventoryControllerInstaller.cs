using UnityEngine;
using Zenject;

namespace Sources
{
    public class InventoryControllerInstaller : MonoInstaller
    {
        [SerializeField] private InventoryController _controller;
        public override void InstallBindings()
        {
            Container.Bind<InventoryController>().FromInstance(_controller).AsSingle();
        }
    }
}