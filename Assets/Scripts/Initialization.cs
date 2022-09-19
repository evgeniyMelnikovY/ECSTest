using Remote;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    [SerializeField] private Ground _ground;
    [SerializeField] private ClientHandler _client;
    [SerializeField] private LevelBuilder _levelBuilder;

    private Server _server;

    private void Awake()
    {
        _server = new Server(60, _client);
    }

    private void Start()
    {
        var level = _levelBuilder.Build();
        var data = _server.SetupWorld(level);
        _client.Tick(data);
      
    }

    private void OnDestroy()
    {
        _server.Dispose();
    }
}