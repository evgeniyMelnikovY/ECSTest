using System;
using System.Collections.Generic;
using UnityEngine;

public class Actor : View
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private const float Step = 0.1f;

    public override Type Marker { get; } = typeof(Remote.Actor);

    private float _animSpeed;
    private float _targetSpeed;

    protected override void CreateRawData(ref List<object> result)
    {
        result.Add(new Remote.Movement {Speed = _speed});
        result.Add(new Remote.Actor());
    }

    protected override void HandleRawData(object data)
    {
        if (data is Remote.Transform tr)
        {
            _targetSpeed = 0;
            if (transform.position != tr.Position)
                _targetSpeed = 1;
        }
        
        if (data is Remote.Destination des)
        {
            _animator.transform.LookAt(des.Position);
        }
    }

    private void Update()
    {
        _animSpeed += Math.Sign(_targetSpeed - _animSpeed) * Step;
        _animSpeed = Mathf.Clamp01(_animSpeed);
        _animator.SetFloat(Speed, _animSpeed);
    }
}