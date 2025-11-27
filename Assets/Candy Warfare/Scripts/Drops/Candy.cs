using System;
using System.Collections;
using UnityEngine;

public class Candy : DroppableCurrency
{
    [Header("Actions")]
    public static Action<Candy> onCollected;

    protected override void Collected()
    {
        onCollected?.Invoke(this);
    }
}
