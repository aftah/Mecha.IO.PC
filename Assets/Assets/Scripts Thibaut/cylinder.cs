using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;
using System.Linq;

public class cylinder : TNBehaviour
{
    public int heal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && tno.isMine)
        {
            tno.Send("Cylinder", Target.AllSaved, other.gameObject.GetComponentsInParent<TNObject>().Where(x => x.uid != 0).FirstOrDefault().uid);
        }
    }
     
    [RFC]
    public void Cylinder(uint id)
    {
        TNObject.Find(tno.channelID, id).GetComponent<healthManager>().AddHealth(heal);
        DestroySelf();
    }


}
