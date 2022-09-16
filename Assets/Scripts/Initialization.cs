using System.Collections.Generic;
using System.Text;
using Remote;
using UnityEngine;
using Transform = Remote.Transform;

public class Initialization : MonoBehaviour, IClientHandler
{
    [SerializeField] private Actor _actor;
    
    private Server _server;

    private void Awake()
    {
        _server = new Server(60, this);
        
        _server.SetupWorld(new List<List<object>>
        {
            new List<object>(_actor.GetRawData())
            {
                new Destination{Position = Vector3.one * 100},
            }
        });
    }

    private void OnDestroy()
    {
        _server.Dispose();
    }

    public void Tick(Dictionary<int, List<object>> data)
    {
        Debug.LogError("=====================");
        
        var str = new StringBuilder();
        foreach (var item in data)
        {
            str.Append($"Id: {item.Key} Components: [");

            foreach (var c in item.Value)
            {
                str.Append(c.GetType());

                if (c is Transform t)
                    str.Append($" {t.Position}");

                str.Append(", ");
            }

            str.Append("] ");
            str.AppendLine();
        }
        
        Debug.LogError(str);


        foreach (var item in data)
        {
            _actor.SetRawData(item.Value);
        }
    }
}