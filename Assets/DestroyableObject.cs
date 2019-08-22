using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;

public class DestroyableObject : CollectableObject
{
    //protected override void OnPickUp()
    //{
    //    if (tno.isMine)
    //        tno.Send("Boum", Target.AllSaved);
    //}

    [RFC]

    public void Boum()
    {
        DestroySelf();
    }
}
