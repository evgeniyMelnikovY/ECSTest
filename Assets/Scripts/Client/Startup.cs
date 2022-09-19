using Remote;
using UnityEngine;
using Zenject;

public class Startup : MonoBehaviour
{
    private Server _server;
    private LevelBuilder _levelBuilder;
    private IClientInitializer _client;

    [Inject]
    public void Construct(
        Server server, 
        LevelBuilder levelBuilder, 
        IClientInitializer client)
    {
        _server = server;
        _levelBuilder = levelBuilder;
        _client = client;
    }

    private void Start()
    {
        var levelData = _levelBuilder.Build();
        var data = _server.SetupWorld(levelData);
        _client.Init(data);
    }
}