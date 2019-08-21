﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;
using System;

public class SetCockpitPosition : TNBehaviour
{
    public System.Collections.Generic.List<Transform> positions = new System.Collections.Generic.List<Transform>();
    private int positionIndex = 0;
    private Vector3 currentVelocity = Vector3.zero;
    private float smoothTime;
    private bool isMoving = false;

    protected override void Awake()
    {
        base.Awake();

        if (!tno.isMine)
            gameObject.SetActive(false);

        FindObjectOfType<PointsManager>().onLevelUp += OnLevelUpEventHandler;
        smoothTime = StaticData.DataInstance.cockpitMoveSmoothTime;
    }

    private void Update()
    {
        if(isMoving)
            transform.position = Vector3.SmoothDamp(transform.position, positions[positionIndex].position, ref currentVelocity, smoothTime);
    }

    private void OnLevelUpEventHandler(object sender, PointsManager.LevelUpEventArgs e)
    {
        StartCoroutine("MoveCockpit");
    }

    private IEnumerator MoveCockpit()
    {
        positionIndex++;
        isMoving = true;
        yield return new WaitForSeconds(smoothTime);
        isMoving = false;
    }
}
