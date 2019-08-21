using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;
using System.Linq;

public class Weapon : TNBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && tno.isMine)
        {
            tno.SendQuickly("DamageDealer", Target.AllSaved, other.gameObject.GetComponentsInParent<TNObject>().Where(x => x.uid != 0).FirstOrDefault().uid);
        }
    }

    [RFC]
    private void DamageDealer(uint id)
    {
        TNObject.Find(tno.channelID, id).GetComponent<healthManager>().RemoveHealth(damage);
    }
}
