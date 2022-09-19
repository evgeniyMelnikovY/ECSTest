using System;
using System.Collections.Generic;
using System.Linq;
using Remote;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour, IPointerClickHandler, IClientHandler
{
    public event Action<Destination> Click;
    
    [SerializeField] private GameObject _indicatorPrefab;

    private GameObject _indicator;
    public int Id;

    public void Init(Dictionary<int, List<object>> source)
    {
        foreach (var item in source)
        {
            if (item.Value.Any(x => x is Remote.Actor))
            {
                Id = item.Key;
                return;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var pos = eventData.pointerCurrentRaycast.worldPosition;
        var dest = new Destination { Position = pos };

        UpdateIndicator(dest);
        
        Click?.Invoke(dest);
    }

    private void UpdateIndicator(Destination source)
    {
        if (_indicator == null)
            _indicator = Instantiate(_indicatorPrefab, transform, true);

        _indicator.transform.position = source.Position;
    }

    public void Tick(Dictionary<int, List<object>> source)
    {
        if (!source.ContainsKey(Id))
            return;

        var data = source[Id];

        foreach (var item in data)
        {
            if (item is Destination destination)
            {
                UpdateIndicator(destination);
                return;
            }
        }

        if (_indicator != null)
            DestroyImmediate(_indicator);
    }
}