// using UnityEngine;
// using Zenject;
//
// public class ObjectsInstaller : MonoInstaller
// {
//     [SerializeField] private Actor _actor;
//     [SerializeField] private Wall _wall;
//     [SerializeField] private Button _button;
//     [SerializeField] private Door _door;
//     
//     public override void InstallBindings()
//     {
//         Container.BindMemoryPool<Actor, LettersPool>().FromComponentInNewPrefab(_letterView);
//         
//         Container.Bind<Actor>().FromComponentInNewPrefab(_actor).AsSingle();
//         Container.Bind<Wall>().FromComponentInNewPrefab(_wall).AsTransient();
//         Container.Bind<Button>().FromComponentInNewPrefab(_button).AsTransient();
//         Container.Bind<Door>().FromComponentInNewPrefab(_door).AsTransient();
//     }
// }
//
// public class ActorsPool : MemoryPool<Actor>
// {
//     protected override void OnDespawned(ILetterView item)
//     {
//         item.Transform.parent = null;
//         item.Transform.gameObject.SetActive(false);
//     }
//
//     protected override void OnSpawned(ILetterView item)
//     {
//         item.Transform.gameObject.SetActive(true);
//     }
// }