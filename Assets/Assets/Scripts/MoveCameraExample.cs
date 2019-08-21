﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;

public class MoveCameraExample : TNBehaviour
{
    public System.Collections.Generic.List<Transform> camPos = new System.Collections.Generic.List<Transform>();
    public int camIndex = 0;
    private Vector3 currentVelocity = Vector3.zero;
    public float smoothTime;

    void Start()
    {
        if (!tno.isMine)
            gameObject.SetActive(false);
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, camPos[camIndex].position, ref currentVelocity, smoothTime);
    }
}
