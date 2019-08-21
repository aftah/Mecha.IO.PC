using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;

public class InstantiateStuff : TNBehaviour
{
    public string stuffPrefab;
    public float autoDestroyDelay;
    public int channelId;

    private void Start()
    {
        channelId = tno.channelID;
    }

    void Update()
    {
        Instantiater();
    }

    public void Instantiater()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (TNManager.isConnected && TNManager.isInChannel)
            {
                Color color = new Color(Random.value, Random.value, Random.value);
                TNManager.Instantiate(channelId, "Stuff", stuffPrefab, true, color);
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
