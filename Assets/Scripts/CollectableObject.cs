using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;
using System.Linq;

public abstract class CollectableObject : TNBehaviour
{
    private int heal;
    private int points;

    protected override void Awake()
    {
        base.Awake();
        heal = StaticData.DataInstance.healPerItem;
        points = StaticData.DataInstance.pointsPerItem;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && tno.isMine)
        {
            tno.Send("Pickup", Target.AllSaved, other.gameObject.GetComponentsInParent<TNObject>().Where(x => x.uid != 0).FirstOrDefault().uid);
            OnPickUp();
        }
    }

    [RFC]
    public void Pickup(uint id)
    {
        TNObject.Find(tno.channelID, id).GetComponent<HealthManager>().AddHealth(heal);
        TNObject.Find(tno.channelID, id).GetComponent<PointsManager>().PointsAdd(points);
    }

    protected virtual void OnPickUp()
    {

    }

}
