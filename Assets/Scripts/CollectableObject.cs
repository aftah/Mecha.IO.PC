using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;
using System.Linq;

public class CollectableObject : TNBehaviour
{
    private int heal;
    private int points;
    private  bool destroyable = false;
    private float mapLength;
    private float mapHeight;

    public bool Destroyable { get => destroyable; set => destroyable = value; }

    protected override void Awake()
    {
        base.Awake();
        heal = StaticData.DataInstance.healPerItem;
        points = StaticData.DataInstance.pointsPerItem;
        mapLength = StaticData.DataInstance.bottomRightCornerPosition;
        mapHeight = StaticData.DataInstance.topLeftCornerPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" /*&& tno.isMine*/)
        {
            tno.Send("Pickup", Target.AllSaved, other.gameObject.GetComponentsInParent<TNObject>().Where(x => x.uid != 0).FirstOrDefault().uid);
            if (Destroyable)
                Boum();
            else
                Teleport();
        }
    }

    [RFC]
    public void Pickup(uint id)
    {
        TNObject.Find(tno.channelID, id).GetComponent<HealthManager>().AddHealth(heal);
        TNObject.Find(tno.channelID, id).GetComponent<PointsManager>().PointsAdd(points);
    }

    private void Teleport()
    {
        int randomZ = UnityEngine.Random.Range(0, (int)mapHeight);
        int randomX = UnityEngine.Random.Range(0, (int)mapLength);
        tno.Send("TeleportItem", Target.AllSaved, randomZ, randomX);
    }

    private void Boum()
    {
        if (tno.isMine)
            tno.Send("NetworkDestroy", Target.AllSaved);
    }

    [RFC]
    private void NetworkDestroy()
    {
        DestroySelf();
    }

    [RFC]
    private void TeleportItem(int randomZ, int randomX)
    {
        gameObject.transform.position = new Vector3(randomX, 0, randomZ);
    }

}
