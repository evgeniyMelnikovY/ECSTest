using Remote;
using UnityEngine;
using Zenject;

public class RootInstaller : MonoInstaller
{
    [SerializeField] private Ground _ground;
    [SerializeField] private ClientHandler _client;
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private Actor _actor;
    [SerializeField] private Startup _startup;

    public override void InstallBindings()
    {
        Container.Bind<Actor>().FromComponentInNewPrefab(_actor).AsSingle();
        Container.Bind<Ground>().FromInstance(_ground).AsSingle();
        Container.Bind<LevelBuilder>().FromInstance(_levelBuilder).AsSingle();
        Container.Bind(typeof(IClientHandler), typeof(IClientInitializer)).To<ClientHandler>().FromInstance(_client).AsSingle();

        Container.Bind<Server>().AsSingle().WithArguments(60);

        Container.Bind<Startup>().FromInstance(_startup).AsSingle().NonLazy();
        Container.Bind<CommandsHandler>().AsSingle().NonLazy();
    }
}