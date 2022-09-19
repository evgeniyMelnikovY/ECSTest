using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Door : View
{
    [SerializeField] private int _id;
    [SerializeField] private Transform _mesh;
    [SerializeField] private Painter _painter;

    private float _openProcess = 1;

    private void OnValidate()
    {
        _painter.SetColor(_id);
    }

    public override Type Marker { get; } = typeof(Remote.Door);

    protected override void CreateRawData(ref List<object> result)
    {
        result.Add(new Remote.Door { OpenProcess = _openProcess });
        result.Add(new Remote.Link { Id = _id });
    }

    protected override void HandleRawData(object data)
    {
        switch (data)
        {
            case Remote.Door door:
                _openProcess = door.OpenProcess;
                _mesh.localPosition = new Vector3(0, Mathf.Lerp(-0.5f, 0.5f, _openProcess), 0);
                break;
            case Remote.Link link:
                _id = link.Id;
                _painter.SetColor(_id);
                break;
        }
    }
}