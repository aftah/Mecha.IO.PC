using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;

public class GetBiggerExample : TNBehaviour
{
    public System.Collections.Generic.List<GameObject> bodies = new System.Collections.Generic.List<GameObject>();
    private int currentSize = 0;

    public void GetBig()
    {
        if (tno.isMine)
        {
            tno.Send("NetworkGetBig", Target.AllSaved);
            GetComponentInChildren<MoveCameraExample>().camIndex++;
        }

    }

    [RFC]
    private void NetworkGetBig()
    {
        bodies[currentSize + 1].SetActive(true);
        bodies[currentSize].SetActive(false);
        currentSize++;
    }

}
