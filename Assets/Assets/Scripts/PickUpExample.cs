using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;
using System.Linq;

public class PickUpExample : TNBehaviour
{
    public int heal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && tno.isMine)
        {
            tno.Send("Pickup", Target.AllSaved, other.gameObject.GetComponentsInParent<TNObject>().Where(x => x.uid != 0).FirstOrDefault().uid);
        }
    }

    [RFC]
    public void Pickup(uint id)
    {
        TNObject.Find(tno.channelID, id).GetComponent<HealthManagerExample>().AddHealth(heal);
        DestroySelf();
    }
}
