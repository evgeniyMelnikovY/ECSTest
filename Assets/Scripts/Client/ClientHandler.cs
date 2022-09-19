using System.Collections.Generic;
using Remote;
using UnityEngine;
using Transform = UnityEngine.Transform;

public class ClientHandler : MonoBehaviour, IClientHandler
{
    [SerializeField] private Transform _environmentContainer;
    
    private readonly Dictionary<int, View> _views = new();
    
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
    }
}