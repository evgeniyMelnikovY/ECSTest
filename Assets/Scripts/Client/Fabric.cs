using System;
using System.Collections.Generic;
using UnityEngine;

public class Fabric : MonoBehaviour
{
    [SerializeField] private List<View> _views;

    private readonly Dictionary<Type, GameObject> _objects = new();

    private static Fabric _instance;

    private void Awake()
    {
        foreach (var view in _views)
            _objects[view.Marker] = view.gameObject;
        
        _instance = this;
    }

    public static View Create(List<object> data)
    {
        foreach (var c in data)
        {
            if (_instance._objects.ContainsKey(c.GetType()))
                return Instantiate(_instance._objects[c.GetType()]).GetComponent<View>();
        }

        return null;
    }
}