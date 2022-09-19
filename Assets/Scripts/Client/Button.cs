using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Button : View
{
    [SerializeField] private int _id;
    [SerializeField] private Painter _painter;

    private void OnValidate()
    {
        _painter.SetColor(_id);
    }

    public override Type Marker { get; } = typeof(Remote.Button);

    protected override void CreateRawData(ref List<object> result)
    {
        result.Add(new Remote.Button { Radius = 0.5f });
        result.Add(new Remote.Link { Id = _id });
    }

    protected override void HandleRawData(object data)
    {
        if (data is Remote.Link link)
        {
            _id = link.Id;
            _painter.SetColor(_id);
        }
    }
}