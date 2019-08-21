using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;

public class InstantiationExample : TNBehaviour
{
    public string prefabToInstantiate;
    public float autoDestroyDelay;
    private int channelId;

    private void Start()
    {
        channelId = tno.channelID;
    }

    void Update()
    {
        InstantiatePrefab();
    }

    public void InstantiatePrefab()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (TNManager.isConnected && TNManager.isInChannel)
            {
                Color color = new Color(Random.value, Random.value, Random.value);
                TNManager.Instantiate(channelId, "Stuff", prefabToInstantiate, true, color);
            }
        }
    }

    [RCC]
    static GameObject Stuff(GameObject prefab, Color c)
    {
        GameObject cube = prefab.Instantiate();
        cube.GetComponentInChildren<Renderer>().material.color = c;
        return cube;
    }
}
