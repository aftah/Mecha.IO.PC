using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;
using System;

public class DeathManager : TNBehaviour
{

    private float mapHeight;
    private float mapLength;
    private string destroyableObject;
    private float offset;

    protected override void Awake()
    {
        base.Awake();
        mapHeight = StaticData.DataInstance.topLeftCornerPosition;
        mapLength = StaticData.DataInstance.bottomRightCornerPosition;
        offset = StaticData.DataInstance.deathRangeLootOffset;
        destroyableObject = StaticData.DataInstance.Object;
    }

    public class OnDeathEventArgs : EventArgs
    {
        public uint id;
    }

    public event EventHandler<OnDeathEventArgs> onDeathEvent;

    public void OnDeathEvent(OnDeathEventArgs e)
    {
        onDeathEvent?.Invoke(this, e);
    }

    public void Die(uint id)
    {
        if (tno.isMine)
        {
            InstantiateItem(id);
            OnDeathEvent(new OnDeathEventArgs() { id = id });
            int randomZ = UnityEngine.Random.Range(0, (int)mapHeight);
            int randomX = UnityEngine.Random.Range(0, (int)mapLength);
            tno.Send("Respawn", Target.AllSaved, randomZ, randomX);
        }
    }

    private void InstantiateItem(uint id)
    {
        for (int i = 0; i < GetComponent<PointsManager>().CurrentLevel; i++)
        {
            float randomX = UnityEngine.Random.Range(gameObject.transform.position.x - offset, gameObject.transform.position.x + offset);
            float randomZ = UnityEngine.Random.Range(gameObject.transform.position.z - offset, gameObject.transform.position.z + offset);
            TNManager.Instantiate(tno.channelID, "Item", destroyableObject, true, randomX, randomZ);
        }
    }

    [RFC]
    private void Respawn(int Zpos, int Xpos)
    {
        gameObject.transform.position = new Vector3(Xpos, 0, Zpos);
    }

    [RCC]
    static GameObject Item(GameObject prefab, float XPos, float ZPos)
    {
        GameObject niqueTaRace = prefab.Instantiate();
        niqueTaRace.GetComponent<CollectableObject>().Destroyable = true;
        niqueTaRace.transform.position = new Vector3(XPos, 0, ZPos);
        return niqueTaRace;
    }
}
