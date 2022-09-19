using System.Collections.Generic;
using Remote;
using UnityEngine;
using Zenject;
using Transform = UnityEngine.Transform;

public interface IClientInitializer
{
    void Init(Dictionary<int, List<object>> data);
}

public class ClientHandler : MonoBehaviour, IClientHandler, IClientInitializer
{
    [SerializeField] private Transform _environmentContainer;
    
    private readonly Dictionary<int, View> _views = new();

    private Ground _ground;

    [Inject]
    public void Construct(Ground ground)
    {
        _ground = ground;
    }

    public void Init(Dictionary<int, List<object>> data)
    {
        _ground.Init(data);
        Tick(data);
    }

    public void Tick(Dictionary<int, List<object>> data)
    {
        foreach (var item in data)
        {
            if (!_views.ContainsKey(item.Key))
            {
                var view = Fabric.Create(item.Value);
                view.transform.SetParent(_environmentContainer, true);
                _views[item.Key] = view;
            }
            
            _views[item.Key].SetRawData(item.Value);
        }
        
        _ground.Tick(data);
    }
}