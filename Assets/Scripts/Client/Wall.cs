using System;
using System.Collections.Generic;

public class Wall : View
{
    public override Type Marker { get; } = typeof(Remote.Wall);

    protected override void CreateRawData(ref List<object> result)
    {
        result.Add(new Remote.Wall());
    }

    protected override void HandleRawData(object data)
    {
    }
}