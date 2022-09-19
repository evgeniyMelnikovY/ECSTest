using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public List<List<object>> Build()
    {
        var data = new List<List<object>>();
        foreach (var actor in FindObjectsOfType<View>())
        {
            data.Add(actor.GetRawData());
            DestroyImmediate(actor.gameObject);
        }

        return data;
    }
}