using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;
using System;

public class ItemInstantiate : TNBehaviour
{
    [SerializeField] private Transform mapCornerTop;
    [SerializeField] private Transform mapCornerRight;
    private float mapHeight;
    private float mapLength;
    private int numberItems;
    private string prefabToInstantiate;

    public float MapHeight { get => mapHeight; set => mapHeight = value; }
    public float MapLength { get => mapLength; set => mapLength = value; }

    protected override void Awake()
    {
        base.Awake();
        numberItems = StaticData.DataInstance.numberOfTeleportableObjects;
        prefabToInstantiate = StaticData.DataInstance.teleportableObject;
    }

    private void Start()
    {
        MapHeight = mapCornerTop.transform.position.z;
        MapLength = mapCornerRight.transform.position.x;

        Invoke("InstantiateObjects", 1);
    }

    private void InstantiateObjects()
    {
        for (int i = 0; i < numberItems; i++)
        {
            int randomZ = UnityEngine.Random.Range(0, (int)MapHeight);
            int randomX = UnityEngine.Random.Range(0, (int)MapLength);
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
