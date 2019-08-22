using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;

public class TeleportableObject : CollectableObject
{
    private float mapLength;
    private float mapHeight;

    protected override void Awake()
    {
        base.Awake();
        mapLength = StaticData.DataInstance.bottomRightCornerPosition;
        mapHeight = StaticData.DataInstance.topLeftCornerPosition;
    }

    protected override void OnPickUp()
    {
        tno.Send("TeleportItem", Target.AllSaved);
    }

    [RFC]
    private void TeleportItem()
    {
        int randomZ = UnityEngine.Random.Range(0, (int)mapHeight);
        int randomX = UnityEngine.Random.Range(0, (int)mapLength);
        gameObject.transform.position = new Vector3(randomX, 0, randomZ);
    }
}
