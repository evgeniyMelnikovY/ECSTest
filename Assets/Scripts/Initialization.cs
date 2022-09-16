using System.Collections.Generic;
using System.Linq;
using Remote;
using UnityEngine;

public class Initialization : MonoBehaviour, IClientHandler
{
    [SerializeField] private Actor _actor;
    [SerializeField] private Ground _ground;
    
    private Server _server;

    private void Awake()
    {
        _server = new Server(60, this);
        
        var result = _server.SetupWorld(new List<List<object>>
        {
            _actor.GetRawData()
        });
        
        _ground.Init(_server, result.First().Key);
    }

    private void OnDestroy()
    {
        _server.Dispose();
    }

    public void Tick(Dictionary<int, List<object>> data)
    {
        // Debug.LogError("=====================");
        //
        // var str = new StringBuilder();
        // foreach (var item in data)
        // {
        //     str.Append($"Id: {item.Key} Components: [");
        //
        //     foreach (var c in item.Value)
        //     {
        //         str.Append(c.GetType());
        //
        //         if (c is Transform t)
        //             str.Append($" {t.Position}");
        //
        //         str.Append(", ");
        //     }
        //
        //     str.Append("] ");
        //     str.AppendLine();
        // }
        //
        // Debug.LogError(str);


        foreach (var item in data)
        {
            _actor.SetRawData(item.Value);
        }
        
        _ground.UpdateFromRemote(data);
    }
}