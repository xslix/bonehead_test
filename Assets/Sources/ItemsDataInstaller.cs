using Sources;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class ItemsDataInstaller : MonoInstaller
{
    [SerializeField] private ItemsData _itemsData;
    public override void InstallBindings()
    {
        Container.Bind<ItemsData>().FromInstance(_itemsData).AsSingle();
    }
}