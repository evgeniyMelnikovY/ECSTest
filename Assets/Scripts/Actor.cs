using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public List<object> GetRawData()
    {
        return new List<object>
        {
            new Remote.Transform{Position = transform.position},
            new Remote.Actor()
        };
    }

    public void SetRawData(List<object> data)
    {
        foreach (var item in data)
        {
            if (item is Remote.Transform tr)
                transform.position = tr.Position;
        }
    }
}