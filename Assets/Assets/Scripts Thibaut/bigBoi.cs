using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;

public class bigBoi : TNBehaviour
{
    public System.Collections.Generic.List<GameObject> bodies = new System.Collections.Generic.List<GameObject>();
    private int currentSize = 0;

    public void GetBig()
    {
        if (tno.isMine)
        {
            tno.Send("NetGetBig", Target.AllSaved);
            GetComponentInChildren<camScript>().camIndex++;
        }
        
    }

    [RFC]
    private void NetGetBig()
    {
        bodies[currentSize + 1].SetActive(true);
        bodies[currentSize].SetActive(false);
        currentSize++;
    }

}
