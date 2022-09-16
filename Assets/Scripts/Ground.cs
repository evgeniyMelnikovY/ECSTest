using System.Collections.Generic;
using Remote;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _indicatorPrefab;

    private GameObject _indicator;
    private Server _server;
    private int _id;

    public void Init(Server server, int id)
    {
        _server = server;
        _id = id;
    }

    public void UpdateFromRemote(Dictionary<int, List<object>> source)
    {
        if (!source.ContainsKey(_id))
            return;

        var data = source[_id];

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

    public void OnPointerClick(PointerEventData eventData)
    {
        // if (eventData.pointerCurrentRaycast.gameObject != gameObject)
            // return;
        
        var pos = eventData.pointerCurrentRaycast.worldPosition;
        var dest = new Destination { Position = pos };

        UpdateIndicator(dest);

        _server.UserAction(new Dictionary<int, List<object>> { { _id, new List<object> { dest } } });
    }

    private void UpdateIndicator(Destination source)
    {
        if (_indicator == null)
            _indicator = Instantiate(_indicatorPrefab, transform, true);

        _indicator.transform.position = source.Position;
    }
}