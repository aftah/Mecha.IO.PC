using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;
using System.Linq;

public class healthRemover : TNBehaviour
{
    public int damage = 3;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            tno.Send("Damage", Target.AllSaved, other.gameObject.GetComponentsInParent<TNObject>().Where(x => x.uid != 0).FirstOrDefault().uid);
        }
    }

    [RFC]
    public void Damage(uint id)
    {
        TNObject.Find(tno.channelID, id).GetComponent<HealthManager>().RemoveHealth(damage);
    }
}
