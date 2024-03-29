﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoBehaviour
{
    public static StaticData DataInstance;


    public int hpMax;
    public int hpMaxPerLevel;
    public int pointsThreshold;
    public int pointsThresholdPerLevel;
    public int healPerItem;
    public int pointsPerItem;
    public float characterMoveSpeed;
    public float characterMoveSpeedPerLevel;
    public float characterRotationSpeed;
    public float characterRotationSpeedPerLevel;
    public float spherCastRange;
    public float sphereCastRangePerLevel;
    public float sphereCastWidth;
    public float sphereCastWidthevel;
    public float weaponFireRate;
    public float weaponFireRatePerLevel;
    public float networkSendRate;
    public float cockpitMoveSmoothTime;
    public int numberOfTeleportableObjects;
    public string Object;
    public float deathRangeLootOffset;
    public float autoDestroyDelay;
    public Transform topLeftCorner;
    public Transform bottomRightCorner;
    [HideInInspector] public float topLeftCornerPosition;
    [HideInInspector] public float bottomRightCornerPosition;



    private void Awake()
    {
        if (DataInstance != null)
        {
            Destroy(this);
        }
        else
        {
            DataInstance = this;
        }

        topLeftCornerPosition = topLeftCorner.transform.position.z;
        bottomRightCornerPosition = bottomRightCorner.transform.position.x;
    }
    

}
