﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;
using System;

public class ItemInstantiate : TNBehaviour
{
    private float mapHeight;
    private float mapLength;
    private int numberItems;
    private string prefabToInstantiate;

    protected override void Awake()
    {
        base.Awake();
        numberItems = StaticData.DataInstance.numberOfTeleportableObjects;
        prefabToInstantiate = StaticData.DataInstance.Object;
        mapHeight = StaticData.DataInstance.topLeftCornerPosition;
        mapLength = StaticData.DataInstance.bottomRightCornerPosition;
    }

    private void Start()
    {
        if (TNManager.GetChannel(tno.channelID).players.Count == 0)
            Invoke("InstantiateObjects", 1);
    }

    private void InstantiateObjects()
    {
        for (int i = 0; i < numberItems; i++)
        {
            int randomZ = UnityEngine.Random.Range(0, (int)mapHeight);
            int randomX = UnityEngine.Random.Range(0, (int)mapLength);
            TNManager.Instantiate(tno.channelID, "Object", prefabToInstantiate, true, randomZ, randomX);
        }
    }

    [RCC]
    static GameObject Object(GameObject prefab, float ZPos, float XPos)
    {
        GameObject lootableObject = prefab.Instantiate();
        lootableObject.transform.position = new Vector3(XPos, 0, ZPos);
        return lootableObject;
    }
}
