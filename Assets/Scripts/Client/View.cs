using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public abstract Type Marker { get; }
    
    protected abstract void CreateRawData(ref List<object> result);
    protected abstract void HandleRawData(object data);
    
    private Transform _tr;

    protected virtual void Awake()
    {
        _tr = transform;
    }

    public List<object> GetRawData()
    {
        var result = new List<object>
        {
            new Remote.Transform
            {
                Position = _tr.position, 
                Rotation = _tr.rotation, 
                Scale = _tr.localScale
            }
        };
        
        CreateRawData(ref result);

        return result;
    }

    public void SetRawData(List<object> data)
    {
        foreach (var item in data)
        {
            HandleRawData(item);
            
            if (item is Remote.Transform tr)
            {
                _tr.position = tr.Position;
                _tr.rotation = tr.Rotation;
                _tr.localScale = tr.Scale;
            }
        }
    }
}